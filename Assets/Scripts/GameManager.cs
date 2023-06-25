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
        string serializedData = "";

        foreach (Tuple<int, Sprite> itemData in obtainedItemData)
        {
            string spriteName = itemData.Item2 != null ? itemData.Item2.name : "";
            string dataString = string.Format("{0},{1};", itemData.Item1, spriteName);

            string existingData = PlayerPrefs.GetString(PlayerPrefsKey);
            if (!string.IsNullOrEmpty(existingData))
            {
                string[] existingDataStrings = existingData.Split(';');
                foreach (string existingDataString in existingDataStrings)
                {
                    string[] existingValues = existingDataString.Split(',');
                    if (existingValues.Length >= 2 && int.TryParse(existingValues[0], out int existingIntValue))
                    {
                        string existingSpriteName = existingValues[1];
                        // Eğer aynı sprite name'i içeren bir veri varsa item data'yı topla ve üzerine yaz
                        if (existingSpriteName == spriteName)
                        {
                            serializedData += string.Format("{0},{1};", itemData.Item1 + existingIntValue, spriteName);
                        }
                    }
                }
            }
            else
            {
                serializedData += dataString;
            }
        }

        PlayerPrefs.SetString(PlayerPrefsKey, serializedData);
        PlayerPrefs.Save();
    }



    public void SaveGame(List<Tuple<int, Sprite>> targetObtainedItemData)
    {
        obtainedItemData = targetObtainedItemData;
        SaveObtainedData();
        Debug.Log("GAME SAVED!");
    }

    public void LoadGame()
    {
        LoadObtainedData();
    }
}