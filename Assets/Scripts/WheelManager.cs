using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using Unity.VisualScripting;

public class WheelManager : MonoBehaviour, IObserver
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
    private Wheel currentWheel { get => WheelList[spinCount]; }
    private WheelSO currentWheelData { get => currentWheel.DataSO; }
    private Wheel oldWheel;
    private int spinCount = 0;
    public int SpinCount { get => spinCount; set => spinCount = value; }
    private List<Tuple<Sprite, int>> obtainedItemData = new List<Tuple<Sprite, int>>();

    public void InitWheelManager()
    {
        //Inits
        SetWheel();
        InitChangeableSpriteAtlas();
        SetWheelItemObjects();
    }
    private void Start()
    {
        SpinButton.Instance.Attach(this);
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
        WheelItem obtainedWheelItem = currentWheel.GetObtainedWheelItem();
        int obtainedItemIndex = currentWheel.GetObtainedItemIndex(obtainedWheelItem);
        Debug.Log(obtainedItemIndex);
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

        yield return new WaitForSeconds(2f);
        GetObtainedWheelItemData(obtainedItem);
        SpinButton.Instance.EnableGameObject(true);
        SpinCount++;
    }
    //Get Obtained Data---------------------------------------------
    public void GetObtainedWheelItemData(WheelItem obtainedItem)
    {
        Debug.Log(obtainedItem.itemSpriteName);
        bool isFailItem = obtainedItem.isFailItem;
        if (isFailItem)
        {
            UIManager.Instance.ChangeStateFail();
        }
        else
        {
            UIManager.Instance.ChangeStateWin();
        }
    }

    //Wheel Initialize----------------------------------------------
    public Transform GetCurrentWheelChildTransform()
    {
        return WheelContainer.wheelItemParent;
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
    }
    private void ResetWheelRotation()
    {

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
[Serializable]
public class DataEntry
{
    public Sprite sprite;
    public int value;
}
