using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class WheelItemContainer : MonoBehaviour
{
    [SerializeField] private Transform ImageParent;
    private Sprite _imageSprite;
    public Sprite ImageSprite { get => _imageSprite; set => _imageSprite = value; }
    private string _valueText;
    public string ValueText { get => _valueText; set => _valueText = value; }
    public void UpdateValues()
    {
        ImageParent.GetComponentInChildren<Image>().sprite = _imageSprite;
        ImageParent.GetComponentInChildren<TextMeshProUGUI>().text = "x" + _valueText;
    }
}
