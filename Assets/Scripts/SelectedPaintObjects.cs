using UnityEngine;

public class SelectedPaintObjects : MonoBehaviour
{
    [SerializeField] private int _currentPaintObject = 0;
    [SerializeField] private GameObject[] _paintObjects;

    private void Start()
    {
        ActivateSelectedObject();
    }

    private void ActivateSelectedObject()
    {
        for (var i = 0; i < _paintObjects.Length; i++)
            _paintObjects[i].gameObject.SetActive(_currentPaintObject == i);
    }

    public void UpdateIndexToOnePaintObject(int value)
    {
        var index = _currentPaintObject + value;

        if (0 > index)
            index = _paintObjects.Length - 1;
        else if (_paintObjects.Length - 1 < index)
            index = 0;

        _currentPaintObject = index; 
        ActivateSelectedObject();
    }
}
