using UnityEngine;

public class ColorsPallet : MonoBehaviour
{
    [SerializeField] private ColorPallet[] _colorsPallets;

    public void SetColorsPallet(Color[] _colors)
    {
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
