using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WheelItem", order = 1)]
public class WheelItemSO : ScriptableObject
{
    public string prefabName;

    public int numberOfPrefabsToCreate;
    public Vector3[] spawnPoints;
}