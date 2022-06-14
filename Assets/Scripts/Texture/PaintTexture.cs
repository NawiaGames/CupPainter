using UnityEngine;

public class PaintTexture
{
    public static Texture2D toTexture2D(RenderTexture rTex, int size)
    {
        var tex = new Texture2D(size, size, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

}
