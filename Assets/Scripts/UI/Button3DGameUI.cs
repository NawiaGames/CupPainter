using Cinemachine;
using GameLib.UI;
using UnityEngine;

public class Button3DGameUI : MonoBehaviour
{
    [SerializeField] private ComparisonTexture _comparisonTexture;
    [SerializeField] private ExampleTextureDraw _exampleTextureDraw;
    [SerializeField] private UIPanel _uiPanelSample;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCameraMain;
    [SerializeField] private GameObject _buttonNextLevelGameObject;
    [SerializeField] private float _borderNextLevel = 60f; 

    
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

    public void ActivateButtonNextLevel(float result)
    {
           if(result > _borderNextLevel)
              _buttonNextLevelGameObject.SetActive(true);
    }
    
    private void DeactivatePanelComparison()
    {
        _uiPanelSample.ActivatePanel();
        _cinemachineVirtualCameraMain.enabled = true;
        _isActivateButtonSample = false;
        _comparisonTexture.OnEnableText();
        _exampleTextureDraw.UpdateAnimation();
        Invoke("DeactivateButtonNextLevel", 0.2f);
    }

    private void DeactivateButtonNextLevel()
    {
        _buttonNextLevelGameObject.SetActive(false);
    }
}