using UnityEngine;

public class SelectedColor : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private SettingsBrush _settingsBrush;
    [SerializeField] private Frames _frames; 
 
    public void CameraToRaycast()
    {
        Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition),out var hitInfo);

        if (hitInfo.collider == null) return;

        if(hitInfo.collider.TryGetComponent(out ColorPallet colorPallet))
            GetColorPallets(colorPallet);

        if (hitInfo.collider.TryGetComponent(out BlendColor blendColor))
            GetColorBlend(blendColor);
    }

    private void GetColorBlend(BlendColor blendColor)
    {
        var colorBrush = _settingsBrush.ColorBrush;
        var colorBlend = blendColor.GetColorBlend(colorBrush);
        _settingsBrush.SetColor(colorBlend);
        _frames.ActivateMixFrame();
    }

    private void GetColorPallets(ColorPallet colorPallet)
    {
        var color = colorPallet.GetColor();
        _settingsBrush.SetColor(color);
        var index = colorPallet.GetIndex();
        _frames.ActivateColorFrame(index);
    }
}
