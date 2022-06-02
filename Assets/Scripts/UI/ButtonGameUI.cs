using UnityEngine;
using UnityEngine.Events;

public class ButtonGameUI : MonoBehaviour
{
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
}
