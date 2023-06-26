using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.U2D;
using Utilities;

public class GameManager : Singleton<GameManager>
{
    private List<Tuple<int, Sprite>> obtainedItemData;
    public List<Tuple<int, Sprite>> ObtainedItemData { get => obtainedItemData; }
    private const string PlayerPrefsKey = "ObtainedItemData";
    public SpriteAtlas ItemSpriteAtlas;

    public override void Awake()
    {
        base.Awake();
        LoadGame();
    }

    private void LoadObtainedData()
    {
        string serializedData = PlayerPrefs.GetString(PlayerPrefsKey);
        if (!string.IsNullOrEmpty(serializedData))
        {
            string[] dataStrings = serializedData.Split(';');
            obtainedItemData = new List<Tuple<int, Sprite>>();
            foreach (string dataString in dataStrings)
            {
                string[] values = dataString.Split(',');

                if (values.Length >= 2 && int.TryParse(values[0], out int intValue))
                {
                    Sprite sprite = null;

                    if (!string.IsNullOrEmpty(values[1]))
                    {
                        string spriteName = Utilities.Utils.RemoveUnwantedText(values[1], "(Clone)");
                        sprite = ItemSpriteAtlas.GetSprite(spriteName);
                    }
                    Tuple<int, Sprite> itemData = new Tuple<int, Sprite>(intValue, sprite);
                    obtainedItemData.Add(itemData);
                }
            }

            Debug.Log("GAME LOADED!");
        }
        else
        {
            Debug.Log("LOAD DATA IS NOT VALID!");
            obtainedItemData = new List<Tuple<int, Sprite>>();
        }
    }
    private void SaveObtainedData()
    {
        Dictionary<string, int> itemCounts = new Dictionary<string, int>();

        // Ürün adetlerini hesapla
        foreach (Tuple<int, Sprite> itemData in obtainedItemData)
        {
            if (itemData.Item2 != null)
            {
                string spriteName = itemData.Item2.name;

                if (itemCounts.ContainsKey(spriteName))
                {
                    // Sözlükte zaten bu ürün varsa adetini artır
                    itemCounts[spriteName] += itemData.Item1;
                }
                else
                {
                    // Yeni bir ürün ise sözlüğe ekle
                    itemCounts.Add(spriteName, itemData.Item1);
                }
            }
        }
        // Yeni serialized data oluştur
        string serializedData = "";
        foreach (KeyValuePair<string, int> kvp in itemCounts)
        {
            string spriteName = kvp.Key;
            int itemCount = kvp.Value;
            string dataString = string.Format("{0},{1};", itemCount, spriteName);
            serializedData += dataString;
        }

        // PlayerPrefs'e kaydet
        PlayerPrefs.SetString(PlayerPrefsKey, serializedData);
        PlayerPrefs.Save();
    }


    public void SaveGame(List<Tuple<int, Sprite>> targetItemData)
    {
        MergeItemData(obtainedItemData, targetItemData);
        SaveObtainedData();
        Debug.Log("GAME SAVED!");
    }
    private void MergeItemData(List<Tuple<int, Sprite>> obtainedItemData, List<Tuple<int, Sprite>> targetObtainedItemData)
    {
        foreach (Tuple<int, Sprite> targetTuple in targetObtainedItemData)
        {
            bool foundMatch = false;

            foreach (Tuple<int, Sprite> obtainedTuple in obtainedItemData)
            {
                if (obtainedTuple.Item2 == targetTuple.Item2) // Sprite'lar aynı ise
                {
                    int newCount = obtainedTuple.Item1 + targetTuple.Item1; // Int değerleri birleştir
                    Tuple<int, Sprite> mergedTuple = new Tuple<int, Sprite>(newCount, obtainedTuple.Item2);
                    obtainedItemData.Remove(obtainedTuple); // Eski tuple'ı listeden kaldır
                    obtainedItemData.Add(mergedTuple); // Yeni tuple'ı ekle
                    foundMatch = true;
                    break; // İlgili tuple işlendi, döngüden çık
                }
            }

            if (!foundMatch)
            {
                obtainedItemData.Add(targetTuple); // Yeni tuple olarak ekle
            }
        }
    }

    public void LoadGame()
    {
        LoadObtainedData();
    }
}