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
    [SerializeField] private ParticleSystem[] _particleSystemWin; 
    private bool _isActivateButtonSample;

    public static float BorderNextLevel = 0;

    private void Awake()
    {
        BorderNextLevel = _borderNextLevel;
    }

    private void Start()
    {
        ActivateButtonUIInGameHud();
    }

    public void ActivateButtonUIInGameHud() => _uiPanelSample.ActivatePanel();
    
    public void OnButtonSample()
    {
        ActivatePanelComparison();
        DeactivateAnimationPallet();
    }

    public void DeactivateAnimationPallet() => _colorsPallet.DeactivateAnimationPallet();

    public void ActivateAnimationPallet() => _colorsPallet.ActivateAnimationPallet(); 

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
        _continuePanel.ActivatePanel();
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

        var index = SelectedPaintObjects.CurrentPaintObjectIndex;
        _save.SetPercentLevel(index, result);
        _save.SetPaintObjectsSelectedObject(index, _comparisonTexture.CreateLevel.PaintObjects[index]);
        ActivatePanelWinOrContinue(result);
    }


    private void ActivatePanelWinOrContinue(float result)
    {
        if (result < _borderNextLevel) return;
        _winPanel.ActivatePanel();
        _continuePanel.DeactivatePanel();
        foreach (var particleSystem in _particleSystemWin)
            particleSystem.Play();
        
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

    public void WinPanelOpenSelected()
    {
        DeactivatePanelComparison();
        _uiPanelSample.DeactivatePanel();
    }
}