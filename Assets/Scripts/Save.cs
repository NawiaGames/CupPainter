using UnityEngine;

[DefaultExecutionOrder(-5)]
public class Save : MonoBehaviour
{
    private const string _percentSaveName = "Percent";
    private int[] _percentLevels;
    private PaintObject[] _paintObjectsSelectedObjects;

    public void SetPaintObjectsSelectedObjects(PaintObject[] materialCloners)
    {
        _paintObjectsSelectedObjects = new PaintObject[materialCloners.Length];
        for (var i = 0; i < materialCloners.Length; i++)
            _paintObjectsSelectedObjects[i] = materialCloners[i];
    }

    public void SetPaintObjectsSelectedObject(int index, PaintObject renderer) =>
        _paintObjectsSelectedObjects[index] = renderer;

    public PaintObject[] GetPaintObjectsSelectedObjects() => _paintObjectsSelectedObjects;

    public void SetPercentLevelsFromSave(int length)
    {
        _percentLevels = new int[length];
        for (var i = 0; i < length; i++)
        {
            _percentLevels[i] = PlayerPrefs.GetInt(_percentSaveName + i, 0);
        }
    }

    public void SetPercentLevel(int index, int value)
    {
        if (_percentLevels[index] < value)
            _percentLevels[index] = value;
    }

    public int[] GetPercentLevels() => _percentLevels;

    private void OnDisable()
    {
        SaveInfo();
    }

    private void SaveInfo()
    {
        for (var i = 0; i < _percentLevels.Length; i++)
        {
            PlayerPrefs.SetInt(_percentSaveName + i, _percentLevels[i]);
        }
    }
}