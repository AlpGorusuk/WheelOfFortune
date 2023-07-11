using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using TMPro;
using WheelOfFortune.UI.Buttons;

namespace WheelOfFortune.Managers
{
    public class WheelManager : Singleton<WheelManager>, IObserver
    {
        public WheelContainer WheelContainer;
        public GameObject wheelItemPrefab;
        [Space(20)]
        public List<Wheel> WheelList;
        [Range(4, 16)]
        public int wheelItemCountSlider = 0;
        [Range(0, 2)]
        public float resultScreenDelay = 1;
        public TextMeshProUGUI safeZoneText;
        //Observable
        private List<IObserver> observers = new List<IObserver>();
        //For spin
        private Wheel currentWheel { get => GetCurrentWheel(); }
        private WheelSO currentWheelData { get => currentWheel.DataSO; }
        private Wheel oldWheel;
        private int spinCount = 0;
        public int SpinCount { get => spinCount; set => spinCount = value; }
        public Action<Tuple<Sprite, int, bool>> itemObtainedCallback;
        public Action SpinStartCallback, SpinStoppedCallback;
        //Consts
        private const float FullDegreeOfCircle = 360;
        private const int SuperZoneCount = 30, SafeZoneCount = 5;
        private const string SpriteCloneText = "(Clone)", SafeZoneText = "YOU ARE IN SAFE ZONE!", SuperZoneText = "YOU ARE IN SUPER ZONE!";
        public override void Awake()
        {
            base.Awake();
            //Callbacks
            UIManager.Instance.playScreen.ItemCollectedCallback += UpdateCurrentWheel;
            UIManager.Instance.playScreen.ItemCollectedCallback += UpdateZoneText;
            UIManager.Instance.playScreen.ItemCollectFailedCallback += ResetCurrentWheel;
        }

        private void Start()
        {
            SpinStartCallback += SpinButton.Instance.Hide;
            SpinStoppedCallback += RotateWheel;
            SpinButton.Instance.Attach(this);
            //
            SetWheel();
            RotateWheel();
            SetWheelItemObjects();
        }
        private void OnDestroy()
        {
            SpinButton.Instance.Detach(this);
            //Callbacks
            UIManager.Instance.playScreen.ItemCollectedCallback -= UpdateCurrentWheel;
            UIManager.Instance.playScreen.ItemCollectedCallback -= UpdateZoneText;
            UIManager.Instance.playScreen.ItemCollectFailedCallback -= ResetCurrentWheel;
            SpinStartCallback -= SpinButton.Instance.Hide;
            SpinStoppedCallback -= RotateWheel;
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
            SpinStartCallback?.Invoke();
            float itemCount = currentWheel.wheelItemList.Count;
            AnimationCurve spinCurve = currentWheelData.spinCurve;

            int spinDir = (int)currentWheelData.e_Spin_Direction;

            Transform childToSpin = GetCurrentWheelChildTransform();

            float targetRotation = (obtainedItemObj.transform.localEulerAngles.z + FullDegreeOfCircle) % FullDegreeOfCircle;
            float totalRotation = FullDegreeOfCircle * spinDir;
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

            yield return new WaitForSeconds(resultScreenDelay);
            SpinStoppedCallback?.Invoke();
            itemObtainedCallback?.Invoke(obtaineItemData);
        }

        //Get Obtained Data---------------------------------------------
        private Tuple<Sprite, int, bool> SetObtainedData(WheelItem obtainedItem)
        {
            string spriteName = "ui_icon_" + obtainedItem.itemSpriteName + "_value";
            Sprite _sprite = UIManager.Instance.GetSpriteFromDictionary(spriteName + SpriteCloneText);
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
                Wheel_Types oldWheelType = oldWheel.DataSO.e_Wheel_Types;
                Wheel_Types currWheelType = currentWheelData.e_Wheel_Types;
                if (currWheelType == oldWheelType)
                {
                    return;
                }
            }

            oldWheel = currentWheel;
        }
        private void RotateWheel() { Transform _transform = GetCurrentWheelChildTransform(); _transform.localEulerAngles = Vector3.zero; }
        private void UpdateCurrentWheel(Tuple<Sprite, int, bool> tuple)
        {
            SpinCount++; SetWheel();
            SetWheelItemObjects();
        }
        public void ResetCurrentWheel()
        {
            SpinCount = 0;
            SetWheel();
            SetWheelItemObjects();
            safeZoneText.gameObject.SetActive(false);
        }
        //Update Zone Text
        private void UpdateZoneText(Tuple<Sprite, int, bool> tuple)
        {
            int count = SpinCount;
            if (count % SuperZoneCount == 0)
            {
                safeZoneText.text = SuperZoneText;
                safeZoneText.gameObject.SetActive(true);
                return;
            }
            else if (count % SafeZoneCount == 0)
            {
                safeZoneText.text = SafeZoneText;
                safeZoneText.gameObject.SetActive(true);
            }
            else
            {
                safeZoneText.gameObject.SetActive(false);
            }
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
            RotateWheelItems(containerList);
        }
        private void RotateWheelItems(List<WheelItemContainer> wheelObjList)
        {
            int itemCount = wheelObjList.Count;
            float itemRotation = FullDegreeOfCircle / itemCount;
            for (int i = 0; i < itemCount; i++)
            {
                wheelObjList[i].transform.localEulerAngles = new Vector3(0f, 0f, itemRotation * -i);
            }
        }
        //Reset Wheel Item Object On Editor
        public void DeleteWheelItems(WheelManager wheelManager)
        {
            List<WheelItemContainer> containerList = wheelManager.WheelContainer.wheelItemContainerList;
            int _count = containerList.Count;

            for (int i = 0; i <= _count - 1; i++)
            {
                DestroyImmediate(containerList[i].gameObject);
            }
            containerList.RemoveRange(0, _count);
        }
        //set Wheel Items--------------------------
        public void SetWheelItemObjects()
        {
            List<WheelItem> wheelItems = currentWheel.wheelItemList;
            List<WheelItemContainer> containerList = WheelContainer.wheelItemContainerList;

            for (int i = 0; i < containerList.Count; i++)
            {
                WheelItemContainer container = containerList[i];
                WheelItem wheelItem = wheelItems[i];

                string spriteName = "ui_icon_" + wheelItem.itemSpriteName + "_value";
                Sprite sprite = UIManager.Instance.GetSpriteFromDictionary(spriteName + SpriteCloneText);
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
            EditorGUILayout.PropertyField(serializedObject.FindProperty("resultScreenDelay"), new GUIContent("Result Screen Delay:"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("WheelContainer"), new GUIContent("Wheel Container:"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("wheelItemCountSlider"), new GUIContent("wheel Item Count Slider:"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("WheelList"), new GUIContent("WheelList:"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("safeZoneText"), new GUIContent("safe Zone Text:"), true);

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
}