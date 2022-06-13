using UnityEngine;

public class InputA : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _paintAndRaycastTransform;
    [SerializeField] private float _speedBrush = 24f;
    [SerializeField] private float _distancBrushToObject = 0.1f;
    [SerializeField] private SettingsBrush _settingsBrush;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRendererBrush;
    [SerializeField] private float _speedSkinedBrush = 12f;
    [SerializeField] private Transform _painTransform;
    [SerializeField] private Transform _raycastTransform;
    [SerializeField] private float _maxDistanceRaycast = 4f;

    private Vector3 _positionFollowerY;
    private Vector3 _positionForwardPaint;
    private float _saveOpacityBrush;
    private float _valueSkinnedMeshBrush;

    private void Start()
    {
        _positionFollowerY = _paintAndRaycastTransform.position;
        _positionForwardPaint = _raycastTransform.position;
        _saveOpacityBrush = _settingsBrush.GetOpacity();
    }

    private void Update()
    {
        Controller();
        MoveBrush();
    }

    private void Controller()
    {
        if (Input.GetMouseButton(0))
        {
            FollowerMousePositionY();
            TryMoveBorder();
        }

        if (Input.GetMouseButtonUp(0))
            ResetPositionBrush();
    }

    private void CanDraw()
    {
        if (_paintAndRaycastTransform.position.y >= _positionFollowerY.y - _distancBrushToObject &&
            _paintAndRaycastTransform.position.y <= _positionFollowerY.y + _distancBrushToObject)
        {
            _settingsBrush.SetOpacity(_saveOpacityBrush);
        }
    }

    private void MoveBrush()
    {
        _paintAndRaycastTransform.position = Vector3.Lerp(_paintAndRaycastTransform.position, _positionFollowerY,
            _speedBrush * Time.deltaTime);

        _painTransform.position =
            Vector3.Lerp(_painTransform.position, _positionForwardPaint, _speedBrush * Time.deltaTime);
        
        var currentSkinned = _skinnedMeshRendererBrush.GetBlendShapeWeight(0);
        var result = Mathf.Lerp(currentSkinned, _valueSkinnedMeshBrush, _speedSkinedBrush * Time.deltaTime);
        _skinnedMeshRendererBrush.SetBlendShapeWeight(0, result);
    }

    private void FollowerMousePositionY()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = -_camera.transform.position.z;
        var mousePositionWorldPoint = _camera.ScreenToWorldPoint(mousePosition);

        var position = _paintAndRaycastTransform.position;
        _positionFollowerY = new Vector3(position.x, mousePositionWorldPoint.y,
            position.z);
    }

    private void TryMoveBorder()
    {
        var raycastPosition = _raycastTransform.position;
        var raycastDirection = _raycastTransform.forward;
        Physics.Raycast(raycastPosition, raycastDirection, out var borderForwardInfo, _maxDistanceRaycast);
        Debug.DrawRay(raycastPosition, raycastDirection * _maxDistanceRaycast, Color.green);
        _positionForwardPaint = _raycastTransform.position;
        if (borderForwardInfo.collider == null) return;
        CanDraw();
        _valueSkinnedMeshBrush = 100f;
        _positionForwardPaint = borderForwardInfo.point;
    }

    private void ResetPositionBrush()
    {
        _positionForwardPaint = _raycastTransform.position;
        _valueSkinnedMeshBrush = 0;
        _settingsBrush.SetOpacity(0);
    }
}