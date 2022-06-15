using UnityEngine;

[CreateAssetMenu (fileName = "New level", menuName = "Level", order = 51)]
public class Level : ScriptableObject
{
    [SerializeField] private GameObject _modelObject;
    [SerializeField] private Texture2D _textureModel;
    [SerializeField] private Color[] _colorsPlattes; 
}
