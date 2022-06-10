using UnityEngine;

[DefaultExecutionOrder(-10)]
public class GenerateTexture : MonoBehaviour
{
    [SerializeField] private Renderer _rendererPaintSample;
    [SerializeField] private RandomColor _randomColors; 

    [SerializeField]  private Texture2D _texture2D;
    private RenderTexture _renderTexture;
    private const int RESOLUTION = 512;
    private const int DEPTH = 0;
    private Color[] _colors;
    public int Resoulution => RESOLUTION;
    public RenderTexture RenderTexture => _renderTexture;

    private void Start()
    {
        _colors = new Color[_randomColors.ImageColors.Length];
        SetColors();
        
        CreateTexture();
        _rendererPaintSample.material.mainTexture = _renderTexture;
    }

    private void SetColors()
    {
        for (var i = 0; i < _colors.Length; i++)
            _colors[i] = _randomColors.ImageColors[i].color;
    }

    private void CreateTexture()
    {
        _texture2D = new Texture2D(RESOLUTION, RESOLUTION);
        SetColorTexture2D();

        _renderTexture = new RenderTexture(RESOLUTION, RESOLUTION, DEPTH)
        {
            anisoLevel = 0,
            autoGenerateMips = false
        };

        Graphics.Blit(_texture2D, _renderTexture);
    }

    private void SetColorTexture2D()
    {
        for (var y = 0; y < RESOLUTION; y++)
        {
            var currentColor = GetCurrentColor(y);
            for (var x = 0; x < RESOLUTION; x++)
            {
                _texture2D.SetPixel(x, y, currentColor);
            }
        }

        _texture2D.Apply();
    }

    private Color GetCurrentColor(int pixelY)
    {
        return pixelY switch
        {
            < RESOLUTION / 3 => _colors[0],
            > RESOLUTION / 3 and < (2 * RESOLUTION) / 3 => _colors[1],
            _ => _colors[2]
        };
    }
}