using System.Collections;
using UnityEngine;

public class BlendColor : MonoBehaviour
{
    [SerializeField] private Renderer _blendRender;
    [SerializeField] private float _speedBlend = 7f; 
    
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
        
        StopCoroutine(BlendColorTime(_blendColor));
        StartCoroutine(BlendColorTime(_blendColor));
   //     _blendRender.material.color = _blendColor;
        
        return _blendColor; 
    }

    private IEnumerator BlendColorTime(Color color)
    {
        while (_blendRender.material.color != color)
        {
            var currentColor = _blendRender.material.color;
            _blendRender.material.color = Color.Lerp(currentColor, color, Time.deltaTime * _speedBlend);
            yield return null; 
        }
    }

    public void Reset()
    {
        _blendColor = _startColor;
        _blendRender.material.color = _blendColor; 
    }
}