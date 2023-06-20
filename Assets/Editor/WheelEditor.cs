using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WheelEditor : EditorWindow
{
    private int mySlider = 8;
    private List<GameObject> WheelObjList = new List<GameObject>();
    WheelManager wheelManager;
    [MenuItem("Window/WheelEditor")]
    public static void ShowWindow()
    {
        // Editor penceresini aç
        var window = GetWindow<WheelEditor>();
        window.titleContent = new GUIContent("WHEEL EDITOR");
        window.Show();
        //
        wheelManager = FindObjectOfType<WheelManager>();
    }
    public void UpdateWheelManager(int _itemCount)
    {
        if (wheelManager != null)
        {
            int itemCount = _itemCount;

            Transform _targetTransform = wheelManager.GetCurrentWheelChildTransform(); ;
            for (int i = 0; i <= itemCount - 1; i++)
            {
                // WheelItem newItem = new WheelItem();
                GameObject wheelObjPrefab = wheelManager.GetWheelItemPrefab();
                GameObject _newWheelObj = GameObject.Instantiate(wheelObjPrefab);
                _newWheelObj.transform.SetParent(_targetTransform, false);
                WheelObjList.Add(_newWheelObj);
                // newItem.itemObject = newWheelObj;
                // wheelManager.wheelItems.Add(newItem);
            }
            OrganizeWheelItems(WheelObjList, _targetTransform);
        }
    }
    private void OrganizeWheelItems(List<GameObject> _wheelObjList, Transform _wheelChildTransform)
    {
        int _itemCount = _wheelObjList.Count;
        float itemRotation = 360 / _itemCount;
        _wheelChildTransform.localEulerAngles = new Vector3(0, 0, -itemRotation) * 0.5f;
        for (int i = 0; i <= _itemCount - 1; i++)
        {
            _wheelObjList[i].transform.localEulerAngles = new Vector3(0, 0, (itemRotation * -i));
        }

    }
    private void DeleteWheelItems(List<GameObject> _wheelObjList)
    {
        int _itemCount = _wheelObjList.Count;
        for (int i = 0; i <= _itemCount - 1; i++)
        {
            Undo.DestroyObjectImmediate(WheelObjList[i]);
        }
        _wheelObjList.RemoveRange(0, _wheelObjList.Count);
    }
    private void OnGUI()
    {
        EditorGUILayout.Space(10f);

        EditorGUILayout.LabelField("Slider:", EditorStyles.boldLabel);
        mySlider = EditorGUILayout.IntSlider(mySlider, 8, 16);

        EditorGUILayout.Space(20f);

        // Buton
        if (GUILayout.Button("Add Wheel Items"))
        {
            UpdateWheelManager(mySlider);
        }
        // Buton
        if (GUILayout.Button("Delete Wheel Items"))
        {
            DeleteWheelItems(WheelObjList);
        }
    }
}