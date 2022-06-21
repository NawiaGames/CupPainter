using System;
using UnityEngine;

public class ExampleTextureDraw : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private float _leftX = -3.8f;
    [SerializeField] private float _rightX = -0.8f;
    [SerializeField] private float _speedMove = 7f;

    private Vector3 _direction;
    private Transform _myTransform;
    private bool _changeDirection; 

    private void Start()
    {
        _myTransform = gameObject.transform;
        _direction = _myTransform.position;
        _direction.x = _leftX; 
    }

    private void Update()
    {
        // if (_direction != _myTransform.position)
        //     _myTransform.position = Vector3.MoveTowards(_myTransform.position, _direction, _speedMove * Time.deltaTime); 
    }

    public void SetTexture(Texture2D texture2D) => _meshRenderer.material.mainTexture = texture2D;

    public void SetHeight(float height)
    {
        var localScale = _meshRenderer.gameObject.transform.localScale;
        _meshRenderer.gameObject.transform.localScale = new Vector3(localScale.x, height,localScale.z);
    }

    public void UpdateDirection()
    {
        _changeDirection = !_changeDirection;
        _direction.x = _changeDirection ? _rightX : _leftX;
    }
}
