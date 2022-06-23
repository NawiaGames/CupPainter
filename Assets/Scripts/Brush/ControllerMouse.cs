using UnityEngine;

//Don't work
public class ControllerMouse : MonoBehaviour
{
    [SerializeField] private SelectedColor _selectedColor;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _selectedColor.CameraToRaycast();
        }
    }
}