using UnityEngine;

[RequireComponent(typeof(Renderer))]
[DefaultExecutionOrder(-10)]
public class ColorPallet : MonoBehaviour
{
    private Renderer _renderer; 
    
    private void Awake()
    {
        _renderer = GetComponent<Renderer>(); 
    }

    public void SetColor(Color color) => _renderer.material.color = color;

    public Color GetColor() => _renderer.material.color; 
}
