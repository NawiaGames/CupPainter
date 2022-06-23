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

    [SerializeField] private Material _blitCheckerMaterial; 
    
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
    //    ComparisonPixelDrawing3();
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

    public void ComparisonPixelDrawing3()
    {
        _comparisonPixel = 0;
        _texture2DDraw = createLevel.Texture2DModelsSample[SelectedPaintObjects.CurrentPaintObjectIndex];
        _texture2DCurrentDraw = PaintTexture.toTexture2D(createLevel
            .PaintObjects[SelectedPaintObjects.CurrentPaintObjectIndex].RenderTexturePaint);
        
        var colorOne = _texture2DCurrentDraw.GetPixels32();
        var colorTwo = _texture2DDraw.GetPixels32();
        
        for (var i = 0; i < colorOne.Length; i++)
        {
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
        Debug.Log("Successed: " + result.ToString("F1") + "%");
    }
    
    public void ComparisonPixelDrawing2()
    {
        _texture2DDraw = createLevel.Texture2DModelsSample[SelectedPaintObjects.CurrentPaintObjectIndex];
        _texture2DCurrentDraw = PaintTexture.toTexture2D(createLevel
            .PaintObjects[SelectedPaintObjects.CurrentPaintObjectIndex].RenderTexturePaint);

        _blitCheckerMaterial.SetTexture("_SourceTex", _texture2DDraw);
        var newTex = new Texture2D(1, 1);
        var myRT = new RenderTexture(1, 1, 24);
        Graphics.Blit(_texture2DCurrentDraw, myRT, _blitCheckerMaterial);
        RenderTexture.active = myRT; 
        newTex.ReadPixels(new Rect(0,0,1,1), 0, 0);
        myRT.Release();

        var px = newTex.GetPixel(1, 1);
        Debug.Log(px);
        Debug.Log("match " + (100-(px.r + px.g + px.b)/3*100) + " %");
        
    }

    public void OnEnableText() => _comparisonText.gameObject.SetActive(false);
}