using System;
using System.Collections.Generic;

[Serializable]
public class Wheel
{
    public string nameOfTheWheel;
    public WheelSO DataSO;
    public List<WheelItem> wheelItemList = new List<WheelItem>();
    public WheelItem GetObtainedWheelItem()
    {
        float weightedSum = 0f;
        foreach (var item in wheelItemList)
        {
            float dropChance = item.GetDropChance();
            weightedSum += dropChance;
        }

        float randomValue = UnityEngine.Random.value * weightedSum;

        foreach (var item in wheelItemList)
        {
            float dropChance = item.GetDropChance();
            randomValue -= dropChance;
            if (randomValue <= 0)
            {
                return item;
            }
        }
        return null;
    }
    public int GetObtainedItemIndex(WheelItem item)
    {
        int obtainedObjindex = wheelItemList.IndexOf(item);
        return obtainedObjindex;
    }
}