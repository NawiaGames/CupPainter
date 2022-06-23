using System.Collections;
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
    private int _sizePixel;
    
    private void Start()
    {
        _sizePixel = CONSTANT.SIZE_PIXEL; 
        _pixelPercent = CONSTANT.PIXEL_PERCENT;
    }

    public void StartCoroutineComparison()
    {
        StopAllCoroutines();
        StartCoroutine("ComparisonPixelDrawing");
    }

    public IEnumerator ComparisonPixelDrawing()
    {
        _comparisonPixel = 0;
        _texture2DDraw = createLevel.Texture2DModelsSample[SelectedPaintObjects.CurrentPaintObjectIndex];
        _texture2DCurrentDraw = PaintTexture.toTexture2D(createLevel
            .PaintObjects[SelectedPaintObjects.CurrentPaintObjectIndex].RenderTexturePaint);

        var value = 5000;

        var colorOne = _texture2DCurrentDraw.GetPixels32();
        var colorTwo = _texture2DDraw.GetPixels32();
        
        for (var i = 0; i < colorOne.Length; i++)
        {
            if ((i + 1) % value == 0)
                yield return null;
            
            if ((colorOne[i].r <= colorTwo[i].r + _accurateBetweenTextures &&
                 colorOne[i].r >= colorTwo[i].r - _accurateBetweenTextures)
                && ((colorOne[i].g <= colorTwo[i].g + _accurateBetweenTextures &&
                     colorOne[i].g >= colorTwo[i].g - _accurateBetweenTextures))
                && (colorOne[i].b <= colorTwo[i].b + _accurateBetweenTextures &&
                    colorOne[i].b >= colorTwo[i].b - _accurateBetweenTextures)
                && (colorOne[i].a <= colorTwo[i].a + _accurateBetweenTextures &&
                    colorOne[i].a >= colorTwo[i].a - _accurateBetweenTextures))
                _comparisonPixel++;
        }
        var result = _comparisonPixel * _pixelPercent;
        
        _comparisonText.gameObject.SetActive(true);
        _comparisonText.text = "Successed: " + result.ToString("F1") + "%";
    }

    public void OnEnableText() => _comparisonText.gameObject.SetActive(false);
}