using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class WheelItemContainer : MonoBehaviour
{
    [SerializeField] private Transform ImageParent;
    private Sprite _imageSprite;
    public Sprite ImageSprite { get => _imageSprite; set => _imageSprite = value; }
    private int _itemValue;
    public int ItemValue { get => _itemValue; set => _itemValue = value; }
    public void UpdateValues(int valueText, Sprite imageSprite)
    {
        ItemValue = valueText;
        string formattedNumber = Utils.FormatNumber(ItemValue);
        ImageSprite = imageSprite;
        ImageParent.GetComponentInChildren<Image>().sprite = _imageSprite;
        ImageParent.GetComponentInChildren<TextMeshProUGUI>().text = "x" + formattedNumber;
    }
    public void UpdateItemValue(int valueText)
    {
        ItemValue = valueText;
        string formattedNumber = Utils.FormatNumber(ItemValue);
        ImageParent.GetComponentInChildren<TextMeshProUGUI>().text = "x" + formattedNumber;
    }
}