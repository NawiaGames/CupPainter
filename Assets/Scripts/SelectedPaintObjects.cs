using UnityEngine;
using UnityEngine.Serialization;

public class SelectedPaintObjects : MonoBehaviour
{
    [FormerlySerializedAs("_createPaintObjects")] [SerializeField] private CreateLevel createLevel;
    [SerializeField] private ColorsPallet _colorsPallet; 
    [SerializeField] private int _currentPaintObject = 0;

    private int _lengthPaintObjects = 0;

    public static int CurrentPaintObjectIndex; 
    
    private void Start()
    {
        _lengthPaintObjects = createLevel.PaintObjects.Length;
        CurrentPaintObjectIndex = _currentPaintObject; 
        ActivateSelectedObject();
        _colorsPallet.SetColorsPallet(createLevel.ColorsPallet[_currentPaintObject].ColorsPallet);
    }

    private void ActivateSelectedObject()
    {
        for (var i = 0; i < _lengthPaintObjects; i++)
        {
            createLevel.PaintObjects[i].gameObject.SetActive(_currentPaintObject == i);
            createLevel.SmallPaintSampleObjects[i].SetActive(_currentPaintObject == i);
            createLevel.BigPaintSampleObjects[i].SetActive(_currentPaintObject == i);
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
        _colorsPallet.SetColorsPallet(createLevel.ColorsPallet[_currentPaintObject].ColorsPallet);
    }
}