using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IntruderProerty
{
    //public properties
    public float intruder_damage = 15.0f;
    public float damageSpeed = 0.25f;

    //private properties
    [HideInInspector] public float current_damage;
    [HideInInspector] public bool isAtPlace = true;
}
