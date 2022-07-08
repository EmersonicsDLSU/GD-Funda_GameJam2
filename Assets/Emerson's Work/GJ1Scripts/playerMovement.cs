using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    private PlayerStaminaSc playerStamina;

    //Player properties:
    [Header("Player Properties")]
    public float speed = 6.0f;
    public float jumpHeight = 4.0f;

    //character movement coordinates
    [HideInInspector] public float movementX = 0.0f;
    [HideInInspector] public float movementY = 0.0f;

    //Gravity value
    private const float gravity = -9.81f;
    private Vector3 velocity;

    //for Ground Check
    [Space]
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRad = 0.4f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGround;

    //jump properties
    private float jumpTimer = 0.0f;
    private bool canJump = true;

    [Space]
    [Header("Stamina")]
    [SerializeField] private GameObject staminaObject;
    [SerializeField] private Image staminaBar;
    [SerializeField] private Text staminaText;


    [Space]
    [Header("Player Audio")]
    public AudioSource head;
    public List<AudioClip> movementAudio;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerStamina = GetComponent<PlayerStaminaSc>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if the player is on the ground
        isGround = Physics.CheckSphere(groundCheck.position,
         groundCheckRad, groundLayer);

        //sets the velocity to a constant value when player is on the ground
        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Sprint Mechanic
        if (Input.GetKey(KeyCode.LeftShift) && isGround && !playerStamina.isGaspingAir)
        {
            speed = 3.0f;
            //consumes stamina
            playerStamina.consumeRunning();
        }
        else if(!Input.GetKey(KeyCode.LeftShift) || playerStamina.isGaspingAir)
        {
            if (playerStamina.isGaspingAir && head.isPlaying == false)
            {
                head.Play();
                speed = 1.0f;
            }
            else if (!playerStamina.isGaspingAir && head.isPlaying == true)
            {
                head.Stop();
                speed = 1.5f;
            }
            else if (!playerStamina.isGaspingAir && head.isPlaying == false)
            {
                speed = 1.5f;
            }
        }

        //horizontal and forward coordinates
        movementX = Input.GetAxis("Horizontal");
        movementY = Input.GetAxis("Vertical");

        //translates the player
        Vector3 move = transform.right * movementX + transform.forward * movementY;
        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded == true && controller.velocity.magnitude > 2.5f && GetComponent<AudioSource>().isPlaying == false)
        {
            GetComponent<AudioSource>().clip = movementAudio[1];
            GetComponent<AudioSource>().volume = UnityEngine.Random.Range(0.8f, 1.0f);
            GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.8f, 1.1f);
            GetComponent<AudioSource>().Play();

        }
        else if (controller.isGrounded == true && controller.velocity.magnitude > 1.0f && GetComponent<AudioSource>().isPlaying == false)
        {
            GetComponent<AudioSource>().clip = movementAudio[0];
            GetComponent<AudioSource>().volume = UnityEngine.Random.Range(0.8f, 1.0f);
            GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.8f, 1.1f);
            GetComponent<AudioSource>().Play();

        }
        if (controller.isGrounded == true && controller.velocity.magnitude == 0f && GetComponent<AudioSource>().isPlaying == true)
        {
            GetComponent<AudioSource>().clip = null;
            GetComponent<AudioSource>().volume = 1;
            GetComponent<AudioSource>().pitch = 1;
            GetComponent<AudioSource>().Stop();
        }

        //Jumping Mechanic
        if (Input.GetButtonDown("Jump") && isGround && canJump && playerStamina.consumeJumping() && !playerStamina.isGaspingAir)
        {
            Debug.Log("jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            canJump = !canJump;
        }
        else if(!canJump)
        {
            jumpTimer -= Time.deltaTime;
            if(jumpTimer <= 0.0f)
            {
                jumpTimer = 0.0f;
                canJump = !canJump;
            }
        }

        //apply gravity to the player
        velocity.y += 2.0f * gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        staminaBar.fillAmount = playerStamina.stamina.player_total_energy / 100;
        staminaText.text = playerStamina.stamina.player_total_energy.ToString("0") + "%";
    }

}
