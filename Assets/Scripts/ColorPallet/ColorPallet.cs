using UnityEngine;

[RequireComponent(typeof(Renderer))]
[DefaultExecutionOrder(-10)]
public class ColorPallet : MonoBehaviour
{
    private Renderer _renderer;
    private int _index;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>(); 
    }

    public void SetColor(Color color) => _renderer.material.color = color;
    
    public Color GetColor() => _renderer.material.color;

    public void SetSmoothness(float smoothness, Color colorSmoothness)
    {
        const string nameSmoothness = "_Smoothness";
        const string nameSpecColor = "_SpecColor"; 
        _renderer.material.SetFloat(nameSmoothness, smoothness);
        _renderer.material.SetColor(nameSpecColor, colorSmoothness);
    }

    public void SetIndex(int value) => _index = value;
    public int GetIndex() => _index; 
}
