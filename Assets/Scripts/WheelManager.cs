using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelManager : MonoBehaviour, IObserver
{
    [SerializeField] GameObject WheelItemPrefab;
    [SerializeField] List<Wheel> WheelList = new List<Wheel>();
    private Wheel _currentWheel;
    private List<WheelItem> wheelItemList = new List<WheelItem>();

    public List<WheelItem> WheelItemList
    {
        get { return wheelItemList; }
        set { wheelItemList = value; }
    }
    private void Start()
    {
        SpinButton.Instance.Attach(this);

        _currentWheel = WheelList[0];
    }
    public void UpdateObserver(Observable observable)
    {

    }

    public void SpinTheCurrentWheel()
    {
        Debug.Log("enter here");
    }
    // private IEnumerator AnimateSpinnigWheel()
    // {

    // }

    public GameObject GetWheelItemPrefab() { return WheelItemPrefab; }
    public Transform GetCurrentWheelChildTransform()
    {
        Transform _transform = WheelList[0].GetWheelChildTransform();
        return _transform;
    }
}
