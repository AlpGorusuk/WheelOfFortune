using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wheel
{
    public List<WheelItem> WheelItemList = new List<WheelItem>();
    public WheelItem GetObtainedItem()
    {
        float weightedSum = 0f;
        foreach (var item in WheelItemList)
        {
            float dropChance = item.GetDropChance();
            weightedSum += dropChance;
        }

        float randomValue = UnityEngine.Random.value * weightedSum;

        foreach (var item in WheelItemList)
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
}

// public class Wheel : MonoBehaviour, ISpinnable
// {
//     [SerializeField] private WheelSO wheelDataSO;
//     [SerializeField] private Transform wheelItemParent;
//     private List<WheelItem> wheelItemList = new List<WheelItem>();

//     public List<WheelItem> WheelItemList
//     {
//         get { return wheelItemList; }
//         set { wheelItemList = value; }
//     }
//     public WheelItem GetObtainedItem()
//     {
//         float weightedSum = 0f;
//         foreach (var item in wheelItemList)
//         {
//             float dropChance = item.GetDropChance();
//             weightedSum += dropChance;
//         }

//         float randomValue = UnityEngine.Random.value * weightedSum;

//         foreach (var item in wheelItemList)
//         {
//             float dropChance = item.GetDropChance();
//             randomValue -= dropChance;
//             if (randomValue <= 0)
//             {
//                 return item;
//             }
//         }
//         return null;
//     }
//     public void Spin()
//     {

//     }
//     //
//     public Transform GetWheelChildTransform() { return wheelItemParent; }
//     public void ClearWheelItemList()
//     {
//         wheelItemList.Clear();
//     }
// }