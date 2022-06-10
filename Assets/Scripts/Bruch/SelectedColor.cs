using PaintIn3D;
using UnityEngine;

public class SelectedColor : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private P3dPaintSphere _paintSphere;
    [SerializeField] private Material _materialBrush;
    public void TrySetColorBrush()
    {
        Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition),out var hitInfo);

        if (hitInfo.collider == null || !hitInfo.collider.TryGetComponent(out ColorPallet colorPallet)) return;

        var color = colorPallet.GetColor();
        _paintSphere.Color = color;
        _materialBrush.color = color; 
    }
}
