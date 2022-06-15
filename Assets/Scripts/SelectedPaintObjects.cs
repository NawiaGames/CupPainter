using UnityEngine;

public class SelectedPaintObjects : MonoBehaviour
{
    [SerializeField] private CreatePaintObjects _createPaintObjects;
    [SerializeField] private int _currentPaintObject = 0;

    private int _lengthPaintObjects = 0;

    private void Start()
    {
        _lengthPaintObjects = _createPaintObjects.PaintObjects.Length;
        ActivateSelectedObject();
    }

    private void ActivateSelectedObject()
    {
        for (var i = 0; i < _lengthPaintObjects; i++)
        {
            _createPaintObjects.PaintObjects[i].SetActive(_currentPaintObject == i);
            _createPaintObjects.SmallPaintSampleObjects[i].SetActive(_currentPaintObject == i);
            _createPaintObjects.BigPaintSampleObjects[i].SetActive(_currentPaintObject == i);
        }
    }

    public void UpdateIndexToOnePaintObject(int value)
    {
        var index = _currentPaintObject + value;

        if (0 > index)
            index = _lengthPaintObjects - 1;
        else if (_lengthPaintObjects - 1 < index)
            index = 0;

        _currentPaintObject = index;
        ActivateSelectedObject();
    }
}