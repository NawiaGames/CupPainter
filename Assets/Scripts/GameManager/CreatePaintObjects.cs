using UnityEngine;

public class CreatePaintObjects : MonoBehaviour
{
    [SerializeField] private Level[] _levelsSO;
    [SerializeField] private Transform _paintObjectsTransform;
    [SerializeField] private Transform _smallPaintSampleTransform;
    [SerializeField] private int _scaleSmallPaintSampleObject = 50; 
    [SerializeField] private Transform _bigPaintSampleTransform;
    [SerializeField] private int _scaleBigPaintSampleObject = 150; 

    private PaintObject[] _paintObjects;
    private GameObject[] _smallPaintSampleObjects;
    private GameObject[] _bigPaintSampleObjects;
    private Texture2D[] _texture2DModelsSample;

    public PaintObject[] PaintObjects => _paintObjects;
    public GameObject[] SmallPaintSampleObjects => _smallPaintSampleObjects;
    public GameObject[] BigPaintSampleObjects => _bigPaintSampleObjects;
    public Texture2D[] Texture2DModelsSample => _texture2DModelsSample; 

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        _paintObjects = new PaintObject[_levelsSO.Length];
        _smallPaintSampleObjects = new GameObject[_levelsSO.Length];
        _bigPaintSampleObjects = new GameObject[_levelsSO.Length];
        _texture2DModelsSample = new Texture2D[_levelsSO.Length]; 
        
        for (var i = 0; i < _levelsSO.Length; i++)
        {
            CreatePaintObject(i);
            
            CreateSmallPaintSampleObject(i);

            CreateBigPaintSampleObject(i);

            _texture2DModelsSample[i] = _levelsSO[i].TextureModel; 
        }
    }

    private void CreateBigPaintSampleObject(int i)
    {
        var bigPaintSampleObject = InstantiateObject(_levelsSO[i].ModelSampleObject);
        bigPaintSampleObject.transform.SetParent(_bigPaintSampleTransform);
        _bigPaintSampleObjects[i] = bigPaintSampleObject;
        _bigPaintSampleObjects[i].transform.localRotation = Quaternion.identity; 
        _bigPaintSampleObjects[i].transform.localPosition = Vector3.zero;
        _bigPaintSampleObjects[i].transform.localScale = new Vector3(_scaleBigPaintSampleObject,
            _scaleBigPaintSampleObject, _scaleBigPaintSampleObject);
    }

    private void CreateSmallPaintSampleObject(int i)
    {
        var smallPaintSampleObject = InstantiateObject(_levelsSO[i].ModelSampleObject);
        smallPaintSampleObject.transform.SetParent(_smallPaintSampleTransform);
        _smallPaintSampleObjects[i] = smallPaintSampleObject;
        _smallPaintSampleObjects[i].transform.localRotation = Quaternion.identity;
        _smallPaintSampleObjects[i].transform.localPosition = Vector3.zero;
        _smallPaintSampleObjects[i].transform.localScale = new Vector3(_scaleSmallPaintSampleObject, 
            _scaleSmallPaintSampleObject, _scaleSmallPaintSampleObject);
    }

    private void CreatePaintObject(int i)
    {
        var paintObjects = InstantiateObject(_levelsSO[i].ModelObject.gameObject);
        paintObjects.gameObject.transform.SetParent(_paintObjectsTransform);
        _paintObjects[i] = paintObjects.GetComponent<PaintObject>();
        _paintObjects[i].gameObject.transform.localPosition = Vector3.zero;
    }

    private GameObject InstantiateObject(GameObject createObject)
    {
        var initObject = Instantiate(createObject);
        initObject.SetActive(false);
        return initObject; 
    }
}
