using UnityEngine;

public class BlendColor : MonoBehaviour
{
    [SerializeField] private Renderer _blendRender;
    [SerializeField] private float _speedBlend = 7f; 
    
    private Color _startColor; 
    private Color _blendColor;

    private void Start()
    {
        _startColor = _blendRender.material.color;
        _blendColor = _startColor;
    }

    private void Update()
    {
        if (_blendRender.material.color != _blendColor)
            _blendRender.material.color = Color.Lerp(_blendRender.material.color, _blendColor, Time.deltaTime * _speedBlend);
    }

    public Color GetColorBlend(Color color)
    {
        if (_blendColor == _startColor)
            _blendColor = color;
        else
            _blendColor = (_blendColor + color) / 2;
        
        return _blendColor; 
    }

    public void Reset()
    {
        _blendColor = _startColor;
        _blendRender.material.color = _blendColor; 
    }
}