using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public GameController GameController;
    public Animator PlayerController;

    private float speed = 4f;
    private Vector3 move;
    public bool moving = false;

    private float gravity = -9.81f;
    private Vector3 velocity;


    public int lives = 3;
    public Text Message;

    private float stamina;

    public Slider Stamina;

    private bool CheatMode = false;

    public GameObject Walking;
    public GameObject Running;


    private void Start()
    {
        stamina = GameController.MaxStamina;
        Stamina.maxValue = GameController.MaxStamina;
        

    }


    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space) && stamina > 0 && !CheatMode)
        {
            if (x != 0 || z != 0)
            {
                speed = 6f;
                stamina -= Time.deltaTime * 2;
            }

        }
        else if(Input.GetKey(KeyCode.Space) && CheatMode)
        {
            speed = 6f;
        }
        else
        {
            speed = 4f;
            if (!Input.GetKey(KeyCode.Space))
            {
                stamina += Time.deltaTime;
            }
        }


        if (x == 0 && z == 0)
        {
            moving = false;
            stamina += Time.deltaTime;
        }
        else
        {
            moving = true;
        }


        if (stamina > GameController.MaxStamina)
        {
            stamina = GameController.MaxStamina;
        }
        //Debug.Log("speed: " + speed + " stamina: " + stamina);
        Stamina.value = stamina;


        move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        PlayerController.SetBool("Walking", moving);
        if (moving && !GameController.isPaused)
        {
            if(speed == 6f)
            {
                Running.SetActive(true);
                Walking.SetActive(false);
            }
            else
            {
                Running.SetActive(false);
                Walking.SetActive(true);
            }
        }
        else
        {
            Running.SetActive(false);
            Walking.SetActive(false);
        }

    }



    public void Die()
    {
        //Debug.Log("You Died");
        
        //GameController.ResetPosition();
        GameController.deaths++;

        //decrease lives
        lives--;
        stamina = GameController.MaxStamina;
        transform.rotation = Quaternion.identity;
        //check if lives are 0 in game controller
        if (lives > 0)
        {
            Message.text = "You Died";
            Message.CrossFadeAlpha(1f, 0f, false);
            Message.CrossFadeAlpha(0f, 3f, false);
            //Message.enabled = false;
        }

    }

    public void SetCheatMode()
    {
        CheatMode = !CheatMode;
    }


}
