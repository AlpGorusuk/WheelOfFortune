using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectedItemPanel : MonoBehaviour, IObserver
{
    [SerializeField] private CloseButton closeButton;
    [SerializeField] private GameObject wheelItemPrefab;
    [SerializeField] private Transform wheelItemParent;
    public void InitCollectedItemPanel()
    {
        Show();
        closeButton.Attach(this);
    }
    private void OnDestroy()
    {
        closeButton.Detach(this);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateObserver(IObservable observable)
    {
        Hide();
        UIManager.Instance.homeScreen.ShowOpenButton();
    }
    public void LoadObtainedItemPanel(List<Tuple<int, Sprite>> obtainedItemData)
    {
        List<Tuple<int, Sprite>> dataList = obtainedItemData;
        foreach (var data in dataList)
        {
            CreateItem(data);
        }
    }
    private void CreateItem(Tuple<int, Sprite> obtainedItemData)
    {
        GameObject _wheelObject = Instantiate(wheelItemPrefab);
        WheelItemContainer wheelItemContainer = _wheelObject.GetComponent<WheelItemContainer>();

        int _itemValue = obtainedItemData.Item1;
        Sprite _objSprite = obtainedItemData.Item2;

        _wheelObject.transform.SetParent(wheelItemParent);
        wheelItemContainer.UpdateValues(_itemValue, _objSprite);
    }
}