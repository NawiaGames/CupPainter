using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MoveToMouse))]
public class ControllerMouse : MonoBehaviour
{
    [SerializeField] private SelectedColor _selectedColor; 
    private MoveToMouse _moveToMouse;

    private void Start()
    {
        _moveToMouse = GetComponent<MoveToMouse>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _moveToMouse.СalculateOffestPositionMouseToBrush();
            _selectedColor.TrySetColorBrush();
        }

        if (Input.GetMouseButton(0))
            _moveToMouse.CalculateMovePositionBrush();

        if (Input.GetMouseButtonUp(0))
            _moveToMouse.ResetBrushButtonUp();

        if (!EventSystem.current.IsPointerOverGameObject())
            _moveToMouse.MoveBrush();
    }
}