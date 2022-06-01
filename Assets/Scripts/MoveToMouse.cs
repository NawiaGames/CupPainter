using UnityEngine;

public class MoveToMouse : MonoBehaviour
{
    [SerializeField] private float _speed = 12.0f;
    [SerializeField] private float _distanceFromCamera = 2.0f;

    void Update()
    {
        FollowerMouse();
    }

    private void FollowerMouse()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = _distanceFromCamera;

        if (Camera.main == null) return;
        var mouseScreenToWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        var position = Vector3.Lerp(transform.position, mouseScreenToWorld, 1.0f - Mathf.Exp(-_speed * Time.deltaTime));
        transform.position = position;
    }
}
