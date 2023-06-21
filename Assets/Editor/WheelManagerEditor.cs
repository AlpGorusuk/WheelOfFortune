using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WheelManagerEditor : EditorWindow
{
    private int mySlider = 8;
    WheelManager wheelManager;
    private List<GameObject> wheelObjList = new List<GameObject>();

    [MenuItem("Window/WheelEditor")]
    public static void ShowWindow()
    {
        var window = GetWindow<WheelManagerEditor>();
        window.titleContent = new GUIContent("WHEEL EDITOR");
        window.Show();
    }
    private void OnEnable()
    {
        wheelManager = FindObjectOfType<WheelManager>();
    }

    //------------------------------------------------------
    public void UpdateWheelManager(int _itemCount)
    {
        List<WheelItem> wheelItemList = new List<WheelItem>();
        if (wheelManager != null)
        {
            int itemCount = _itemCount;
            Transform _targetTransform = wheelManager.GetCurrentWheelChildTransform(); ;
            for (int i = 0; i <= itemCount - 1; i++)
            {
                GameObject wheelObjPrefab = wheelManager.WheelItemPrefab();
                GameObject _newWheelObj = GameObject.Instantiate(wheelObjPrefab);
                //
                _newWheelObj.transform.SetParent(_targetTransform, false);
                wheelObjList.Add(_newWheelObj);
            }
            RotateWheelItems(wheelObjList, _targetTransform);
            wheelManager.SetWheelItemObjectList(wheelObjList);
        }
    }
    private void RotateWheelItems(List<GameObject> _wheelObjList, Transform _wheelChildTransform)
    {
        int _itemCount = _wheelObjList.Count;
        float itemRotation = 360 / _itemCount;
        _wheelChildTransform.localEulerAngles = new Vector3(0, 0, -itemRotation) * 0.5f;
        for (int i = 0; i <= _itemCount - 1; i++)
        {
            _wheelObjList[i].transform.localEulerAngles = new Vector3(0, 0, (itemRotation * -i));
        }
    }

    //------------------------------------------------------
    private void DeleteWheelItems(List<GameObject> _wheelObjList)
    {
        int _itemCount = _wheelObjList.Count;
        if (_itemCount <= 0)
        {
            Debug.LogError($"YOU NEED TO SET WHEEL OBJECTS!");
            return;
        }
        for (int i = 0; i <= _itemCount - 1; i++)
        {
            Undo.DestroyObjectImmediate(wheelObjList[i]);
        }
        _wheelObjList.RemoveRange(0, _wheelObjList.Count);
    }

    private void OnGUI()
    {
        EditorGUILayout.Space(10f);

        EditorGUILayout.LabelField("Slider:", EditorStyles.boldLabel);
        mySlider = EditorGUILayout.IntSlider(mySlider, 8, 16);

        EditorGUILayout.Space(20f);

        // Buton for Add Wheel Items
        if (GUILayout.Button("Add Wheel Items"))
        {
            UpdateWheelManager(mySlider);
        }
        // Buton for Delete Wheel Items
        if (GUILayout.Button("Delete Wheel Items"))
        {
            DeleteWheelItems(wheelObjList);
        }
    }
}