using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectPaintObjectUI : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;

    public Button Button => _button;
    public TMP_Text Text => _text; 

}
