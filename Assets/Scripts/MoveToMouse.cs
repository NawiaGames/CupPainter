using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveToMouse : MonoBehaviour
{
    [SerializeField] private float _speed = 12.0f;
    [SerializeField] private float _distanceFromCamera = 2.0f;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _limitPositionX = 1f;

    void Update()
    {
        FollowerMouse();
    }
    

    private void FollowerMouse()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = _distanceFromCamera;
        if (_camera == null) return;
        var mouseScreenToWorld = _camera.ScreenToWorldPoint(mousePosition);
        var position = Vector3.Lerp(transform.position, mouseScreenToWorld, 1.0f - Mathf.Exp(-_speed * Time.deltaTime));
        
        if (position.x < _limitPositionX)
        {
            position = new Vector3(_limitPositionX, position.y, position.z); 
        }
        
        transform.position = position;
    }

/*    private Vector3 GetMiddleScreenToWorld()
    {
        var middle = new Vector3(_camera.pixelWidth + _offsetMiddleScreenX / 2, _camera.pixelHeight / 2, 0); 
        middle = _camera.ScreenToWorldPoint(middle);
        return middle; 
    }*/
}
