using System;
using System.Collections;
using GameLib.UI;
using UnityEngine;

public class TutorialCup : MonoBehaviour
{
    [SerializeField] private InputOverlayTutorial _inputOverlayTutorial;
    [SerializeField] private Transform _compareTransform; 
    [SerializeField] private float _timeNextStep = 3f;

    private bool _nextStep = true;

    private void Start()
    {
        Debug.Log(_compareTransform.position);
        _inputOverlayTutorial.Activate(_compareTransform.position);
    }

    public void StartTutorial()
    {
     /*   StopAllCoroutines();
        
        if (SelectedPaintObjects.CurrentPaintObjectIndex == 0)
        {
            StartCoroutine(StartTutorialLevel1Coroutine());
        }*/
    }

    private IEnumerator StartTutorialLevel1Coroutine()
    {
        var position = new Vector3[]{ Vector3.down * 30f, _compareTransform.position};
        _nextStep = true; 
        for (var i = 0; i < position.Length; )
        {
            if (_nextStep)
            {
                _nextStep = false;
                StartCoroutine(WaitClick(position[i]));
                i++;
                yield return new WaitForSeconds(_timeNextStep);
            }
            yield return null; 
        }
        
    }

    private IEnumerator WaitClick(Vector3 position)
    {
        _inputOverlayTutorial.Activate(position);

        while (true)
        {
            if(Input.GetMouseButtonDown(0))
                break;
            yield return null;
        }
        
        _inputOverlayTutorial.Deactivate();
        _nextStep = true; 
    }
    
}
