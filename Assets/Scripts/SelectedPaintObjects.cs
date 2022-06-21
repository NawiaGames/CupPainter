using UnityEngine;
using UnityEngine.Serialization;

public class SelectedPaintObjects : MonoBehaviour
{
    [SerializeField] private CreateLevel _createLevel;
    [SerializeField] private ColorsPallet _colorsPallet;
    [SerializeField] private BlendColor _blendColor;
    [SerializeField] private ExampleTextureDraw _exampleTextureDraw; 
    [SerializeField] private int _currentPaintObject = 0;

    private int _lengthPaintObjects = 0;

    public static int CurrentPaintObjectIndex; 
    
    private void Start()
    {
        BeginLevel();
    }

    private void BeginLevel()
    {
        _lengthPaintObjects = _createLevel.PaintObjects.Length;
        CurrentPaintObjectIndex = _currentPaintObject;
        ActivateSelectedObject();
        
        SetSettingsLevel();
    }

    private void ActivateSelectedObject()
    {
        for (var i = 0; i < _lengthPaintObjects; i++)
        {
            _createLevel.PaintObjects[i].gameObject.SetActive(_currentPaintObject == i);
            if (_currentPaintObject == i)
            {
                var height = _createLevel.PaintObjects[i].MeshCollider.bounds.size.y;
                Debug.Log(height);
                _exampleTextureDraw.SetHeight(height);
            }
            _createLevel.SmallPaintSampleObjects[i].SetActive(_currentPaintObject == i);
    //        createLevel.BigPaintSampleObjects[i].SetActive(_currentPaintObject == i);
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
        
        SetSettingsLevel();
    }

    private void SetSettingsLevel()
    {
        _colorsPallet.SetColorsPallet(_createLevel.ColorsPallet[_currentPaintObject].ColorsPallet);
        _blendColor.gameObject.SetActive(_createLevel.CanActivatePallets[_currentPaintObject]);
        _exampleTextureDraw.SetTexture(_createLevel.Texture2DModelsSample[_currentPaintObject]);
    }
}