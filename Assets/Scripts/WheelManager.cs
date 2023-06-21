using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WheelManager : MonoBehaviour, IObserver
{
    [Header("Wheel")]
    [SerializeField] private WheelContainer WheelContainer;
    [Space(10)]
    [Header("Wheel Item")]
    [SerializeField] private GameObject wheelItemPrefab;
    [Space(10)]
    [SerializeField] private List<Wheel> WheelList = new List<Wheel>();
    private void Start()
    {
        SpinButton.Instance.Attach(this);
    }
    private void OnDestroy()
    {
        SpinButton.Instance.Detach(this);
    }
    //IObserver---
    public void UpdateObserver(Observable observable)
    {
        SpinTheCurrentWheel();
    }
    //Spin Current Wheel---
    public void SpinTheCurrentWheel()
    {

    }


    //
    public GameObject WheelItemPrefab() { return wheelItemPrefab; }
    public Transform GetCurrentWheelChildTransform()
    {
        return WheelContainer.wheelItemParent;
    }
    //Init Wheel
    private void SetWheel()
    {

    }
    //Get Correct Wheel Data
    public WheelSO GetWheelData(string name)
    {
        List<WheelSO> wheelDataSOList = WheelContainer.wheelDataSOList;
        return wheelDataSOList.FirstOrDefault(obj => obj.name == name);
    }
    //Add Wheel Item Objects
    public void SetWheelItemObjectList(List<GameObject> _list)
    {
        WheelContainer.WheelItemObjectList = _list;
    }
    //Remove Wheel Item Objects on list
    public void ResetWheelItemObjectList()
    {
        WheelContainer.WheelItemObjectList.Clear();
    }
}
[Serializable]
public class WheelContainer
{
    public List<WheelSO> wheelDataSOList;
    public Image WheelBaseImage;
    public Image WheelIndicatorImage;
    public Transform wheelItemParent;
    public List<GameObject> WheelItemObjectList = new List<GameObject>();
}