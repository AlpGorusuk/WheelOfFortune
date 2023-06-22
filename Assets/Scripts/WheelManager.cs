using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEditor;
using Unity.VisualScripting;

public class WheelManager : Singleton<WheelManager>, IObservable, IObserver
{
    public WheelContainer WheelContainer;
    public GameObject wheelItemPrefab;
    [Space(10)]
    public SpriteAtlas itemSpriteAtlas;
    [Space(20)]
    public List<Wheel> WheelList;
    [Range(4, 16)]
    public int wheelItemCountSlider = 0;

    //Variables-----------------------------
    public Dictionary<string, Sprite> itemSpriteDictionary = new Dictionary<string, Sprite>();
    //Observable
    private List<IObserver> observers = new List<IObserver>();
    //For spin
    private int spinCount = 0;
    private Wheel oldWheel;

    //
    private void Start()
    {
        SpinButton.Instance.Attach(this);
        //Inits
        SetWheel();
        InitChangeableSpriteAtlas();
        SetWheelItemObjects();
    }
    private void OnDestroy()
    {
        SpinButton.Instance.Detach(this);
    }
    //IObserver---
    public void UpdateObserver(IObservable observable)
    {
        SpinTheCurrentWheel();
    }
    //Spin Current Wheel---
    public void SpinTheCurrentWheel()
    {

    }

    //Wheel Initialize----------------------------------------------
    public GameObject WheelItemPrefab() { return wheelItemPrefab; }
    public Transform GetCurrentWheelChildTransform()
    {
        return WheelContainer.wheelItemParent;
    }
    //Init Wheel
    private void SetWheel()
    {
        int spinCount = this.spinCount;
        Wheel currentWheel = WheelList[spinCount];
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

        GameObject wheelObjPrefab = WheelItemPrefab();

        for (int i = 0; i < wheelItemCount; i++)
        {
            GameObject newWheelObj = GameObject.Instantiate(wheelObjPrefab);
            newWheelObj.transform.SetParent(targetTransform, false);

            containerList.Add(newWheelObj.GetComponent<WheelItemContainer>());
        }

        RotateWheelItems(containerList, targetTransform);
    }
    private void RotateWheelItems(List<WheelItemContainer> wheelObjList, Transform wheelChildTransform)
    {
        int itemCount = wheelObjList.Count;
        float itemRotation = 360f / itemCount;
        wheelChildTransform.localEulerAngles = new Vector3(0f, 0f, -itemRotation * 0.5f);
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

    //Observable Props-------------------
    public void Attach(IObserver observer)
    {
    }

    public void Detach(IObserver observer)
    {
    }

    public void Notify()
    {
    }
    //Sprite Atlas-------------------------
    public void InitChangeableSpriteAtlas()
    {
        Sprite[] sprites = new Sprite[itemSpriteAtlas.spriteCount];
        itemSpriteAtlas.GetSprites(sprites);
        foreach (Sprite sprite in sprites)
        {
            itemSpriteDictionary.Add(sprite.name, sprite);
        }
    }
    private Sprite GetSpriteFromAtlas(string spriteName)
    {
        Sprite sprite = null;
        itemSpriteDictionary.TryGetValue(spriteName, out sprite);
        return sprite;
    }
    //set Wheel Items--------------------------
    public void SetWheelItemObjects()
    {
        int spinCount = this.spinCount;

        Wheel currentWheel = WheelList[spinCount];
        List<WheelItem> wheelItems = currentWheel.wheelItemList;

        for (int i = 0; i < WheelContainer.wheelItemContainerList.Count; i++)
        {
            WheelItemContainer container = WheelContainer.wheelItemContainerList[i];
            WheelItem wheelItem = wheelItems[i];

            string spriteName = "ui_icon_" + wheelItem.itemSpriteName + "_value(Clone)";
            Sprite sprite = GetSpriteFromAtlas(spriteName);
            if (sprite != null)
            {
                container.ImageSprite = sprite;
            }
            else
            {
                Debug.LogWarning("Sprite not found: " + spriteName);
            }

            container.ValueText = wheelItem.itemValue.ToString();
            container.UpdateValues();
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
        EditorGUILayout.PropertyField(serializedObject.FindProperty("itemSpriteAtlas"), new GUIContent("item Sprite Atlas:"), true);
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