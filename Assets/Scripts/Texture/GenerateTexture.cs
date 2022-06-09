using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class GenerateTexture : MonoBehaviour
{
    [SerializeField] private Texture2D _texture2D; 
    private RenderTexture _renderTexture; 
    private const int RESOLUTION = 512;
    private const int DEPTH = 0;
    private Color _colorUp = Color.red;
    private Color _colorMiddle = Color.blue;
    private Color _colorDown = Color.green; 
    public int Resoulution => RESOLUTION;
    public RenderTexture RenderTexture => _renderTexture; 

    private void Awake()
    {
        CreateTexture();
        
        GetComponent<Renderer>().material.mainTexture = _renderTexture;
    }

    private void CreateTexture()
    {
        _texture2D = new Texture2D(RESOLUTION, RESOLUTION);
        SetColorTexture2D();
        
        _renderTexture = new RenderTexture(RESOLUTION, RESOLUTION, DEPTH )
        {
            anisoLevel = 0,
            autoGenerateMips = false
        };

        Graphics.Blit(_texture2D, _renderTexture);
    }

    private void SetColorTexture2D()
    {
        Color currentColor; 
        for (int y = 0; y < RESOLUTION; y++)
        {
            currentColor = GetCurrentColor(y); 
            for (int x = 0; x < RESOLUTION; x++)
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
            < RESOLUTION / 3 => _colorDown,
            > RESOLUTION / 3 and < (2 * RESOLUTION) / 3 => _colorMiddle,
            _ => _colorUp
        };
    }
}
