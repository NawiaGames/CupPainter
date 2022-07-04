using System;
using System.Collections;
using GameLib.UI;
using UnityEngine;

public class TutorialCup : MonoBehaviour
{
    [SerializeField] private InputOverlayTutorial _inputOverlayTutorial;

    private void Awake()
    {
        _inputOverlayTutorial.Initialize();
    }

    private void Start()
    {
     //   _inputOverlayTutorial.Activate(Vector3.down * 30f);
    }
    
    

    public void StartTutorial()
    {
      //  StopAllCoroutines();
    //  _inputOverlayTutorial.Activate(Vector3.down * 30f);
     /*   Debug.Log("Active? " + gameObject.activeInHierarchy);
        if (SelectedPaintObjects.CurrentPaintObjectIndex == 1)
        {
            _inputOverlayTutorial.Activate(Vector3.down * 30f);*/
     //       StartCoroutine(StartTutorialLevel1Coroutine());
     //   }
    }

    private IEnumerator StartTutorialLevel1Coroutine()
    {
        Debug.Log("I am work");
        while (true)
        {
            if(Input.GetMouseButtonDown(0))
                break;
            yield return null;
        }
        
      //  _inputOverlayTutorial.Deactivate();
    }
}
