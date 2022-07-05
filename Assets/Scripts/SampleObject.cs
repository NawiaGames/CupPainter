using UnityEngine;

public class SampleObject : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    public Renderer Renderer => _renderer; 
}
