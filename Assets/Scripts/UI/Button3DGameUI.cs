using Cinemachine;
using GameLib.UI;
using TMPro;
using UnityEngine;

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

    public void ActivatePanelWinOrLoose(float result)
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
            _textSuccessedLoose.text ="Match: " +  result.ToString("F1") + "%";

        }
    }
    
    private void DeactivatePanelComparison()
    {
        _uiPanelSample.ActivatePanel();
        _cinemachineVirtualCameraMain.enabled = true;
        _isActivateButtonSample = false;
        _exampleTextureDraw.UpdateAnimation();
        _winPanel.DeactivatePanel();
        _loosePanel.DeactivatePanel();
        Invoke("DeactivateButtonNextLevel", 0.2f);
    }

    private void DeactivateButtonNextLevel()
    {
        _buttonNextLevelGameObject.SetActive(false);
    }
}