using System.Collections;
using GameLib.UI;
using UnityEngine;

public class TutorialCup : MonoBehaviour
{
    [SerializeField] private InputOverlayTutorial _inputOverlayTutorial;
    [SerializeField] private float _timeNextStep = 3f;
    [SerializeField] private float _beginWaitTimeTutorial = 1f; 
    [SerializeField] private Transform _compareTransform; 
    [SerializeField] private Transform _colorPaintPallet;
    [SerializeField] private Transform _mixColorsTransform;
    [SerializeField] private Transform _waterCupTransform;

    private bool _nextStep = true;
    private int _counterStep;

    private Vector3[] positionsLevelOne;
    private Vector3[] positionsLevelTwo;
    private Vector3[] positionsLevelEight;

    private void Awake()
    {
        positionsLevelOne = new Vector3[]{ Vector3.zero, _compareTransform.position};
        positionsLevelTwo = new Vector3[] { _colorPaintPallet.position };
        positionsLevelEight = new Vector3[] { _mixColorsTransform.position, _waterCupTransform.position };
    }

    public void StartTutorial()
    {
        StopAllCoroutines();

        switch (SelectedPaintObjects.CurrentPaintObjectIndex)
        {
            case 0:
            {
                
                StartCoroutine(StartTutorialLevelCoroutine(positionsLevelOne));
                break;
            }
            case 1:
            {

                StartCoroutine(StartTutorialLevelCoroutine(positionsLevelTwo, _beginWaitTimeTutorial));
                break;
            }
            case 7:
            {
                StartCoroutine(StartTutorialLevelCoroutine(positionsLevelEight, _beginWaitTimeTutorial));
                break;
            }
        }
    }

    private IEnumerator StartTutorialLevelCoroutine(Vector3[] positions, float waitTime = 0)
    {
        yield return new WaitForSeconds(waitTime); 
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
