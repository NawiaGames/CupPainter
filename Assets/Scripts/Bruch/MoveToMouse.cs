using System;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class MoveToMouse : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [Header("Settings")] [SerializeField] private float _speed = 12.0f;
    [SerializeField] private float _distanceFromCamera = 2.0f;
    [SerializeField] private float _offestPositionObjectZ = 0.1f;
    [SerializeField] private float _offestPositionBorder = 0.1f; 
    [SerializeField] private float _positionBruchZ = -1f;
    [Header("Limit position")] [SerializeField]
    private float _limitPositionX = 1f;
    [Header("Raycast")] [SerializeField] private Transform _raycastTransform;
    [SerializeField] private float _maxDistanceRaycast = 0.5f;
    [FormerlySerializedAs("_bruchElements")] [SerializeField]
    private Transform _bruchTransform;

    private Vector3 _positionBorder = Vector3.zero;
    private Vector3 _offestMouse = Vector3.zero;
    
    private Vector3 _rightEdge;
    private Vector3 _upEdge; 

    private void Start()
    {
        _bruchTransform.position =
            new Vector3(_bruchTransform.position.x, _bruchTransform.position.y, _positionBruchZ);
        _rightEdge = _camera.ViewportToWorldPoint(new Vector3(0, -1, _camera.transform.position.z));
        _upEdge = _camera.ViewportToWorldPoint(new Vector3(0, 1, _camera.transform.position.z));
        Debug.Log(_rightEdge);
        Debug.Log(_upEdge);


        /* float width = _camera.pixelWidth;
         float height = _camera.pixelHeight;
       
         Vector2 bottomLeft = _camera.ScreenToWorldPoint(new Vector2 (0, 0));
         Vector2 bottomRight = _camera.ScreenToWorldPoint(new Vector2 (width, 0));
         Vector2 topLeft = _camera.ScreenToWorldPoint(new Vector2 (0, height));
         Vector2 topRight = _camera.ScreenToWorldPoint(new Vector2 (width, height));*/
  
 /* Debug.Log(bottomLeft);
  Debug.Log(bottomRight);
  Debug.Log(topLeft);
  Debug.Log(topRight);*/



/* var test1 = _camera.ScreenToWorldPoint(new Vector3(0, 1, - _camera.transform.position.z));
 var test2 = _camera.ScreenToWorldPoint(new Vector3(1, 0, - _camera.transform.position.z));*/

// Debug.Log(test1 + " - " + test2);
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
            _offestMouse = _bruchTransform.position - transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            var position = transform.position + _offestMouse;
            position = LimitPosition(position);
            _bruchTransform.position = position;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _bruchTransform.position =
                new Vector3(_bruchTransform.position.x, _bruchTransform.position.y, _positionBruchZ);
        }
    }

    private void FollowerMouse()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = _distanceFromCamera;
        var mouseScreenToWorld = _camera.ScreenToWorldPoint(mousePosition);
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

        _positionBorder = MoveBorderPaint();
        if (position.z < _positionBorder.z)
            position.z = _positionBorder.z + _offestPositionObjectZ;
        
        return position;
    }

    private Vector3 MoveBorderPaint()
    {
        var raycastPosition = _raycastTransform.position;
        var raycastDirection = _raycastTransform.forward;
        Physics.Raycast(raycastPosition, raycastDirection, out var borderForwardInfo, _maxDistanceRaycast);
        Debug.DrawRay(raycastPosition, raycastDirection * _maxDistanceRaycast, Color.green);

        if (borderForwardInfo.collider != true)
            return new Vector3(0, 0, _positionBruchZ);
        
        var positionBorder = borderForwardInfo.point;
        return positionBorder;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(_positionBorder, Vector3.one * 0.3f);
    }
}