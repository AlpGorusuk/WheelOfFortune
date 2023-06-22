using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Wheel", order = 1)]
public class WheelSO : ScriptableObject
{
    public e_Wheel_Types e_Wheel_Types;
    public e_Spin_Direction e_Spin_Direction;
    public Sprite baseSprite, indicatorSprite;
    public AnimationCurve spinCurve;
    public int minSpinTime = 3, maxSpinTime = 4;
    public Sprite GetWheelBaseSprite() { return baseSprite; }
    public Sprite GetWheelIndicatorSprite() { return indicatorSprite; }
}