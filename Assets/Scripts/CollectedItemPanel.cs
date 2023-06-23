using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedItemPanel : Singleton<CollectedItemPanel>
{
    [SerializeField] private GameObject wheelItemPrefab;
    [SerializeField] private Transform wheelItemParent;
    private List<Tuple<Sprite, int, bool>> obtainedItemDataList = new List<Tuple<Sprite, int, bool>>();
    public void UpdateCollectItemList(Tuple<Sprite, int, bool> obtainedItemData)
    {
        obtainedItemDataList.Add(obtainedItemData);
    }
}
