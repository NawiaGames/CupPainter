using PaintIn3D;
using UnityEngine;

[RequireComponent(typeof(P3dPaintableTexture))]
public class BlendColor : MonoBehaviour
{
    [SerializeField] private Texture2D _texture2D; 
    private P3dPaintableTexture _pantableTexture;

    private const int SIZE_TEXTURE = 512; 
    private void Start()
    {
        _pantableTexture = GetComponent<P3dPaintableTexture>();
    }

    private void Update()
    {
        _texture2D = PaintTexture.toTexture2D(_pantableTexture.Current, SIZE_TEXTURE);
    }
}
