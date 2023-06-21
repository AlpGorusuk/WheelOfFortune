using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Wheel", order = 1)]
public class WheelSO : ScriptableObject
{
    public SpriteAtlas spriteAtlas;
    public e_Wheel_Types e_Wheel_Types;
    public e_Spin_Direction e_Spin_Direction;
    public AnimationCurve spinCurve;
    public int minSpinTime = 3, maxSpinTime = 4;
    public Sprite GetWheelBaseSpriteFromAtlas()
    {
        string itemTypeString = Enum.GetName(typeof(e_Item_Types), e_Wheel_Types);
        string _indicatorSprite = "ui_wheel_" + itemTypeString + "_base";
        return spriteAtlas.GetSprite(_indicatorSprite);
    }
    public Sprite GetWheelIndicatorSpriteFromAtlas()
    {
        string itemTypeString = Enum.GetName(typeof(e_Item_Types), e_Wheel_Types);
        string _indicatorSprite = "ui_wheel_" + itemTypeString + "_indicator";
        return spriteAtlas.GetSprite(_indicatorSprite);
    }
}