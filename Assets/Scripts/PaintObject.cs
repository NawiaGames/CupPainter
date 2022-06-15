using PaintIn3D;
using UnityEngine;

public class PaintObject : MonoBehaviour
{
    [SerializeField] private P3dPaintableTexture _p3dPaintableTexture;

    public RenderTexture RenderTexturePaint => _p3dPaintableTexture.Current; 
}
