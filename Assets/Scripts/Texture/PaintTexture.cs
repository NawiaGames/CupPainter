using UnityEngine;

public class PaintTexture
{
    public static Texture2D toTexture2D(RenderTexture rTex)
    {
        var tex = new Texture2D(CONSTANT.SIZE_PIXEL, CONSTANT.SIZE_PIXEL, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}
