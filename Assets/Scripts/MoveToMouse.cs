using System;
using PaintIn3D;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveToMouse : MonoBehaviour
{
    [SerializeField] private float _speed = 12.0f;
    [SerializeField] private float _distanceFromCamera = 2.0f;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _limitPositionX = 1f;
    [SerializeField] private Transform _transformPaint;
    [SerializeField] private float _maxDistanceRaycast = 0.5f;

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
  // var position = mouseScreenToWorld; 
   
        if (position.x < _limitPositionX)
            position = new Vector3(_limitPositionX, position.y, position.z);
        Vector3 cup ;
        
        if (MoveBorderPaint(out cup)) // && mouseScreenToWorld.x < transform.position.x)
        {
       //     position = cup - transform.forward * 1.5f;
       position = new Vector3(Mathf.Max(test.x, position.x) ,position.y, position.z);
            // position = test; 
        }
        position = new Vector3(Mathf.Max(test.x, position.x) ,position.y, position.z);
        //if(position.x < test.x ) return;

        Debug.Log("X: " + position.x +" test x: " + test.x);
        transform.position = position;
        
    }


 


    private void OnDrawGizmos()
    {
    //    Gizmos.DrawWireSphere(_transformPaint.position, 0.5f);
        Gizmos.color = Color.black;
        Gizmos.DrawCube(test,Vector3.one * 0.3f);
    }

    private Vector3 test = Vector3.zero; 
    private bool MoveBorderPaint(out Vector3 cup)
    {
        Physics.Raycast(_transformPaint.position, _transformPaint.forward, out var borderForwardInfo, _maxDistanceRaycast);
       Debug.DrawRay(_transformPaint.position, _transformPaint.forward * _maxDistanceRaycast, Color.green);
  //      Physics.SphereCast(_transformPaint.position, 0.5f, _transformPaint.forward, out var borderForwardInfo,
     //       _maxDistanceRaycast);
        
        if (borderForwardInfo.collider != true)
        {
          //  Debug.Log("sdasd");
            cup = Vector3.zero;
            return false;
        }

        cup = borderForwardInfo.point;
        test = cup;
        var paintable = borderForwardInfo.collider.GetComponent<P3dPaintable>();
    //    Debug.Log(borderForwardInfo.collider.gameObject.name);
        return paintable;
  
    }
    
  /*  private void MoveBorderPaint()
    {
        Physics.Raycast(_transformPaint.position, Vector3.forward, out var borderForwardInfo, _maxDistanceRaycast);
        
        if (borderForwardInfo.collider != true) return;
        
        var paintable = borderForwardInfo.collider.GetComponent<P3dPaintable>();
        if (!paintable) return;
  //      var closetPoint = borderForwardInfo.collider.ClosestPointOnBounds(_transformPaint.position); 
        Debug.Log(borderForwardInfo.collider.gameObject.name);
        transform.position = new Vector3(closetPoint.x, closetPoint.y, transform.position.z);
    }*/
}
