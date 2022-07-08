using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrapMechanicSc : MonoBehaviour
{
    //Use for raycasting
    private RaycastHit hit;
    private Ray ray;
    [SerializeField] private Camera cameraobj;

    //script compoent for EnemySpawnHandler
    private EnemyRandomLoc ERL;

    // Start is called before the first frame update
    void Start()
    {
        ERL = GameObject.FindObjectOfType<EnemyRandomLoc>();
        if(cameraobj == null)
        {
            cameraobj = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkTrapMechanic();
    }

    private void checkTrapMechanic()
    {
        if (Physics.Raycast(cameraobj.transform.position, cameraobj.transform.forward, out hit, 5))
        {
            //Checks if its a touch object is a trap cube
            if (Input.GetMouseButtonDown(0) && hit.transform.GetComponent<TrapStatusSc>() != null && hit.transform.GetComponent<TrapStatusSc>().isAvailable)
            {
                TrapStatusSc trap_status = hit.transform.GetComponent<TrapStatusSc>();
                Debug.LogError($"Trap Clicked: {trap_status.transform.name}, Cooldown: {trap_status.cooldown_ticks}");
                //Checks if the intruder is on the place of the recently activated trap
                if (ERL.isAtPlace && hit.transform.parent.tag == ERL.current_loc.transform.tag)
                {
                    BarrierStatus barrier_status = hit.transform.parent.GetComponent<BarrierStatus>();
                    //Checks if the barrier is still under condition, if less than, then the trap can't be used(N/A)
                    if (barrier_status.health < trap_status.health_usage_threshold)
                    {
                        Debug.LogError($"Trap name: {this.transform.name}; Unavailable, low barrier health!");
                        return;
                    }
                    Debug.LogError($"Intruder is stun for {trap_status.dispel_duration} seconds");
                    //Set conditions
                    trap_status.isAvailable = false;
                    //finishes the damaging process and dispels the enemy for a bit
                    ERL.current_damage = ERL.intruder_damage;
                    ERL.increment_relocate_invterval = trap_status.dispel_duration;
                    ERL.current_loc.GetComponent<BarrierStatus>().enemySounds.Stop();
                    ERL.current_loc.GetComponent<BarrierStatus>().enemyTrap.Play();
                    /*
                     * Instantiate a sound here for the stun effect or add activate a UI displaying the stun effect
                     * */

                }
            }
        }
    }
}
