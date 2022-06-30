using System.Collections;
using Cinemachine;
using GameLib.UI;
using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class PanelMatch : MonoBehaviour
{
    [SerializeField] private ComparisonTexture _comparisonTexture;
    [SerializeField] private ExampleTextureDraw _exampleTextureDraw;
    [SerializeField] private UIPanel _uiPanelSample;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCameraMain;
    [SerializeField] private InputA _inputA;
    [Header("Level parameters")]
    [SerializeField] private float _borderNextLevel = 60f;
    [SerializeField] private float _addPercentForLevel = 1.05f; 
    [Header("Win Panel")] [SerializeField] private UIPanel _winPanel;
    
    [Header("Match Panel")] [SerializeField]
    private UIPanel _matchPanel;

    [SerializeField] private UIPanel _continuePanel;
    [SerializeField] private Slider _sliderProgressSuccessed;
    [SerializeField] private TMP_Text _textSuccessed;
    [SerializeField] private float _speedSlider = 6f;
    [SerializeField] private Save _save;
    [SerializeField] private ColorsPallet _colorsPallet; 
    private bool _isActivateButtonSample;

    private void Start()
    {
        ActivateButtonUIInGameHud();
    }

    public void ActivateButtonUIInGameHud() => _uiPanelSample.ActivatePanel();
    
    public void OnButtonSample()
    {
        ActivatePanelComparison();
        _colorsPallet.DeactivateAnimationPallet();
    }

    private void ActivatePanelComparison()
    {
        _inputA.SetIsUIMenu(true);
        _uiPanelSample.DeactivatePanel();
        _cinemachineVirtualCameraMain.enabled = false;
        _isActivateButtonSample = true;
        _exampleTextureDraw.SetStateAnimation(true);
        _comparisonTexture.StartCoroutineComparison();
    }

    public void ActivateProgressSlider(float result)
    {
        _matchPanel.ActivatePanel();
        StartCoroutine(MoveProgressSuccessed(result));
    }

    private IEnumerator MoveProgressSuccessed(float endProgress)
    {
        float startProgress = 0;
        _textSuccessed.text = startProgress.ToString("F0") + "%";
        _sliderProgressSuccessed.value = startProgress;
        endProgress = Mathf.Min(endProgress * _addPercentForLevel, 100); 
        var result = 0;
        while (startProgress != endProgress)
        {
            startProgress = Mathf.MoveTowards(startProgress, endProgress, Time.deltaTime * _speedSlider);
            result = Mathf.RoundToInt(startProgress);
            _sliderProgressSuccessed.value = result;
            _textSuccessed.text = result.ToString("F0") + "%";
            yield return null;
        }
        _save.SetPercentLevel(SelectedPaintObjects.CurrentPaintObjectIndex, result);
        ActivatePanelWinOrContinue(result);
    }


    private void ActivatePanelWinOrContinue(float result)
    {
        if (result > _borderNextLevel)
            _winPanel.ActivatePanel();
        else
            _continuePanel.ActivatePanel();
    }

    public void DeactivatePanelComparison()
    {
        _inputA.SetIsUIMenu(false);
        _uiPanelSample.ActivatePanel();
        _cinemachineVirtualCameraMain.enabled = true;
        _isActivateButtonSample = false;
        _exampleTextureDraw.SetStateAnimation(false);
        _comparisonTexture.StopAllCoroutinesComparison();
        StopAllCoroutines();
        _matchPanel.DeactivatePanel();
        _colorsPallet.ActivateNXLAnimationPallet();
    }
}