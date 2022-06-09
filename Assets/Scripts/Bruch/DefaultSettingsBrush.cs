using UnityEngine;
using UnityEngine.UI;

public class DefaultSettingsBrush : MonoBehaviour
{
    [SerializeField] private ButtonGameUI _buttonGame;
    [Header("DefaultSettings")]
    [SerializeField] private Slider _sliderOpacity;
    [SerializeField] private Slider _sliderHardness;
    [SerializeField] private Slider _sliderRadius;
    [SerializeField] private Slider _sliderSpeedRotation;
    [SerializeField] private RandomColor _randomColor;
    private void Start()
    {
        SetSettings();
    }

    private void SetSettings()
    {
        _buttonGame.OnChangeOpacity(_sliderOpacity.value);
        _buttonGame.OnChangeHardness(_sliderHardness.value);
        _buttonGame.OnChangeRadius(_sliderRadius.value);
        _buttonGame.OnChangeSpeedRotation(_sliderSpeedRotation.value);
        _randomColor.OnSelectedColor(0);
    }
}
