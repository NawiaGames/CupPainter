using System;
using System.Collections;
using GameLib.UI;
using UnityEngine;

public class TutorialCup : MonoBehaviour
{
    [SerializeField] private InputOverlayTutorial _inputOverlayTutorial;

    public void StartTutorial()
    {
        if (SelectedPaintObjects.CurrentPaintObjectIndex == 0)
        {
            StartCoroutine(StartTutorialLevel1Coroutine());
        }
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
        
        _inputOverlayTutorial.Deactivate();
    }
}
