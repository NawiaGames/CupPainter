using System.Collections;
using Cinemachine;
using GameLib.UI;
using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class Button3DGameUI : MonoBehaviour
{
    [SerializeField] private ComparisonTexture _comparisonTexture;
    [SerializeField] private ExampleTextureDraw _exampleTextureDraw;
    [SerializeField] private UIPanel _uiPanelSample;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCameraMain;
    [SerializeField] private InputA _inputA; 
    [SerializeField] private float _borderNextLevel = 60f;
    [Header("Win Panel")]
    [SerializeField] private UIPanel _winPanel;
    [Header("Match Panel")]
    [SerializeField] private UIPanel _matchPanel;
    [SerializeField] private UIPanel _continuePanel; 
    [SerializeField] private Slider _sliderProgressSuccessed;
    [SerializeField] private TMP_Text _textSuccessed; 
    [SerializeField] private float _speedSlider = 6f;

    private bool _isActivateButtonSample;

    private void Start()
    {
        _uiPanelSample.ActivatePanel();
    }

    public void OnButtonSample()
    {
        ActivatePanelComparison();
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
        _textSuccessed.text = startProgress.ToString("F1") + "%";
        _sliderProgressSuccessed.value = startProgress; 
        
        while (startProgress != endProgress)
        {
            startProgress = Mathf.MoveTowards(startProgress, endProgress, Time.deltaTime * _speedSlider);
            _sliderProgressSuccessed.value = startProgress; 
            _textSuccessed.text = startProgress.ToString("F1") + "%";
            yield return null; 
        }
        
         ActivatePanelWinOrContinue(endProgress);
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
    }
}