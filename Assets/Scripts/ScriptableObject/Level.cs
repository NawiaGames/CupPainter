using UnityEngine;

[CreateAssetMenu (fileName = "New level", menuName = "Level", order = 51)]
public class Level : ScriptableObject
{
    [SerializeField] private GameObject _modelObject;
    [SerializeField] private GameObject _modelSampleObject; 
    [SerializeField] private Texture2D _textureModel;
    [SerializeField] private Color[] _colorsPallet;

    public GameObject ModelObject => _modelObject;
    public GameObject ModelSampleObject => _modelSampleObject; 
    public Texture2D TextureModel => _textureModel;
    public Color[] ColorsPallet => _colorsPallet; 
}
