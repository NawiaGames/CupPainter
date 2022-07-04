using UnityEngine;

[DefaultExecutionOrder(-5)]
public class Save : MonoBehaviour
{
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

    public void SetPercentLevels(int length)
    {
        _percentLevels = new int[length]; 
        for (var i = 0; i < length; i++)
        {
            _percentLevels[i] = 0; 
        }
    }

    public void SetPercentLevel(int index, int value) => _percentLevels[index] = value; 

    public int[] GetPercentLevels() => _percentLevels;
}
