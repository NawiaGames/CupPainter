using UnityEngine;
using UnityEngine.UI;

public class CreateLevel : MonoBehaviour
{
    [SerializeField] private Level[] _levelsSO;
    [SerializeField] private Transform _paintObjectsTransform;
    [SerializeField] private Transform _spawnPaintSampleTransform;
    [SerializeField] private int _scaleSmallPaintSampleObject = 50; 
    [SerializeField] private Transform _selectedSpawnPaintObjectsTransform;
    [SerializeField] private GameObject _templateCreatePaintObject; 
    [SerializeField] private int _scaleSelectedPaintObject = 30;
    [SerializeField] private ButtonGameUI _buttonGameUI; 
    
    private UnityEngine.Events.UnityAction _buttonCallback;
    private PaintObject[] _paintObjects;
    private GameObject[] _smallPaintSampleObjects;
    private GameObject[] _selectedSpawnPaintObjects;
    private Texture2D[] _texture2DModelsSample;
    private Colors[] _colorsPallet;
    private bool[] _canActivatePallets;

    public PaintObject[] PaintObjects => _paintObjects;
    public GameObject[] SmallPaintSampleObjects => _smallPaintSampleObjects;
    public GameObject[] SelectedSpawnPaintObjects => _selectedSpawnPaintObjects;
    public Texture2D[] Texture2DModelsSample => _texture2DModelsSample;
    public Colors[] ColorsPallet => _colorsPallet;
    public bool[] CanActivatePallets => _canActivatePallets;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        _paintObjects = new PaintObject[_levelsSO.Length];
        _smallPaintSampleObjects = new GameObject[_levelsSO.Length];
        _selectedSpawnPaintObjects = new GameObject[_levelsSO.Length];
        _texture2DModelsSample = new Texture2D[_levelsSO.Length];
        _colorsPallet = new Colors[_levelsSO.Length];
        _canActivatePallets = new bool[_levelsSO.Length]; 
        
        for (var i = 0; i < _levelsSO.Length; i++)
        {
            CreatePaintObject(i);
            
            CreateSmallPaintSampleObject(i);

            CreateSelectedPaintObject(i);

            _texture2DModelsSample[i] = _levelsSO[i].TextureModel;
            _canActivatePallets[i] = _levelsSO[i].ActivatePalletBlend; 
            _colorsPallet[i] = new Colors(_levelsSO[i].ColorsPallet); 
        }
    }

    private void CreateSelectedPaintObject(int i)
    {
        var template = Instantiate(_templateCreatePaintObject, _selectedSpawnPaintObjectsTransform);
        template.transform.localScale = Vector3.one;
        template.transform.localRotation = Quaternion.identity;
        template.transform.localPosition = Vector3.zero;
        var button = template.GetComponent<Button>();
        if (button != null)
        {
            _buttonCallback = null;
            _buttonCallback = () => _buttonGameUI.SelectedLevel(i);
            button.onClick.AddListener(_buttonCallback);
        }
        var selectedPaintObject = InstantiateObject(_levelsSO[i].ModelSampleObject, template.transform, true);
        _selectedSpawnPaintObjects[i] = selectedPaintObject;
        _selectedSpawnPaintObjects[i].transform.localRotation = Quaternion.identity; 
        _selectedSpawnPaintObjects[i].transform.localPosition = Vector3.zero;
        _selectedSpawnPaintObjects[i].transform.localScale = new Vector3(_scaleSelectedPaintObject,
            _scaleSelectedPaintObject, _scaleSelectedPaintObject);
    }

    private void CreateSmallPaintSampleObject(int i)
    {
        var smallPaintSampleObject = InstantiateObject(_levelsSO[i].ModelSampleObject, _spawnPaintSampleTransform, false);
        _smallPaintSampleObjects[i] = smallPaintSampleObject;
        _smallPaintSampleObjects[i].transform.localRotation = Quaternion.identity;
        _smallPaintSampleObjects[i].transform.localPosition = Vector3.zero;
        _smallPaintSampleObjects[i].transform.localScale = new Vector3(_scaleSmallPaintSampleObject, 
            _scaleSmallPaintSampleObject, _scaleSmallPaintSampleObject);
    }

    private void CreatePaintObject(int i)
    {
        var paintObjects = InstantiateObject(_levelsSO[i].ModelObject.gameObject, _paintObjectsTransform, false);
        _paintObjects[i] = paintObjects.GetComponent<PaintObject>();
        _paintObjects[i].gameObject.transform.localPosition = Vector3.zero;
    }

    private GameObject InstantiateObject(GameObject createObject, Transform parent, bool activate)
    {
        var initObject = Instantiate(createObject, parent);
        initObject.SetActive(activate);
        return initObject; 
    }
}
