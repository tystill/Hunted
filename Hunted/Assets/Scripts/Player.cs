using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public GameController GameController;

    private float speed = 4f;
    private Vector3 move;

    private float gravity = -9.81f;
    private Vector3 velocity;


    public int lives = 3;
    public Text Message;


    private void Start()
    {



    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            speed = 6f;
        }
        else
        {
            speed = 4f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        
    }


    public IEnumerator Die()
    {
        Debug.Log("You Died");
        
        GameController.ResetPosition();

        //decrease lives
        lives--;
        //check if lives are 0 in game controller
        if (lives > 0)
        {
            Message.text = "You Died";
            Message.enabled = true;
            yield return new WaitForSeconds(3f);
            Message.enabled = false;
        }
        yield return null;

    }




}
