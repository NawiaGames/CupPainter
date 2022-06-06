using UnityEngine;

public class MoveToMouse : MonoBehaviour
{
    [SerializeField] private float _speed = 12.0f;
    [SerializeField] private float _distanceFromCamera = 2.0f;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _limitPositionX = 1f;
    [SerializeField] private Transform _raycastTransform;
    [SerializeField] private float _maxDistanceRaycast = 0.5f;
    [SerializeField] private float _offestPositionObjectZ = 0.1f; 
    private Vector3 _positionBorder = Vector3.zero;

    private void Update()
    {
        FollowerMouse();
    }

    private void FollowerMouse()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = _distanceFromCamera;
        var mouseScreenToWorld = _camera.ScreenToWorldPoint(mousePosition);
        var position = Vector3.Lerp(transform.position, mouseScreenToWorld, 1.0f - Mathf.Exp(-_speed * Time.deltaTime));

        if (position.x < _limitPositionX)
            position = new Vector3(_limitPositionX, position.y, position.z);

        _positionBorder = MoveBorderPaint();
        if(_positionBorder.z > position.z)
            position = new Vector3(position.x, position.y, _positionBorder.z + _offestPositionObjectZ);

        transform.position = position;
    }

    private Vector3 MoveBorderPaint()
    {
        var raycastPosition = _raycastTransform.position;
        var raycastDirection = _raycastTransform.forward;
        Physics.Raycast(raycastPosition, raycastDirection, out var borderForwardInfo, _maxDistanceRaycast);
        Debug.DrawRay(raycastPosition, raycastDirection * _maxDistanceRaycast, Color.green);

        if (borderForwardInfo.collider != true)
            return new Vector3(0,0, -10);
        

        var positionBorder = borderForwardInfo.point;
        return positionBorder;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(_positionBorder, Vector3.one * 0.3f);
    }
}