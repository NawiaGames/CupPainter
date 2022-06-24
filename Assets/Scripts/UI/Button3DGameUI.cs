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
    [SerializeField] private GameObject _buttonNextLevelGameObject;
    [SerializeField] private float _borderNextLevel = 60f;
    [SerializeField] private UIPanel _winPanel;
    [SerializeField] private UIPanel _loosePanel;
    [SerializeField] private TMP_Text _textSuccessedWin;
    [SerializeField] private TMP_Text _textSuccessedLoose;
    [Header("Match Panel")]
    [SerializeField] private UIPanel _matchPanel; 
    [SerializeField] private Slider _sliderProgressSuccessed;
    [SerializeField] private TMP_Text _textSuccessed; 
    [SerializeField] private float _speedSlider = 6f;

    private bool _isActivateButtonSample;

    private void Start()
    {
        _uiPanelSample.ActivatePanel();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0) || !_isActivateButtonSample) return;
        DeactivatePanelComparison();
    }

    public void OnButtonSample()
    {
        ActivatePanelComparison();
    }

    private void ActivatePanelComparison()
    {
        _uiPanelSample.DeactivatePanel();
        _cinemachineVirtualCameraMain.enabled = false;
        _isActivateButtonSample = true;
        _exampleTextureDraw.UpdateAnimation();
        _comparisonTexture.StartCoroutineComparison();
    }

    public void ActivateProgressSlider(float result)
    {
        _matchPanel.ActivatePanel();
        StopAllCoroutines();
        StartCoroutine(MoveProgressSuccessed(result));
    }

    private IEnumerator MoveProgressSuccessed(float endProgress)
    {
        float startProgress = 0; 
        _textSuccessed.text = startProgress.ToString("F1") + "%";
        
        while (startProgress != endProgress)
        {
            startProgress = Mathf.MoveTowards(startProgress, endProgress, Time.deltaTime * _speedSlider);
            _sliderProgressSuccessed.value = startProgress; 
            _textSuccessed.text = startProgress.ToString("F1") + "%";
            yield return null; 
        }
        
   //     ActivatePanelWinOrLose(endProgress);
    }
    

    private void ActivatePanelWinOrLose(float result)
    {
        if (result > _borderNextLevel)
        {
            _buttonNextLevelGameObject.SetActive(true);
            _winPanel.ActivatePanel();
            _textSuccessedWin.text = "Match: " + result.ToString("F1") + "%";
        }
        else
        {
            _loosePanel.ActivatePanel();
            _textSuccessedLoose.text = "Match: " + result.ToString("F1") + "%";
        }
    }

    private void DeactivatePanelComparison()
    {
        _uiPanelSample.ActivatePanel();
        _cinemachineVirtualCameraMain.enabled = true;
        _isActivateButtonSample = false;
        _exampleTextureDraw.UpdateAnimation();
        
      //  _winPanel.DeactivatePanel();
     //   _loosePanel.DeactivatePanel();
        _matchPanel.DeactivatePanel();
        Invoke("DeactivateButtonNextLevel", 0.2f);
    }

    private void DeactivateButtonNextLevel()
    {
        _buttonNextLevelGameObject.SetActive(false);
    }
}