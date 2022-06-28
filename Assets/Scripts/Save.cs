using UnityEngine;

[DefaultExecutionOrder(-5)]
public class Save : MonoBehaviour
{
    private int[] _percentLevels;
    private int _sizePercentLevels;

    public void SetPercentLevels(int length)
    {
        _sizePercentLevels = length;
        _percentLevels = new int[_sizePercentLevels]; 
        for (var i = 0; i < _sizePercentLevels; i++)
        {
            _percentLevels[i] = 0; 
        }
    }

    public void SetPercentLevel(int index, int value) => _percentLevels[index] = value; 

    public int[] GetPercentLevels() => _percentLevels;
    
}
