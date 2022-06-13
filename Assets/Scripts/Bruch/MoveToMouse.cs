using UnityEngine;
using UnityEngine.EventSystems;

public class MoveToMouse : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [Header("Settings")] [SerializeField] private float _speed = 12.0f;
    [SerializeField] private float _distanceFromCamera = 2.0f;
    [SerializeField] private float _offestPositionObjectZ = 0.1f;
    [SerializeField] private float _offestPositionBorder = 0.1f;

    [Header("Limit position")] [SerializeField]
    private float _limitPositionX = 1f;

    [Header("Raycast")] [SerializeField] private Transform _raycastTransform;

    [SerializeField] private float _maxDistanceRaycast = 0.5f;

    [SerializeField] private SkinnedMeshRenderer _brushSkinnedMeshRenderer;
    [SerializeField] private GameObject _paintObject;
    [SerializeField] private float _positionBruchZ = -1f;

    private LimitPositionBrush _limitPositionBrush;
    private Vector3 _positionBorder = Vector3.zero;
    private Vector3 _rightEdge;
    private Vector3 _upEdge;
    private Vector3 _positionTargetMouse;
    private float _blendShapesBrush;
    private float _currentBlendShapesBruch;
    private bool _isEndBorder;
    private bool _canDraw;

    private void Start()
    {
        SetLimitPosition();
        ResetBrushButtonUp();
    }

    private void SetLimitPosition()
    {
        _rightEdge = _camera.ViewportToWorldPoint(new Vector3(0, -1, _camera.transform.position.z));
        _upEdge = _camera.ViewportToWorldPoint(new Vector3(0, 1, _camera.transform.position.z));

        _limitPositionBrush = new LimitPositionBrush(_limitPositionX, _rightEdge.x - _offestPositionBorder,
            _upEdge.y - _offestPositionBorder / 4f, -_upEdge.y + _offestPositionBorder);
    }

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            MoveBrush();
        UpdateBlendShapesBrush();
    }

    private void MoveBrush()
    {
    //    transform.position = _positionTargetMouse;
        transform.position = Vector3.Lerp(transform.position, _positionTargetMouse,
            1.0f - Mathf.Exp(-_speed * Time.deltaTime));
    }

    public void ResetBrushButtonUp()
    {
        _canDraw = false;
        _paintObject.SetActive(false);
        _positionTargetMouse.z = _positionBruchZ;
        _positionTargetMouse.x = _rightEdge.x - _offestPositionBorder;
        _blendShapesBrush = 0f;
    }

    private void UpdateBlendShapesBrush()
    {
        _currentBlendShapesBruch = Mathf.Lerp(_currentBlendShapesBruch, _blendShapesBrush, _speed / 2 * Time.deltaTime);
        _brushSkinnedMeshRenderer.SetBlendShapeWeight(0, _currentBlendShapesBruch);
    }

    public void FollowerMouse()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = _distanceFromCamera;
        _positionTargetMouse = _camera.ViewportToWorldPoint(_camera.ScreenToViewportPoint(mousePosition));
        _positionTargetMouse.z = _positionBruchZ;
        _positionTargetMouse = _limitPositionBrush.Limit(_positionTargetMouse);

        TryMoveBorderPaintZ(out var result, _positionTargetMouse);
        _positionTargetMouse = result;
        

    }

    public void CanDraw()
    {
        _canDraw = CheckCanDraw();
        if (_canDraw)
            _paintObject.SetActive(true);
    }
    private bool CheckCanDraw() => transform.position.y >= _positionTargetMouse.y - 0.05f &&
                                   transform.position.y <= _positionTargetMouse.y + 0.05f; 
    
    private void TryMoveBorderPaintZ(out Vector3 result, Vector3 position)
    {
        var raycastPosition = _raycastTransform.position;
        var raycastDirection = _raycastTransform.forward;
        Physics.Raycast(raycastPosition, raycastDirection, out var borderForwardInfo, _maxDistanceRaycast);
        Debug.DrawRay(raycastPosition, raycastDirection * _maxDistanceRaycast, Color.green);
        result = position;
        result.z = _positionBruchZ;
        _blendShapesBrush = 0f;
        if (borderForwardInfo.collider == null) return;

        _blendShapesBrush = 100f;
        Debug.Log(borderForwardInfo.point);
        var positionBorder = borderForwardInfo.point;
        result.z = positionBorder.z;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(_positionBorder, Vector3.one * 0.3f);
    }
}