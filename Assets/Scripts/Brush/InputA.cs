using UnityEngine;

public class InputA : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _distanceDraw = 0.05f;
    [SerializeField] private Transform _paintAndRaycastTransform;

    [Header("Brush settings")] [SerializeField]
    private SettingsBrush _settingsBrush;

    [SerializeField] private Transform _painTransform;
    [SerializeField] private float _speedBrushY = 12f;
    [SerializeField] private float _speedBrushX = 12f;
    [SerializeField] private float _distancBrushToObjectX = 0.1f;
    [SerializeField] private float _limitUpY = 1f;
    [SerializeField] private float _limitDownY = 1f;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRendererBrush;
    [SerializeField] private float _speedSkinedBrush = 12f;

    [Header("Raycast settings")] [SerializeField]
    private Transform _raycastTransform;

    [SerializeField] private float _maxDistanceRaycast = 4f;

    [Header("Particle system")] [SerializeField]
    private GameObject _paticleSystemBrushGameObject;

    private ParticleSystem _particleSystemBrush;
    private Vector3 _positionFollowerY;
    private Vector3 _positionForwardPaint;
    private Vector3 _upEdge;
    private float _valueSkinnedMeshBrush;
    private bool _canDraw;
    private bool _isLimitPosition;
    private bool _isUIMenu;

    private void Start()
    {
        _positionFollowerY = _paintAndRaycastTransform.position;
        _positionForwardPaint = _raycastTransform.position;
        _upEdge = _camera.ViewportToWorldPoint(new Vector3(0, 1, _camera.transform.position.z));
        _particleSystemBrush = _paticleSystemBrushGameObject.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!_isUIMenu)
            Controller();
        
        MoveBrush();
    }

    private void Controller()
    {
        if (Input.GetMouseButton(0))
        {
            FollowerMousePositionY();
            if (!_isLimitPosition)
            {
                TryMoveBorder();
                CanDraw();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            ResetBrush();
            _settingsBrush.SetAngleDecal(Random.Range(-180, 180));
        }
    }

    private void CanDraw()
    {
        if ((!(_painTransform.position.x <= _positionForwardPaint.x + _distanceDraw) ||
             !(_painTransform.position.x >= _positionForwardPaint.x - _distanceDraw)) || !_canDraw) return;

        Handheld.Vibrate();
        _settingsBrush.SetOpacityFromSlider();
        _paticleSystemBrushGameObject.SetActive(true);
        var main = _particleSystemBrush.main;
        main.startColor = _settingsBrush.ColorBrush;
        _valueSkinnedMeshBrush = 100f;
    }

    private void MoveBrush()
    {
        _paintAndRaycastTransform.position = Vector3.MoveTowards(_paintAndRaycastTransform.position, _positionFollowerY,
            _speedBrushY * Time.deltaTime);

        if ((_paintAndRaycastTransform.position.y >= _positionFollowerY.y - _distancBrushToObjectX &&
             _paintAndRaycastTransform.position.y <= _positionFollowerY.y + _distancBrushToObjectX))
        {
            _painTransform.position =
                Vector3.MoveTowards(_painTransform.position, _positionForwardPaint, _speedBrushX * Time.deltaTime);
        }

        var currentSkinned = _skinnedMeshRendererBrush.GetBlendShapeWeight(0);
        var result = Mathf.Lerp(currentSkinned, _valueSkinnedMeshBrush, _speedSkinedBrush * Time.deltaTime);
        _skinnedMeshRendererBrush.SetBlendShapeWeight(0, result);
    }

    private void FollowerMousePositionY()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = -_camera.transform.position.z;
        var mousePositionWorldPoint = _camera.ScreenToWorldPoint(mousePosition);
        mousePositionWorldPoint.y = LimitPositionY(mousePositionWorldPoint.y);
        var position = _paintAndRaycastTransform.position;
        _positionFollowerY = new Vector3(_paintAndRaycastTransform.position.x, mousePositionWorldPoint.y,
            _paintAndRaycastTransform.position.z);
    }

    private float LimitPositionY(float positionY)
    {
        if (_upEdge.y - _limitUpY < positionY || -_upEdge.y + _limitDownY > positionY)
        {
            _isLimitPosition = true;
            return _paintAndRaycastTransform.position.y;
        }

        _isLimitPosition = false;
        return positionY;
    }

    private void TryMoveBorder()
    {
        var raycastPosition = _raycastTransform.position;
        var raycastDirection = _raycastTransform.forward;
        Physics.Raycast(raycastPosition, raycastDirection, out var borderForwardInfo, _maxDistanceRaycast);
        Debug.DrawRay(raycastPosition, raycastDirection * _maxDistanceRaycast, Color.green);
        _positionForwardPaint = _raycastTransform.position;
        _valueSkinnedMeshBrush = 0;
        _canDraw = false;
        _paticleSystemBrushGameObject.SetActive(false);
        if (borderForwardInfo.collider == null) return;

        _canDraw = true;
        _positionForwardPaint = borderForwardInfo.point;
    }

    private void ResetBrush()
    {
        _paticleSystemBrushGameObject.SetActive(false);
        _positionForwardPaint = _raycastTransform.position;
        _valueSkinnedMeshBrush = 0;
        _settingsBrush.SetOpacity(0);
    }

    public void SetIsUIMenu(bool state) => _isUIMenu = state;
}