using System.Collections;
using GameLib.UI;
using UnityEngine;

public class TutorialCup : MonoBehaviour
{
    [SerializeField] private InputOverlayTutorial _inputOverlayTutorial;
    [SerializeField] private Transform _compareTransform; 
    [SerializeField] private float _timeNextStep = 3f;
    [SerializeField] private Transform _colorPaintPallet;
    [SerializeField] private Transform _mixColorsTransform;
    [SerializeField] private Transform _waterCupTransform;

    private bool _nextStep = true;
    private int _counterStep; 

    public void StartTutorial()
    {
        StopAllCoroutines();

        switch (SelectedPaintObjects.CurrentPaintObjectIndex)
        {
            case 0:
            {
                var positions = new Vector3[]{ Vector3.zero, _compareTransform.position};
                StartCoroutine(StartTutorialLevelCoroutine(positions));
                break;
            }
            case 1:
            {
                var positions = new Vector3[] { _colorPaintPallet.position };
                StartCoroutine(StartTutorialLevelCoroutine(positions));
                break;
            }
            case 7:
            {
                var positions = new Vector3[] { _mixColorsTransform.position, _waterCupTransform.position };
                StartCoroutine(StartTutorialLevelCoroutine(positions));
                break;
            }
        }
    }

    private IEnumerator StartTutorialLevelCoroutine(Vector3[] positions)
    {
        _nextStep = true;
        _counterStep = 0;
        while (_counterStep < positions.Length)
        {
            if (_nextStep)
            {
                _nextStep = false;
                if(_counterStep > 0)
                    yield return new WaitForSeconds(_timeNextStep);
                StartCoroutine(WaitClick(positions[_counterStep]));
  
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
        _counterStep++; 
        _nextStep = true; 
    }
    
}
