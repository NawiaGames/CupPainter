using UnityEngine;

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
    [Header("Brush")] [SerializeField] private Transform _brushTransform;
    [SerializeField] private SkinnedMeshRenderer _brushSkinnedMeshRenderer;
    [SerializeField] private float _positionBruchZ = -1f;

    private LimitPositionBrush _limitPositionBrush;
    private Vector3 _positionBorder = Vector3.zero;
    private Vector3 _offestMouse = Vector3.zero;
    private Vector3 _rightEdge;
    private Vector3 _upEdge;
    private Vector3 _positionTargetMouse;
    private float _blendShapesBrush;
    private float _currentBlendShapesBruch;
    private bool _isEndBorder;

    private void Start()
    {
        _brushTransform.position =
            new Vector3(_brushTransform.position.x, _brushTransform.position.y, _positionBruchZ);

        SetLimitPosition();

        _positionTargetMouse = _brushTransform.position;
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
        FollowerMouse();
        UpdateBlendShapesBrush();
    }

    public void MoveBrush() => _brushTransform.position =
        Vector3.Lerp(_brushTransform.position, _positionTargetMouse, _speed * Time.deltaTime);

    public void СalculateOffestPositionMouseToBrush() => _offestMouse = _brushTransform.position - transform.position;

    public void CalculateMovePositionBrush()
    {
        _positionTargetMouse = transform.position + _offestMouse;

        _positionTargetMouse = _limitPositionBrush.Limit(_positionTargetMouse);

        if (TryMoveBorderPaintZ(out var result, _positionTargetMouse))
            _positionTargetMouse = result;

    }

    public void ResetBrushButtonUp()
    {
        _positionTargetMouse.z = _positionBruchZ;
        _positionTargetMouse.x = _rightEdge.x - _offestPositionBorder;
        _blendShapesBrush = 0f;
    }

    private void UpdateBlendShapesBrush()
    {
        _currentBlendShapesBruch = Mathf.Lerp(_currentBlendShapesBruch, _blendShapesBrush, _speed / 2 * Time.deltaTime);
        _brushSkinnedMeshRenderer.SetBlendShapeWeight(0, _currentBlendShapesBruch);
    }

    private void FollowerMouse()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = _distanceFromCamera;
        var mouseScreenToWorld = _camera.ViewportToWorldPoint(_camera.ScreenToViewportPoint(mousePosition));
        transform.position = mouseScreenToWorld;
    }

    private bool TryMoveBorderPaintZ(out Vector3 result, Vector3 position)
    {
        var raycastPosition = _raycastTransform.position;
        var raycastDirection = _raycastTransform.forward;
        Physics.Raycast(raycastPosition, raycastDirection, out var borderForwardInfo, _maxDistanceRaycast);
        Debug.DrawRay(raycastPosition, raycastDirection * _maxDistanceRaycast, Color.green);
        result = Vector3.zero;
        _blendShapesBrush = 0f;
        if (borderForwardInfo.collider != true) return false;

        _blendShapesBrush = 100f;
        var positionBorder = borderForwardInfo.point;
        position.z = positionBorder.z + _offestPositionObjectZ;
        result = position;
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(_positionBorder, Vector3.one * 0.3f);
    }
}