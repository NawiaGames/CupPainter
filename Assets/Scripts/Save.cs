using UnityEngine;

[DefaultExecutionOrder(-5)]
public class Save : MonoBehaviour
{
    [SerializeField] private SelectedPaintObjects _selectedPaintObjects;
    [SerializeField] private bool _isOpenAllLevels = false;
    private const string _percentSaveName = "Percent";
    private int[] _percentLevels;
    private PaintObject[] _paintObjectsSelectedObjects;

    public void SetPaintObjectsSelectedObjects(PaintObject[] paintObjects)
    {
        _paintObjectsSelectedObjects = new PaintObject[paintObjects.Length];
        for (var i = 0; i < paintObjects.Length; i++)
            _paintObjectsSelectedObjects[i] = paintObjects[i];
    }

    public void SetPaintObjectsSelectedObject(int index, PaintObject paintObject) =>
        _paintObjectsSelectedObjects[index] = paintObject;

    public void SetRenderPaintObjects(SampleObject[] sampleObjects)
    {
        for (var i = 0; i < sampleObjects.Length; i++)
        {
            if(_percentLevels[i] > PanelMatch.BorderNextLevel)
            _paintObjectsSelectedObjects[i].Renderer.material.mainTexture =
                sampleObjects[i].Renderer.material.mainTexture;
        }
    }
    
    public PaintObject[] GetPaintObjectsSelectedObjects() => _paintObjectsSelectedObjects;

    public void SetPercentLevelsFromSave(int length)
    {
        var indexLevel = 0;
        _percentLevels = new int[length];
        if (!_isOpenAllLevels)
        {
            for (var i = 0; i < length; i++)
            {
                _percentLevels[i] = PlayerPrefs.GetInt(_percentSaveName + i, 0);
                if (_percentLevels[i] > PanelMatch.BorderNextLevel)
                    indexLevel = i + 1;
            }
        }
        else
        {
            for (var i = 0; i < length; i++)
            {
                _percentLevels[i] = 100;
                if (_percentLevels[i] > PanelMatch.BorderNextLevel)
                    indexLevel = 1;
            }
        }

        _selectedPaintObjects.SetCurrentIndex(indexLevel);
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

    public void SaveInfo()
    {
        for (var i = 0; i < _percentLevels.Length; i++)
        {
            PlayerPrefs.SetInt(_percentSaveName + i, _percentLevels[i]);
        }
    }
}