using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    [SerializeField] private Level[] _levelsSO;
    [Header("Paint Sample")]
    [SerializeField] private Transform _paintObjectsTransform;
    [SerializeField] private Transform _spawnPaintSampleTransform;
    [SerializeField] private int _scaleSmallPaintSampleObject = 50;
    [SerializeField] private float _downPositionYpaintSample = -60; 
    [Header("Paint Selected")]
    [SerializeField] private Transform _selectedSpawnPaintObjectsTransform;
    [SerializeField] private SelectPaintObjectUI _templateCreatePaintObject;
    [SerializeField] private int _scaleSelectedPaintObject = 30;
    [SerializeField] private float _downPositionYPaintSelected = -50; 
    [SerializeField] private ButtonGameUI _buttonGameUI;
    [Space]
    [SerializeField] private Save _save;
    private UnityEngine.Events.UnityAction _buttonCallback;
    private PaintObject[] _paintObjects;
    private GameObject[] _smallPaintSampleObjects;
    private PaintObject[] _selectedSpawnPaintObjects;
    private Texture2D[] _texture2DModelsSample;
    private SelectPaintObjectUI[] _selectedPaintObjects; 
    private Colors[] _colorsPallet;
    private bool[] _canActivatePallets;
    private int[] _percentLevels;
   
    public PaintObject[] PaintObjects => _paintObjects;
    public PaintObject[] SelectedSpawnPaintObjects => _selectedSpawnPaintObjects;
    public GameObject[] SmallPaintSampleObjects => _smallPaintSampleObjects;
    public Texture2D[] Texture2DModelsSample => _texture2DModelsSample;
    public Colors[] ColorsPallet => _colorsPallet;
    public SelectPaintObjectUI[] SelectPaintObjectUI => _selectedPaintObjects; 
    public bool[] CanActivatePallets => _canActivatePallets;
    public Save Save => _save; 

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        _paintObjects = new PaintObject[_levelsSO.Length];
        _smallPaintSampleObjects = new GameObject[_levelsSO.Length];
        _selectedSpawnPaintObjects = new PaintObject[_levelsSO.Length];
        _texture2DModelsSample = new Texture2D[_levelsSO.Length];
        _colorsPallet = new Colors[_levelsSO.Length];
        _canActivatePallets = new bool[_levelsSO.Length];
        _selectedPaintObjects = new SelectPaintObjectUI[_levelsSO.Length];
        
        _save.SetPercentLevels(_levelsSO.Length);
        _percentLevels = _save.GetPercentLevels();
        
        for (var i = 0; i < _levelsSO.Length; i++)
        {
            CreatePaintObject(i);

            CreateSmallPaintSampleObject(i);

            CreateSelectedPaintObject(i);

            _texture2DModelsSample[i] = _levelsSO[i].TextureModel;
            _canActivatePallets[i] = _levelsSO[i].ActivatePalletBlend;
            _colorsPallet[i] = new Colors(_levelsSO[i].ColorsPallet);
        }
        
        _save.SetPaintObjectsSelectedObjects(_selectedSpawnPaintObjects);
    }

    private void CreateSelectedPaintObject(int i)
    {
        var template = Instantiate(_templateCreatePaintObject, _selectedSpawnPaintObjectsTransform);
        var templateGameObject = template.gameObject;
        templateGameObject.transform.localScale = Vector3.one;
        templateGameObject.transform.localRotation = Quaternion.identity;
        templateGameObject.transform.localPosition = Vector3.zero;
        _selectedPaintObjects[i] = template; 
        
        _buttonCallback = null;
        _buttonCallback = () => _buttonGameUI.SelectedLevel(i);
        template.Button.onClick.AddListener(_buttonCallback);
        template.Text.text = _percentLevels[i] + "%"; 

        var selectedPaintObject = InstantiateObject(_levelsSO[i].ModelObject.gameObject, template.transform, true);
        _selectedSpawnPaintObjects[i] = selectedPaintObject.GetComponent<PaintObject>();
        _selectedSpawnPaintObjects[i].transform.localRotation = Quaternion.identity;
        var position = new Vector3(0, _downPositionYPaintSelected, 0); 
        _selectedSpawnPaintObjects[i].transform.localPosition = position;
        _selectedSpawnPaintObjects[i].transform.localScale = new Vector3(_scaleSelectedPaintObject,
            _scaleSelectedPaintObject, _scaleSelectedPaintObject);
    }

    private void CreateSmallPaintSampleObject(int i)
    {
        var smallPaintSampleObject =
            InstantiateObject(_levelsSO[i].ModelSampleObject, _spawnPaintSampleTransform, false);
        _smallPaintSampleObjects[i] = smallPaintSampleObject;
        _smallPaintSampleObjects[i].transform.localRotation = Quaternion.identity;
        var position = new Vector3(0, _downPositionYpaintSample , 0);
        _smallPaintSampleObjects[i].transform.localPosition = position;
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