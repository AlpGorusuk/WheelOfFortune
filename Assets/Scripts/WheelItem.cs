using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WheelItem
{
    [Range(1f, 99f)]
    //The chance of this item for select 1 to 99
    public float dropChance;
    public string itemSpriteName;
    public int itemValue;
    public float GetDropChance() { return dropChance; }
}
