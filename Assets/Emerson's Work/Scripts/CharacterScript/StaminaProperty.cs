using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StaminaProperty
{
    public float player_total_energy = 100.0f;
    public float runningEnergy = 20.0f;
    public float jumpingEnergy = 30.0f;
    public float consumption_speed = 0.5f;
    public float gasping_air_threshold = 50.0f;
    //regenaration of stamina
    public float regenaration_rate = 5.0f;
    public float regeneration_speed = 0.5f;
}
