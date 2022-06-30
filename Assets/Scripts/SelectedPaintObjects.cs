using UnityEngine;

public class SelectedPaintObjects : MonoBehaviour
{
    [SerializeField] private CreateLevel _createLevel;
    [SerializeField] private ColorsPallet _colorsPallet;
    [SerializeField] private BlendColor _blendColor;
    [SerializeField] private ExampleTextureDraw _exampleTextureDraw;
    [SerializeField] private PanelMatch _panelMatch;
    [SerializeField] private int _currentPaintObject = 0;

    [Header("Change materials")] [SerializeField]
    private Renderer _rendererColorMatch; 

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
            var isPaintObject = _currentPaintObject == i;
            
            _createLevel.PaintObjects[i].gameObject.SetActive(isPaintObject);
            if (isPaintObject)
            {
                var height = _createLevel.PaintObjects[i].MeshCollider.bounds.size.y;
                _exampleTextureDraw.SetHeight(height);
            }
            _createLevel.SmallPaintSampleObjects[i].SetActive(isPaintObject);
        }
    }

    public void UpdateIndexToOnePaintObject(int value)
    {
        var index = _currentPaintObject + value;

        if (0 > index)
            index = _lengthPaintObjects - 1;
        else if (_lengthPaintObjects - 1 < index)
            index = 0;

        LoadSelectedPaintObject(index);
    }

    public void LoadSelectedPaintObject(int index)
    {
        CurrentPaintObjectIndex = index; 
        _currentPaintObject = index;
        ActivateSelectedObject();
        
        SetSettingsLevel();
    }

    private void SetSettingsLevel()
    {
        _panelMatch.ActivateButtonUIInGameHud();
        _colorsPallet.ActivateAnimationPallet();
        _colorsPallet.SetColorsPallet(_createLevel.ColorsPallet[_currentPaintObject].ColorsPallet);
        _blendColor.EnableGameObject(_createLevel.CanActivatePallets[_currentPaintObject]);
        _exampleTextureDraw.SetTexture(_createLevel.Texture2DModelsSample[_currentPaintObject]);
        
        var colorSmoothness = _createLevel.PaintObjects[_currentPaintObject].ColorSmoothness;
        var smoothness = _createLevel.PaintObjects[_currentPaintObject].Smoothness;
        _colorsPallet.SettingsBrush.SetColorSmoothness(smoothness, colorSmoothness);
        UpdateMaterialColorMatch(smoothness, colorSmoothness);
        _colorsPallet.SetSmoothness(smoothness, colorSmoothness);
    }

    private void UpdateMaterialColorMatch(float smoothness, Color colorSmoothness)
    {
        const string nameSmoothness = "_Smoothness";
        const string nameSpecColor = "_SpecColor"; 
        _rendererColorMatch.material.SetFloat(nameSmoothness, smoothness);
        _rendererColorMatch.material.SetColor(nameSpecColor, colorSmoothness);
        
    }
}