using UnityEngine;

public class Frames : MonoBehaviour
{
    [SerializeField] private GameObject[] _frames;

    public void ActivateMixFrame()
    {
        for (var i = 0; i < _frames.Length - 1; i++)
            _frames[i].SetActive(false);
        
        _frames[_frames.Length - 1].SetActive(true);
    }

    public void ActivateColorFrame(int index)
    {
        for (var i = 0; i < _frames.Length; i++)
            _frames[i].SetActive(i == index);
    }
}