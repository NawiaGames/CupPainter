using System;
using PaintIn3D;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ButtonGameUI : MonoBehaviour
{
    [SerializeField] private P3dPaintSphere _paintSphere;
    [SerializeField] private Rotation _rotationObject;
    [SerializeField] private SelectedPaintObjects _selectedPaintObjects;

    public void OnOverloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    
    public void OnChangeOpacity(float value) => _paintSphere.Opacity = value;
    
    public void OnChangeHardness(float value) => _paintSphere.Hardness = value;

    public void OnChangeRadius(float value) => _paintSphere.Radius = value;

    public void OnChangeSpeedRotation(float value) => _rotationObject.SetSpeed(value);

    public void OnAddIndexPaintObject() => _selectedPaintObjects.UpdateIndexToOnePaintObject(1);

    public void OnSubtractIndexPaintObject() => _selectedPaintObjects.UpdateIndexToOnePaintObject(-1); 
}
