using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonGameUI : MonoBehaviour
{
    [SerializeField] private CreateLevel _createLevel; 
    [SerializeField] private SettingsBrush _settingsBrush;
    [SerializeField] private BlendColor _blendColor; 
    [SerializeField] private RandomColor _randomColor;
    [SerializeField] private Rotation _rotationObject;
    [SerializeField] private SelectedPaintObjects _selectedPaintObjects;
    [SerializeField] private GameObject _panelSelectedPaintObjects; 
    [SerializeField] private GameObject _debugMenu; 
    
    private bool _isOpenDebugMenu;

    public void OnOverloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void OnChangeOpacity(float value) => _settingsBrush.SetOpacity(value);

    public void OnChangeHardness(float value) => _settingsBrush.SetHardness(value);

    public void OnChangeRadius(float value) => _settingsBrush.SetRadius(value); 

    public void OnChangeSpeedRotation(float value) => _rotationObject.SetSpeed(value);

    public void OnAddIndexPaintObject() => _selectedPaintObjects.UpdateIndexToOnePaintObject(1);

    public void OnSubtractIndexPaintObject() => _selectedPaintObjects.UpdateIndexToOnePaintObject(-1);

    public void OnSelectedColor(int index)
    {
        var color = _randomColor.ImageColors[index].color;
        _settingsBrush.SetColor(color);
    }
    
    public void ActivateDebugMenu()
    {
        _isOpenDebugMenu = !_isOpenDebugMenu; 
        _debugMenu.SetActive(_isOpenDebugMenu);
    }

    public void OnResetBlend() => _blendColor.Reset();

    public void ResetPaintTexture() => _createLevel.PaintObjects[SelectedPaintObjects.CurrentPaintObjectIndex]
        .PP3dPaintableTexture.Clear();

    public void SelectedLevel(int index = 0)
    {
        _selectedPaintObjects.LoadSelectedPaintObject(index); 
        _panelSelectedPaintObjects.SetActive(false);
    }
}
