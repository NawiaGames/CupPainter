using PaintIn3D;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBrush : MonoBehaviour
{
    [SerializeField] private ButtonGameUI _buttonGame;
//    [Header("Brush")] [SerializeField] private P3dPaintSphere _paintSphere;
    [SerializeField] private P3dPaintDecal _paintDecal;
    [SerializeField] private P3dPaintDecal _paintDecalSmoothness; 
    [SerializeField] private Material _materialBrush;

    [Header("DefaultSettings")] [SerializeField]
    private Slider _sliderOpacity;

    [SerializeField] private Slider _sliderHardness;
    [SerializeField] private Slider _sliderRadius;
    [SerializeField] private Slider _sliderSpeedRotation;

    public Color ColorBrush => _paintDecal.Color;

    private void Start()
    {
        SetSettings();
    }

    private void SetSettings()
    {
        SetOpacity(_sliderOpacity.value);
        SetHardness(_sliderHardness.value);
        SetRadius(_sliderRadius.value);
        _buttonGame.OnChangeSpeedRotation(_sliderSpeedRotation.value);
    }

    public void SetOpacity(float value)
    {
//        _paintSphere.Opacity = value;
        _paintDecal.Opacity = value;
        _paintDecalSmoothness.Opacity = value; 
    }

    public void SetHardness(float value)
    {
   //     _paintSphere.Hardness = value;
        _paintDecal.Hardness = value;
        _paintDecalSmoothness.Hardness = value;
    }

    public void SetRadius(float value)
    {
    //    _paintSphere.Radius = value;
        _paintDecal.Radius = value;
        _paintDecalSmoothness.Radius = value; 
    }

    public void SetColor(Color color)
    {
    //    _paintSphere.Color = color;
        _materialBrush.color = color;
        _paintDecal.Color = color;
        _paintDecalSmoothness.Color = color; 
    }

    public void SetAngleDecal(int value)
    {
        _paintDecal.Angle = value;
        _paintDecalSmoothness.Angle = value; 
    }

    public void SetOpacityFromSlider()
    {
        SetOpacity(_sliderOpacity.value);
    }
}