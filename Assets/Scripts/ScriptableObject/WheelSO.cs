using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Wheel", order = 2)]
public class WheelSO : ScriptableObject
{
    public e_Spin_Direction spinDirection;
    public AnimationCurve spinCurve;
    public int minSpinTime = 3, maxSpinTime = 4;
}