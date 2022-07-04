using TMPro;
using UnityEngine;

public class BlendColor : MonoBehaviour
{
    [SerializeField] private GameObject _waterCupGameObject;
    [SerializeField] private GameObject _mix;
    [SerializeField] private TextMeshPro _text; 
    [SerializeField] private float _speedBlend = 7f;
    private Renderer _blendRender;

    private Color _startColor; 
    private Color _blendColor;

    
    private void Awake()
    {
        _blendRender = _mix.GetComponent<Renderer>();
    }

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
        BeginMixColors();
        if (_blendColor == _startColor)
            _blendColor = color;
        else
            _blendColor = (_blendColor + color) / 2;
        
        return _blendColor; 
    }

    private void BeginMixColors()
    {
        _text.enabled = false;
        _mix.SetActive(true);
    }

    public void Reset()
    {
        _text.enabled = true;
        _mix.SetActive(false);
        _blendColor = _startColor;
        _blendRender.material.color = _blendColor; 
    }

    public void EnableGameObject(bool state)
    {
        gameObject.SetActive(state);
        _waterCupGameObject.SetActive(state);
    }
}