using System.Collections;
using Cinemachine;
using GameLib.UI;
using UnityEngine;

public class Button3DGameUI : MonoBehaviour
{
    [SerializeField] private ComparisonTexture _comparisonTexture;
    [SerializeField] private ExampleTextureDraw _exampleTextureDraw;
    [SerializeField] private UIPanel _uiPanelSample;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCameraMain;
    
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
        _comparisonTexture.ComparisonPixelDrawing();
        _exampleTextureDraw.UpdateAnimation();
    }
    
    private void DeactivatePanelComparison()
    {
        _uiPanelSample.ActivatePanel();
        _cinemachineVirtualCameraMain.enabled = true;
        _isActivateButtonSample = false;
        _comparisonTexture.OnEnableText();
        _exampleTextureDraw.UpdateAnimation();
    }
}