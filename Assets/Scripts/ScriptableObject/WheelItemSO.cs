using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WheelItem", order = 1)]
public class WheelItemSO : ScriptableObject
{
    [Range(1f, 99f)]
    [Tooltip("The chance of this item being selected (1% to 99%)")]
    public float dropChance = 1f;
    public Sprite itemSprite;
    public e_Item_Types itemType;
    public int itemValue = 1;
}