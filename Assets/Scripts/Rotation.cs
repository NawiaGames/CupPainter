using System;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationSpeed;
    [SerializeField] private int _changeSpeedRotation = 10;
    [SerializeField] private ButtonGameUI _eventButtonGameUI;

    private void Start()
    {
        _eventButtonGameUI.OnUpdateSpeedRotationEvent += OnUpdateSpeed;
    }

    private void OnEnable()
    {
        _eventButtonGameUI.OnUpdateSpeedRotationEvent -= OnUpdateSpeed;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(_rotationSpeed * Time.deltaTime, Space.World);
    }

    private void OnUpdateSpeed(bool addSpeed)
    {
        if (addSpeed)
            _rotationSpeed = new Vector3(_rotationSpeed.x, _rotationSpeed.y + _changeSpeedRotation, _rotationSpeed.z);
        else
        {
            if(_rotationSpeed.y > 0 )
                _rotationSpeed = new Vector3(_rotationSpeed.x, _rotationSpeed.y - _changeSpeedRotation, _rotationSpeed.z);
        }

    }
}