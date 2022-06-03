using PaintIn3D;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ButtonGameUI : MonoBehaviour
{
    [SerializeField] private P3dPaintSphere _paintSphere; 
    private bool _isAddSpeed; 

    public event UnityAction<bool> OnUpdateSpeedRotationEvent;

    public void OnAddSpeedRotationButton()
    {
        _isAddSpeed = true; 
        OnUpdateSpeedRotationEvent?.Invoke(_isAddSpeed);
    }

    public void OnSubtractSpeedRotationButton()
    {
        _isAddSpeed = false;
        OnUpdateSpeedRotationEvent?.Invoke(_isAddSpeed); 
    }

    public void OnOverloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void OnChangeOpacity(float value)
    {
        _paintSphere.Opacity = value; 
    }
}
