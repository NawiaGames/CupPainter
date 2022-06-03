using PaintIn3D;
using UnityEngine;

public class OnCollisionPaint : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        P3dPaintable paintable = collision.gameObject.GetComponent<P3dPaintable>();
        if (paintable)
        {
            var position = collision.contacts[0].point;
            position.z = 5; 
            Debug.Log(collision.gameObject.name);
            transform.position = position; 
        }
    }
}
