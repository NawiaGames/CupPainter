using PaintIn3D;
using UnityEngine;

public class PaintObject : MonoBehaviour
{
    [SerializeField] private P3dPaintableTexture _p3dPaintableTexture;
    private MeshCollider _meshCollider; 
    public RenderTexture RenderTexturePaint => _p3dPaintableTexture.Current;
    public MeshCollider MeshCollider => _meshCollider;

    private void Awake()
    {
        _meshCollider = _p3dPaintableTexture.gameObject.GetComponent<MeshCollider>(); 
    }
}
