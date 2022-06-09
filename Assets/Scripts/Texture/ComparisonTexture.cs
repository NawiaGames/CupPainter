using System;
using System.Collections;
using PaintIn3D;
using TMPro;
using UnityEngine;

public class ComparisonTexture : MonoBehaviour
{
    [SerializeField] private TMP_Text _comparisonText;
    [SerializeField] private GenerateTexture _drawingObject;
    [SerializeField] private P3dPaintableTexture _currentDrawingObject;

    private Texture2D _texture2DDraw;
    private Texture2D _texture2DCurrentDraw;
    private int _comparisonPixel;
    private float _pixelPercent;
    

    private void Start()
    {
        _texture2DDraw = toTexture2D(_drawingObject.RenderTexture);
        _texture2DCurrentDraw = new Texture2D(_drawingObject.Resoulution, _drawingObject.Resoulution);
        var sizePixels = _drawingObject.Resoulution * _drawingObject.Resoulution;
        _pixelPercent = 100f/(float)sizePixels; 
    }

 /*   private void Update()
    {
        ComparisonPixelDrawing();
    }*/

    public void ComparisonPixelDrawing()
    {
        _comparisonPixel = 0;
        if (_texture2DCurrentDraw != null) _texture2DCurrentDraw = null;
        _texture2DCurrentDraw = toTexture2D(_currentDrawingObject.Current);
        for (var y = 0; y < _drawingObject.Resoulution; y++)
        {
            for (var x = 0; x < _drawingObject.Resoulution; x++)
            {
                var colorOne = _texture2DCurrentDraw.GetPixel(y, x);
                var colorTwo = _texture2DDraw.GetPixel(y, x);
                
                if ((colorOne.r <= colorTwo.r + 0.1f && colorOne.r >= colorTwo.r - 0.1f)
                    && ( (colorOne.g <= colorTwo.g + 0.1f && colorOne.g >= colorTwo.g - 0.1f))
                    && (colorOne.b <= colorTwo.b + 0.1f && colorOne.b >= colorTwo.b - 0.1f)
                    && (colorOne.a <= colorTwo.a + 0.1f && colorOne.a >= colorTwo.a - 0.1f))
                    _comparisonPixel++;
       /*    if(colorOne == colorTwo)
                    _comparisonPixel++;*/
            }
        }

        var successedPrecent = _comparisonPixel * _pixelPercent;
        _comparisonText.gameObject.SetActive(true); 
        _comparisonText.text = "Successed: " + successedPrecent.ToString("F1") + "%";
    }
    
    Texture2D toTexture2D(RenderTexture rTex)
    {
        var tex = new Texture2D(_drawingObject.Resoulution, _drawingObject.Resoulution, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    public void OnEnableText() => _comparisonText.gameObject.SetActive(false); 

}