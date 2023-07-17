using System;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Managers;

namespace WheelOfFortune.UI.Panels
{
    public class ObtainedItemPanel : MonoBehaviour
    {
        [SerializeField] private GameObject wheelItemPrefab;
        [SerializeField] private Transform wheelItemParent;
        private Dictionary<Sprite, Tuple<int, WheelItemContainer>> obtainedItemDataDictionary = new Dictionary<Sprite, Tuple<int, WheelItemContainer>>();
        private void Start()
        {
            UIManager.Instance.playScreen.AddEventListener<Tuple<Sprite, int, bool>>(UpdateObtainItemDictionary);
            UIManager.Instance.playScreen.AddEventListener<EventArgs>(args => ClearObtainedItems());
        }
        private void OnDestroy()
        {
            UIManager.Instance.playScreen.RemoveEventListener<Tuple<Sprite, int, bool>>(UpdateObtainItemDictionary);
            UIManager.Instance.playScreen.RemoveEventListener<EventArgs>(args => ClearObtainedItems());
        }
        private void UpdateObtainItemDictionary(Tuple<Sprite, int, bool> obtainedItemData)
        {
            Sprite _sprite = obtainedItemData.Item1;
            int itemValue = obtainedItemData.Item2;
            bool isItemAlreadyExists = IsItemAlreadyExists(_sprite);

            if (!isItemAlreadyExists)
            {
                WheelItemContainer wheelItemContainer = CreateItem(obtainedItemData);
                Tuple<int, WheelItemContainer> tempTuple = new Tuple<int, WheelItemContainer>(itemValue, wheelItemContainer);
                AddItem(_sprite, tempTuple);
            }
            else
            {
                Tuple<int, WheelItemContainer> updatedTuple = obtainedItemDataDictionary[_sprite];
                int newItemValue = updatedTuple.Item1 + itemValue;
                updatedTuple = new Tuple<int, WheelItemContainer>(newItemValue, updatedTuple.Item2);
                obtainedItemDataDictionary[_sprite] = updatedTuple;
                UpdateItem(updatedTuple);
            }
        }
        public List<Tuple<int, Sprite>> GetSaveableObtainedItemData()
        {
            List<Tuple<int, Sprite>> saveableDataList = new List<Tuple<int, Sprite>>();

            foreach (KeyValuePair<Sprite, Tuple<int, WheelItemContainer>> kvp in obtainedItemDataDictionary)
            {
                Sprite sprite = kvp.Key;
                Tuple<int, WheelItemContainer> data = kvp.Value;
                saveableDataList.Add(new Tuple<int, Sprite>(data.Item1, sprite));
            }
            ClearObtainedItems();
            return saveableDataList;
        }
        public void ClearObtainedItems()
        {
            foreach (var kvp in obtainedItemDataDictionary)
            {
                Tuple<int, WheelItemContainer> data = kvp.Value;
                Destroy(data.Item2.gameObject);
            }

            obtainedItemDataDictionary.Clear();
        }
        private WheelItemContainer CreateItem(Tuple<Sprite, int, bool> obtainedItemData)
        {
            GameObject _wheelObject = Instantiate(wheelItemPrefab);
            WheelItemContainer wheelItemContainer = _wheelObject.GetComponent<WheelItemContainer>();

            Sprite _objSprite = obtainedItemData.Item1;
            int _itemValue = obtainedItemData.Item2;

            _wheelObject.transform.SetParent(wheelItemParent);
            _wheelObject.transform.localScale = Vector3.one;
            wheelItemContainer.UpdateValues(_itemValue, _objSprite);
            return wheelItemContainer;
        }
        private void UpdateItem(Tuple<int, WheelItemContainer> _tuple)
        {
            _tuple.Item2.UpdateItemValue(_tuple.Item1);
        }
        // Dictionary Check
        public bool IsItemAlreadyExists(Sprite sprite)
        {
            return obtainedItemDataDictionary.ContainsKey(sprite);
        }

        public void AddItem(Sprite sprite, Tuple<int, WheelItemContainer> data)
        {
            if (!IsItemAlreadyExists(sprite))
            {
                obtainedItemDataDictionary.Add(sprite, data);
            }
        }
    }
}