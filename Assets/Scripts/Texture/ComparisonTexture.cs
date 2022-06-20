using TMPro;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class ComparisonTexture : MonoBehaviour
{
    [SerializeField] private TMP_Text _comparisonText;
    [SerializeField] private CreateLevel createLevel;

    [SerializeField] private Texture2D _texture2DDraw;
    [SerializeField] private Texture2D _texture2DCurrentDraw;
    [SerializeField] private float _accurateBetweenTextures = 0.1f;
    private int _comparisonPixel;
    private float _pixelPercent;

    private const int SIZE = 512;


    private void Start()
    {
        var sizePixels = SIZE * SIZE;
        _pixelPercent = 100f / (float)sizePixels;
    }

    public void ComparisonPixelDrawing()
    {
        _comparisonPixel = 0;
        _texture2DDraw = createLevel.Texture2DModelsSample[SelectedPaintObjects.CurrentPaintObjectIndex];
        _texture2DCurrentDraw = PaintTexture.toTexture2D(createLevel
            .PaintObjects[SelectedPaintObjects.CurrentPaintObjectIndex].RenderTexturePaint);

        for (var y = 0; y < SIZE; y++)
        {
            for (var x = 0; x < SIZE; x++)
            {
                var colorOne = _texture2DCurrentDraw.GetPixel(y, x);
                var colorTwo = _texture2DDraw.GetPixel(y, x);
                if ((colorOne.r <= colorTwo.r + _accurateBetweenTextures &&
                     colorOne.r >= colorTwo.r - _accurateBetweenTextures)
                    && ((colorOne.g <= colorTwo.g + _accurateBetweenTextures &&
                         colorOne.g >= colorTwo.g - _accurateBetweenTextures))
                    && (colorOne.b <= colorTwo.b + _accurateBetweenTextures &&
                        colorOne.b >= colorTwo.b - _accurateBetweenTextures)
                    && (colorOne.a <= colorTwo.a + _accurateBetweenTextures &&
                        colorOne.a >= colorTwo.a - _accurateBetweenTextures))
                    _comparisonPixel++;
            }
        }

        var successedPrecent = _comparisonPixel * _pixelPercent;
        _comparisonText.gameObject.SetActive(true);
        _comparisonText.text = "Successed: " + successedPrecent.ToString("F1") + "%";
    }

    public void OnEnableText() => _comparisonText.gameObject.SetActive(false);
}