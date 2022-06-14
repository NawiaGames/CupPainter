using UnityEngine;

public class BlendColor : MonoBehaviour
{
    [SerializeField] private Renderer _blendRender;

    private Texture2D _texture2D;
    private Color _startColor; 
    private Color _blendColor;
    private Color _brushColor;

    private void Start()
    {
        _startColor = _blendRender.material.color;
    }

    public Color GetColorBlend(Color color)
    {
        Debug.Log(" I am work");
        if (_blendColor == _startColor)
            _blendColor = color;
        else
            _blendColor = (_blendColor + color) / 2;
        
        _blendRender.material.color = _blendColor;
        
        return _blendColor; 
    }

    public void Reset()
    {
        _blendColor = _startColor;
        _blendRender.material.color = _blendColor; 
    }
}