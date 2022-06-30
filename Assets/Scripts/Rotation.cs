using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationSpeed;

    private void Update()
    {
        Rotate();
    }

    private void Rotate() => transform.Rotate(_rotationSpeed * Time.deltaTime, Space.World);

//    public void SetSpeed(float value) => _rotationSpeed.y = value; 
}