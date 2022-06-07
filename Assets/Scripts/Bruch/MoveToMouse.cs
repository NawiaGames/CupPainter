using System.Collections;
using PaintIn3D;
using UnityEngine;
using UnityEngine.Serialization;


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
    [SerializeField] private float _positionBruchZ = -1f;
    [SerializeField] private float _speedBrushZ = 6f;

    private Vector3 _positionBorder = Vector3.zero;
    private Vector3 _offestMouse = Vector3.zero;
    private Vector3 _rightEdge;
    private Vector3 _upEdge;
    private Vector3 _positionTargetMouse;
    private bool _isEndBorder;

    private void Start()
    {
        _brushTransform.position =
            new Vector3(_brushTransform.position.x, _brushTransform.position.y, _positionBruchZ);
        _rightEdge = _camera.ViewportToWorldPoint(new Vector3(0, -1, _camera.transform.position.z));
        _upEdge = _camera.ViewportToWorldPoint(new Vector3(0, 1, _camera.transform.position.z));

        _positionTargetMouse = _brushTransform.position;
    }

    private void Update()
    {
        FollowerMouse();
        ControllerMouse();
    }

    private void ControllerMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _offestMouse = _brushTransform.position - transform.position;
            Debug.Log(_offestMouse);
        }

        if (Input.GetMouseButton(0))
        {
            _positionTargetMouse = transform.position + _offestMouse;

            _positionTargetMouse = LimitPosition(_positionTargetMouse);

            if (TryMoveBorderPaintZ(out var result, _positionTargetMouse))
            {
                _positionTargetMouse = result;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _positionTargetMouse.z = _positionBruchZ;
        }

        _brushTransform.position =
            Vector3.Lerp(_brushTransform.position, _positionTargetMouse, _speed * Time.deltaTime);
    }

    private void FollowerMouse()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = _distanceFromCamera;
        var mouseScreenToWorld = _camera.ViewportToWorldPoint(_camera.ScreenToViewportPoint(mousePosition));
        var position = Vector3.Lerp(transform.position, mouseScreenToWorld, 1.0f - Mathf.Exp(-_speed * Time.deltaTime));

        transform.position = position;
    }

    private Vector3 LimitPosition(Vector3 position)
    {
        if (position.x < _limitPositionX)
            position.x = _limitPositionX;

        if (position.x > _rightEdge.x - _offestPositionBorder)
            position.x = _rightEdge.x - _offestPositionBorder;

        if (position.y > _upEdge.y + _offestPositionBorder)
            position.y = _upEdge.y + _offestPositionBorder;

        if (position.y < -_upEdge.y + _offestPositionBorder)
            position.y = -_upEdge.y + _offestPositionBorder;

        return position;
    }

    private bool TryMoveBorderPaintZ(out Vector3 result, Vector3 position)
    {
        var raycastPosition = _raycastTransform.position;
        var raycastDirection = _raycastTransform.forward;
        Physics.Raycast(raycastPosition, raycastDirection, out var borderForwardInfo, _maxDistanceRaycast);
        Debug.DrawRay(raycastPosition, raycastDirection * _maxDistanceRaycast, Color.green);
        
        result = Vector3.zero;
        if (borderForwardInfo.collider != true) return false;

        if (!borderForwardInfo.collider.gameObject.GetComponent<P3dPaintable>()) return false;
        
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