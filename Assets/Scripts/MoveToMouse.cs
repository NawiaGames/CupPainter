using System;
using UnityEngine;

public class MoveToMouse : MonoBehaviour
{
    [SerializeField] private float _speed = 12.0f;
    [SerializeField] private float _distanceFromCamera = 2.0f;
    [SerializeField] private Camera _camera;
 /*   [Range(0f,1f)]
    [SerializeField] private float _offsetMiddleScreenX = 0.2f; */
    private Vector3 _middleScreen;
    private void Start()
    {
        _middleScreen = GetMiddleScreenToWorld();
    }

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
        
        if (position.x < _middleScreen.x)
        {
            position = new Vector3(_middleScreen.x, position.y, position.z); 
        }
        
        transform.position = position;
    }

    private Vector3 GetMiddleScreenToWorld()
    {
        var middle = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0); 
        middle = _camera.ScreenToWorldPoint(middle);
        return middle; 
    }
}
