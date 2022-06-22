using UnityEngine;

public class ExampleTextureDraw : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Animator _animator; 
    
    private bool _changeDirection;
    
    public void SetTexture(Texture2D texture2D) => _meshRenderer.material.mainTexture = texture2D;

    public void SetHeight(float height)
    {
        var localScale = _meshRenderer.gameObject.transform.localScale;
        _meshRenderer.gameObject.transform.localScale = new Vector3(localScale.x, height,localScale.z);
    }

    public void UpdateAnimationTrigger()
    {
        _changeDirection = !_changeDirection;

        _animator.SetTrigger(_changeDirection ? "activate" : "deactivate");
    }
}
