using UnityEngine;

public class Colors
{
    private Color[] _colorsPallet;

    public Color[] ColorsPallet => _colorsPallet; 

    public Colors(Color[] colors)
    {
        var size = colors.Length;
        _colorsPallet = new Color[size];
        
        for (var i = 0; i < size; i++)
            _colorsPallet[i] = colors[i];
    }
}