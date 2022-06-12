using UnityEngine;
using UnityEngine.UI;

public class RandomColor : MonoBehaviour
{
    [SerializeField] private Image[] _imageColors;
    [SerializeField] private ColorPallet[] _colorPallets;
    [SerializeField] private SettingsBrush _settingsBrush;
    public Image[] ImageColors => _imageColors; 
    private void Awake()
    {
        RandomColorPaintSphere();
    }

    private void RandomColorPaintSphere()
    {
        var countColor = 0; 
        foreach (var imageColor in _imageColors)
        {
            var color = Random.ColorHSV(0, 1, 0.75f, 0.75f, 0.75f, 0.75f);
            imageColor.color = color;
            _colorPallets[countColor].SetColor(color);
            countColor++;
        }
    }
}
