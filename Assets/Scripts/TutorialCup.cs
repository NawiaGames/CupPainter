using System.Collections;
using GameLib.UI;
using UnityEngine;

public class TutorialCup : MonoBehaviour
{
    [SerializeField] private InputOverlayTutorial _inputOverlayTutorial;

    public void StartTutorial()
    {
        StopAllCoroutines();
        
        if(SelectedPaintObjects.CurrentPaintObjectIndex == 1)
            StartCoroutine(StartTutorialLevel1Coroutine());
    }

    private IEnumerator StartTutorialLevel1Coroutine()
    {
        _inputOverlayTutorial.Activate(Vector3.down * 30f);

        while (true)
        {
            if(Input.GetMouseButtonDown(0))
                break;
            yield return null;
        }
    }
}
