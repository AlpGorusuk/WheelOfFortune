using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using Unity.VisualScripting;

public class WheelManager : Singleton<WheelManager>, IObserver
{
    public WheelContainer WheelContainer;
    public GameObject wheelItemPrefab;
    [Space(20)]
    public List<Wheel> WheelList;
    [Range(4, 16)]
    public int wheelItemCountSlider = 0;
    //Observable
    private List<IObserver> observers = new List<IObserver>();
    //For spin
    private Wheel currentWheel { get => GetCurrentWheel(); }
    private WheelSO currentWheelData { get => currentWheel.DataSO; }
    private Wheel oldWheel;
    private int spinCount = 0;
    public int SpinCount { get => spinCount; set => spinCount = value; }
    public Action<Tuple<Sprite, int, bool>> wheelSpinnedCallback;
    public override void Awake()
    {
        base.Awake();
        //Callbacks
        UIManager.Instance.playScreen.ItemCollectedCallback += UpdateCurrentWheel;
        UIManager.Instance.playScreen.ItemCollectFailedCallback += ResetWheel;
    }
    public void InitWheelManager()
    {
        //Inits
        SetWheel();
        SetWheelItemObjects();
    }
    private void Start()
    {
        SpinButton.Instance.Attach(this);
    }
    private void OnDestroy()
    {
        SpinButton.Instance.Detach(this);
        //Callbacks
        UIManager.Instance.playScreen.ItemCollectedCallback -= UpdateCurrentWheel;
        UIManager.Instance.playScreen.ItemCollectFailedCallback -= ResetWheel;
    }
    //IObserver---
    public void UpdateObserver(IObservable observable)
    {
        SpinTheCurrentWheel();
    }
    //Spin Current Wheel---
    public void SpinTheCurrentWheel()
    {
        WheelItem obtainedWheelItem = currentWheel.GetObtainedWheelItem();
        int obtainedItemIndex = currentWheel.GetObtainedItemIndex(obtainedWheelItem);

        GameObject wheelItemObj = WheelContainer.wheelItemContainerList[obtainedItemIndex].gameObject;
        int spinTime = currentWheelData.GetRandomSpintTime();

        StartCoroutine(SpinWheel(spinTime, wheelItemObj, obtainedWheelItem));
    }

    private IEnumerator SpinWheel(float spinTime, GameObject obtainedItemObj, WheelItem obtainedItem)
    {
        SpinButton.Instance.EnableGameObject(false);

        float itemCount = currentWheel.wheelItemList.Count;
        AnimationCurve spinCurve = currentWheelData.spinCurve;

        int spinDir = (int)currentWheelData.e_Spin_Direction;

        Transform childToSpin = GetCurrentWheelChildTransform();

        float targetRotation = (obtainedItemObj.transform.localEulerAngles.z + 360f) % 360f;
        float totalRotation = 360f * spinDir;
        float targetAngle = totalRotation * spinTime + targetRotation;
        float spinTimer = 0f;

        while (spinTimer <= spinTime)
        {
            float newRotation = targetAngle * -spinCurve.Evaluate(spinTimer / spinTime);
            childToSpin.localEulerAngles = new Vector3(0f, 0f, newRotation);
            spinTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //
        Tuple<Sprite, int, bool> obtaineItemData = SetObtainedData(obtainedItem);

        yield return new WaitForSeconds(1f);
        wheelSpinnedCallback?.Invoke(obtaineItemData);
        SpinButton.Instance.EnableGameObject(true);
    }

    //Get Obtained Data---------------------------------------------
    private Tuple<Sprite, int, bool> SetObtainedData(WheelItem obtainedItem)
    {
        string spriteName = "ui_icon_" + obtainedItem.itemSpriteName + "_value(Clone)";
        Sprite _sprite = UIManager.Instance.GetSpriteFromAtlas(spriteName);
        int itemValue = obtainedItem.itemValue;
        bool isFailItem = obtainedItem.isFailItem;

        Tuple<Sprite, int, bool> obtaineItemData = new Tuple<Sprite, int, bool>(_sprite, itemValue, isFailItem);
        return obtaineItemData;
    }

    //Wheel Initialize----------------------------------------------
    public Transform GetCurrentWheelChildTransform()
    {
        return WheelContainer.wheelItemParent;
    }
    private Wheel GetCurrentWheel()
    {
        if (SpinCount >= WheelList.Count)
        {
            SpinCount = 0;
        }
        return WheelList[SpinCount];
    }
    public void SetCurrentWheelChildTransform(Vector3 angle)
    {
        WheelContainer.wheelItemParent.localEulerAngles = angle;
    }
    //Init Wheel
    private void SetWheel()
    {
        WheelSO currentWheelData = currentWheel.DataSO;

        WheelContainer.WheelBaseImage.sprite = currentWheelData.GetWheelBaseSprite();
        WheelContainer.WheelIndicatorImage.sprite = currentWheelData.GetWheelIndicatorSprite();

        if (oldWheel != null)
        {
            e_Wheel_Types oldWheelType = oldWheel.DataSO.e_Wheel_Types;
            e_Wheel_Types currWheelType = currentWheelData.e_Wheel_Types;
            if (currWheelType == oldWheelType)
            {
                return;
            }
        }

        oldWheel = currentWheel;
    }
    private void UpdateCurrentWheel(Tuple<Sprite, int, bool> tuple) { SpinCount++; SetWheel(); SetWheelItemObjects(); }
    private void ResetWheel() { SpinCount = 0; }
    //Set Wheel Item Object On Editor
    public void UpdateWheelManager()
    {
        List<WheelItemContainer> containerList = WheelContainer.wheelItemContainerList;
        Transform targetTransform = GetCurrentWheelChildTransform();
        int wheelItemCount = wheelItemCountSlider;

        if (containerList.Count >= wheelItemCount)
        {
            Debug.LogError("MAX COUNT REACHED!");
            return;
        }

        GameObject wheelObjPrefab = wheelItemPrefab;

        for (int i = 0; i < wheelItemCount; i++)
        {
            GameObject newWheelObj = GameObject.Instantiate(wheelObjPrefab);
            newWheelObj.transform.SetParent(targetTransform, false);

            containerList.Add(newWheelObj.GetComponent<WheelItemContainer>());
        }
        RotateWheelItems(containerList);
    }
    private void RotateWheelItems(List<WheelItemContainer> wheelObjList)
    {
        int itemCount = wheelObjList.Count;
        float itemRotation = 360f / itemCount;
        for (int i = 0; i < itemCount; i++)
        {
            wheelObjList[i].transform.localEulerAngles = new Vector3(0f, 0f, itemRotation * -i);
        }
    }
    //Reset Wheel Item Object On Editor
    public void DeleteWheelItems(WheelManager wheelManager)
    {
        Undo.RegisterCompleteObjectUndo(wheelManager, "Delete wheel items");
        List<WheelItemContainer> containerList = wheelManager.WheelContainer.wheelItemContainerList;
        int _count = containerList.Count;

        for (int i = 0; i <= _count - 1; i++)
        {
            Undo.DestroyObjectImmediate(containerList[i].gameObject);
        }
        containerList.RemoveRange(0, _count);
    }
    //set Wheel Items--------------------------
    public void SetWheelItemObjects()
    {
        List<WheelItem> wheelItems = currentWheel.wheelItemList;

        for (int i = 0; i < WheelContainer.wheelItemContainerList.Count; i++)
        {
            WheelItemContainer container = WheelContainer.wheelItemContainerList[i];
            WheelItem wheelItem = wheelItems[i];

            string spriteName = "ui_icon_" + wheelItem.itemSpriteName + "_value(Clone)";
            Sprite sprite = UIManager.Instance.GetSpriteFromAtlas(spriteName);
            if (sprite != null)
            {
                container.ImageSprite = sprite;
            }
            else
            {
                Debug.LogWarning("Sprite not found: " + spriteName);
            }

            container.ItemValue = wheelItem.itemValue;
            container.UpdateValues(container.ItemValue, container.ImageSprite);
        }
    }
}

//EDiTOR FOR EASILY USAGE
#if UNITY_EDITOR
[CustomEditor(typeof(WheelManager))]
public class WheelManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var wheelManager = (WheelManager)target;

        EditorGUILayout.Space(20f);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("wheelItemPrefab"), new GUIContent("Wheel Item Prefab:"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("WheelContainer"), new GUIContent("WheelContainer:"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("wheelItemCountSlider"), new GUIContent("wheelItemCountSlider:"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("WheelList"), new GUIContent("WheelList:"), true);

        EditorGUILayout.Space(20f);

        // Button for Add Wheel Items
        if (GUILayout.Button("Add Wheel Items"))
        {
            wheelManager.UpdateWheelManager();
        }

        // Button for Delete Wheel Items
        if (GUILayout.Button("Delete Wheel Items"))
        {
            wheelManager.DeleteWheelItems(wheelManager);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif

[Serializable]
public class WheelContainer
{
    public Image WheelBaseImage;
    public Image WheelIndicatorImage;
    public Transform wheelItemParent;
    public List<WheelItemContainer> wheelItemContainerList;
}