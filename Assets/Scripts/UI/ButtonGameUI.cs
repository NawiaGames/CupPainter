using GameLib.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonGameUI : MonoBehaviour
{
    [SerializeField] private CreateLevel _createLevel;
    [SerializeField] private SettingsBrush _settingsBrush;
    [SerializeField] private BlendColor _blendColor;
    [SerializeField] private RandomColor _randomColor;
    [SerializeField] private SelectedPaintObjects _selectedPaintObjects;
    [SerializeField] private UIPanel _uilngameHUD;
    [SerializeField] private UIPanel _panelSelectedPaintObjects;
    [SerializeField] private GameObject _debugMenu;

    private bool _isOpenDebugMenu;

    public void OnOverloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void OnChangeOpacity(float value) => _settingsBrush.SetOpacity(value);

    public void OnChangeHardness(float value) => _settingsBrush.SetHardness(value);

    public void OnChangeRadius(float value) => _settingsBrush.SetRadius(value);

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

    [ContextMenu("Reset Blend")]
    public void OnResetBlend()
    {
        HapticManager.VibLo(this);
        _blendColor.Reset();
    }

    [ContextMenu("Reset Paint Texture")]
    public void ResetPaintTexture()
    {
        _createLevel.PaintObjects[SelectedPaintObjects.CurrentPaintObjectIndex].P3dPaintableTexture.Clear();
        _createLevel.Save.SetPercentLevel(SelectedPaintObjects.CurrentPaintObjectIndex, 0);
    }

    public void SelectedLevel(int index = 0)
    {
        _selectedPaintObjects.LoadSelectedPaintObject(index);
        _panelSelectedPaintObjects.DeactivatePanel();
        _uilngameHUD.ActivatePanel();
    }

    [ContextMenu("Reset Save")]
    public void DeleteSave() => PlayerPrefs.DeleteAll();


    [ContextMenu("Open selected panel")]
    public void OpenSelectedPanel()
    {
        _uilngameHUD.DeactivatePanel();
        _panelSelectedPaintObjects.ActivatePanel();
        InitializationSelectedPanel();
    }

    private void InitializationSelectedPanel()
    {
        var materialsClones = _createLevel.Save.GetPaintObjectsSelectedObjects();
        var percentLevels = _createLevel.Save.GetPercentLevels();
        var indexNextLevel = 0;

        for (var i = 0; i < materialsClones.Length; i++)
        {
            _createLevel.SelectedSpawnPaintObjects[i].Renderer.material.mainTexture =
                materialsClones[i].Renderer.material.mainTexture;

            if (i == 0 || percentLevels[i] > PanelMatch.BorderNextLevel)
            {
                _createLevel.SelectPaintObjectUI[i].UIPanelUnlock.DeactivatePanel();
                _createLevel.SelectPaintObjectUI[i].Text.enabled = true;
                _createLevel.SelectPaintObjectUI[i].Text.text = percentLevels[i] + "%";
            }
            else
            {
                _createLevel.SelectPaintObjectUI[i].UIPanelUnlock.ActivatePanel();
                _createLevel.SelectPaintObjectUI[i].Text.enabled = false;
            }

            if (percentLevels[i] > PanelMatch.BorderNextLevel)
            {
                if (i + 1 < materialsClones.Length)
                    indexNextLevel = i + 1;
                else
                {
                    indexNextLevel = i;
                }
            }
        }

        _createLevel.SelectPaintObjectUI[indexNextLevel].Text.enabled = true;
        _createLevel.SelectPaintObjectUI[indexNextLevel].Text.text = percentLevels[indexNextLevel] + "%";
        _createLevel.SelectPaintObjectUI[indexNextLevel].UIPanelUnlock.DeactivatePanel();
    }
}