using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaminaSc : MonoBehaviour
{
    public StaminaProperty stamina;
    [HideInInspector] public bool isGaspingAir = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RegenerateStamina();
        checkIfGaspingAir();
        //Debug.Log($"Player's Energy: {stamina.player_total_energy}");
    }

    //if true, then character needs a break before she can run and jump again
    private void checkIfGaspingAir()
    {
        if(stamina.player_total_energy <= 5.0f && !isGaspingAir)
        {
            isGaspingAir = true;
        }
        else if(isGaspingAir && stamina.player_total_energy >= stamina.gasping_air_threshold)
        {
            isGaspingAir = false;
        }
    }

    //consume enrgy but speeds the movement of the player
    public bool consumeJumping()
    {
        //if player has energy to jump; threshold is the jump value
        if(stamina.jumpingEnergy <= stamina.player_total_energy)
        {
            stamina.player_total_energy -= stamina.jumpingEnergy;
            stamina.player_total_energy = Mathf.Clamp(stamina.player_total_energy, 0.0f, 100.0f);
            Debug.Log($"Player is jumping!");
            return true;
        }
        else
        {
            Debug.Log($"Player can't jump!");
            return false;
        }
    }

    //consume enrgy but speeds the movement of the player
    public void consumeRunning()
    {
        stamina.player_total_energy -= stamina.runningEnergy * stamina.consumption_speed * Time.deltaTime;
        //stamina.player_total_energy = Mathf.Clamp(stamina.player_total_energy, 0.0f, 100.0f);
        //Debug.Log($"Player is running!");
    }

    private void RegenerateStamina()
    {
        stamina.player_total_energy += stamina.regenaration_rate * stamina.regeneration_speed * Time.deltaTime;
        stamina.player_total_energy = Mathf.Clamp(stamina.player_total_energy, 0.0f, 100.0f);
    }
}
