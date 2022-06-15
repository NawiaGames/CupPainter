using UnityEngine;

public class CreatePaintObjects : MonoBehaviour
{
    [SerializeField] private Level[] _levelsSO;
    [SerializeField] private Transform _paintObjectsTransform;
    [SerializeField] private Transform _smallPaintSampleTransform;
    [SerializeField] private int _scaleSmallPaintSampleObject = 50; 
    [SerializeField] private Transform _bigPaintSampleTransform;
    [SerializeField] private int _scaleBigPaintSampleObject = 150; 

    private GameObject[] _paintObjects;
    private GameObject[] _smallPaintSampleObjects;
    private GameObject[] _bigPaintSampleObjects;

    public GameObject[] PaintObjects => _paintObjects;
    public GameObject[] SmallPaintSampleObjects => _smallPaintSampleObjects;
    public GameObject[] BigPaintSampleObjects => _bigPaintSampleObjects; 

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        _paintObjects = new GameObject[_levelsSO.Length];
        _smallPaintSampleObjects = new GameObject[_levelsSO.Length];
        _bigPaintSampleObjects = new GameObject[_levelsSO.Length];

        for (var i = 0; i < _levelsSO.Length; i++)
        {
            CreatePaintObject(i);
            
            CreateSmallPaintSampleObject(i);

            CreateBigPaintSampleObject(i);
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
        var paintObjects = InstantiateObject(_levelsSO[i].ModelObject);
        paintObjects.transform.SetParent(_paintObjectsTransform);
        _paintObjects[i] = paintObjects;
        _paintObjects[i].transform.localPosition = Vector3.zero;
    }

    private GameObject InstantiateObject(GameObject createObject)
    {
        var initObject = Instantiate(createObject);
        initObject.SetActive(false);
        return initObject; 
    }
}
