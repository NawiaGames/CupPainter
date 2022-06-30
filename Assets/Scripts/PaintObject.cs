using PaintIn3D;
using UnityEngine;

public class PaintObject : MonoBehaviour
{
    [SerializeField] private P3dPaintableTexture _p3dPaintableTexture;
    [Range(0,1)]
    [SerializeField] private float _smoothness = 0.25f;
    [SerializeField] private Color _colorSmoothness = Color.black;

    private P3dPaintableTexture _p3dPaintableTextureSmoothnes; 
    private MeshCollider _meshCollider;
    private P3dMaterialCloner _p3dMaterialCloner;
    public RenderTexture RenderTexturePaint => _p3dPaintableTexture.Current;
    public MeshCollider MeshCollider => _meshCollider;
    public P3dPaintableTexture PP3dPaintableTexture => _p3dPaintableTexture;
    public Color ColorSmoothness => _colorSmoothness;
    public float Smoothness => _smoothness; 
    
    private void Awake()
    {
        _p3dPaintableTextureSmoothnes = _p3dPaintableTexture.gameObject.AddComponent<P3dPaintableTexture>();
        var slot = new P3dSlot(0, "_SpecGlossMap");
        _p3dPaintableTextureSmoothnes.Slot = slot;
        _p3dPaintableTextureSmoothnes.Group = 55;
        _p3dPaintableTextureSmoothnes.Color = Color.black;
        _p3dPaintableTextureSmoothnes.Activate();
        
        _meshCollider = _p3dPaintableTexture.gameObject.GetComponent<MeshCollider>();
        _p3dMaterialCloner = _p3dPaintableTexture.gameObject.GetComponent<P3dMaterialCloner>();
    }

    private void Start()
    {
        _p3dMaterialCloner.Current.SetFloat("_Smoothness", _smoothness);
        _p3dMaterialCloner.Activate();
    }
}
