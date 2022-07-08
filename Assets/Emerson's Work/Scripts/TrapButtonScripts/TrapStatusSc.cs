using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStatusSc : MonoBehaviour
{
    //Cooldown timer properties
    [SerializeField] private float trap_cooldown = 30.0f;
    [HideInInspector] public float cooldown_ticks = 0.0f;

    //Status and effect properties
    [HideInInspector] public bool isAvailable = true;
    public float dispel_duration = 5.0f;
    public float health_usage_threshold = 50.0f;

    //Animator component
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAvailable)
        {
            anim.SetBool("isCooldown", true);
            cooldown_ticks += Time.deltaTime;
            if(cooldown_ticks >= trap_cooldown)
            {
                isAvailable = true;
                cooldown_ticks = 0.0f;
                anim.SetBool("isCooldown", false);
                /*
                 * Can add a ticking sound when its available again
                 * */
            }
        }
    }
}
