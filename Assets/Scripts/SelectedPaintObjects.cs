using UnityEngine;

public class SelectedPaintObjects : MonoBehaviour
{
    [SerializeField] private CreatePaintObjects _createPaintObjects;
    [SerializeField] private ColorsPallet _colorsPallet; 
    [SerializeField] private int _currentPaintObject = 0;

    private int _lengthPaintObjects = 0;

    public static int CurrentPaintObjectIndex; 
    
    private void Start()
    {
        _lengthPaintObjects = _createPaintObjects.PaintObjects.Length;
        CurrentPaintObjectIndex = _currentPaintObject; 
        ActivateSelectedObject();
        _colorsPallet.SetColorsPallet(_createPaintObjects.ColorsPallet[_currentPaintObject].ColorsPallet);
    }

    private void ActivateSelectedObject()
    {
        for (var i = 0; i < _lengthPaintObjects; i++)
        {
            _createPaintObjects.PaintObjects[i].gameObject.SetActive(_currentPaintObject == i);
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
        
        CurrentPaintObjectIndex = index; 
        _currentPaintObject = index;
        ActivateSelectedObject();
        _colorsPallet.SetColorsPallet(_createPaintObjects.ColorsPallet[_currentPaintObject].ColorsPallet);
    }
}