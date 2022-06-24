using UnityEngine;

public class ColorsPallet : MonoBehaviour
{
    [SerializeField] private ColorPallet[] _colorsPallets;
    [SerializeField] private SettingsBrush _settingsBrush; 
    
    public void SetColorsPallet(Color[] _colors)
    {
        _settingsBrush.SetColor(_colors[0]);
        
        for (var i = 0; i < _colorsPallets.Length; i++)
        {
            if (i < _colors.Length)
            {
                _colorsPallets[i].gameObject.SetActive(true);
                _colorsPallets[i].SetColor(_colors[i]);
            }
            else
            {
                _colorsPallets[i].gameObject.SetActive(false);
            }
                
        }
    }
}
