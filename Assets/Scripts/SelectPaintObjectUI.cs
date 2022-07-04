using GameLib.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectPaintObjectUI : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private UIPanel _uiPanelUnlock;
    [SerializeField] private GameObject _lockedGameObject;
    [SerializeField] private GameObject _readyGameObject;
    public Button Button => _button;
    public TMP_Text Text => _text;
    public UIPanel UIPanelUnlock => _uiPanelUnlock;
    public GameObject LockedGameObject =>_lockedGameObject;
    public GameObject ReadyGameObject => _readyGameObject;


}
