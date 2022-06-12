using UnityEngine;

public class SelectedColor : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private SettingsBrush _settingsBrush;
 
    public void TrySetColorBrush()
    {
        Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition),out var hitInfo);

        if (hitInfo.collider == null || !hitInfo.collider.TryGetComponent(out ColorPallet colorPallet)) return;

        var color = colorPallet.GetColor();
        _settingsBrush.SetColor(color);
    }
}
