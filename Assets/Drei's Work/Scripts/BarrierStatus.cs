using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BarrierStatus : MonoBehaviour
{
    public GameObject barrierObject;
    //Animator component of barricade
    private Animator barrierAnim;

    public Text healthDisplay;
    public Image imageFill;
    public float health = 100;
    public AudioSource barrierBreak;
    public AudioSource enemyTrap;
    public AudioSource enemySounds;
    public GameObject particleEffect;

    bool barrierBroken = false;
    bool once = false;

    // Start is called before the first frame update
    void Start()
    {
        enemySounds = this.GetComponent<AudioSource>();
        barrierAnim = barrierObject.GetComponent<Animator>();
        TimeManager.Instance.startStopWatch();
    }

    // Update is called once per frame
    void Update()
    {
        callAnimations();

        if (health > 50.0f)
            imageFill.color = Color.green;
        else if (health <= 50.0f && health > 25.0f)
            imageFill.color = Color.yellow;
        else if (health <= 25.0f)
            imageFill.color = Color.red;

        if (health == 0.0f && barrierBreak.isPlaying == false && barrierBroken == true && once == false)
        {
            barrierBreak.Play();
            barrierBroken = false;
            once = true;
        } 
        if (health <= 0.0f && !Input.GetMouseButton(0))
        {
            this.barrierObject.SetActive(false);
            health = 0.0f;
            barrierBroken = true;
        }

        imageFill.fillAmount = health / 100;
        healthDisplay.text = health.ToString("0.0") + "%";
    }

    private void callAnimations()
    {
        //update the percentage of the barrier's health to the animator
        barrierAnim.SetFloat("Percentage", health);
    }
}
