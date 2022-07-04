using PaintIn3D;
using UnityEngine;

public class PaintObject : MonoBehaviour
{
    [SerializeField] private P3dPaintableTexture _p3dPaintableTexture;
    [Range(0,1)]
    [SerializeField] private float _smoothness = 0.25f;
    [SerializeField] private Color _colorSmoothness = Color.black;

    private P3dPaintableTexture _p3dPaintableTextureSmoothness; 
    private MeshCollider _meshCollider;
    private P3dMaterialCloner _p3dMaterialCloner;
    private Renderer _renderer;
    public Renderer Renderer => _renderer; 
    public RenderTexture RenderTexturePaint => _p3dPaintableTexture.Current;
    public MeshCollider MeshCollider => _meshCollider;
    public P3dPaintableTexture P3dPaintableTexture => _p3dPaintableTexture;
    public Color ColorSmoothness => _colorSmoothness;
    public float Smoothness => _smoothness; 
    
    private void Awake()
    {

        
        _meshCollider = _p3dPaintableTexture.gameObject.GetComponent<MeshCollider>();
        _p3dMaterialCloner = _p3dPaintableTexture.gameObject.GetComponent<P3dMaterialCloner>();
        _renderer = _p3dPaintableTexture.gameObject.GetComponent<Renderer>();
    }

    private void Start()
    {
        _p3dMaterialCloner.Current.SetFloat("_Smoothness", _smoothness);
        _p3dMaterialCloner.Activate();
    }
}
