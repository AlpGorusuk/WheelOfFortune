using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class WheelItem
{
    [Range(1f, 99f)]
    //The chance of this item for select 1 to 99
    public float dropChance = 1f;
    public Sprite itemSprite;
    public e_Item_Types itemType;
    public int itemValue = 1;
    public float GetDropChance() { return dropChance; }
}
