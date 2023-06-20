using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour, ISpinnable
{
    [SerializeField] private WheelSO wheelDataSO;
    [SerializeField] private Transform wheelItemParent;
    public void Spin()
    {

    }
    public Transform GetWheelChildTransform() { return wheelItemParent; }
}