using PaintIn3D;
using UnityEngine;
using UnityEngine.UI;

public class RandomColor : MonoBehaviour
{
    [SerializeField] private Image _imageColor; 
    [SerializeField] private P3dPaintSphere _p3dPaintSphere; 
    private Color _color;

    private void Start()
    {
        RandomColorPaintSphere();
    }

    private void RandomColorPaintSphere()
    {
        _color = Random.ColorHSV(0, 1, 0.75f, 0.75f, 0.75f, 0.75f);
        _imageColor.color = _color; 
    }

    public void OnUpdateColor()
    {
        _p3dPaintSphere.Color = _color; 
        RandomColorPaintSphere();
    }
}
