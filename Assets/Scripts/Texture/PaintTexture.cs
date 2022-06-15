using UnityEngine;

public class PaintTexture
{
    private const int SIZE = 512; 
    public static Texture2D toTexture2D(RenderTexture rTex)
    {
        var tex = new Texture2D(SIZE, SIZE, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

}
