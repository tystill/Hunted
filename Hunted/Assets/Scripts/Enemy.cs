using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameController GameController;
    public Animator Zombie;
    [SerializeField] private Vector3 target;
    private Vector3 startingPosition;
    public Player Player;
    public NavMeshAgent NavMeshAgent;
    


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;

        
    }

    public void SetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;


    }


    public void ResetPosition()
    {
        NavMeshAgent.enabled = false;
        transform.position = startingPosition;
        NavMeshAgent.enabled = true;
        target = startingPosition;

    }

    // Update is called once per frame
    void Update()
    {

        NavMeshAgent.SetDestination(target);


    }



    private IEnumerator OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player")
        {


            //Debug.Log("kill player");
            Player.controller.enabled = false;
            yield return new WaitForSeconds(0.2f);
            NavMeshAgent.enabled = false;
            Zombie.SetBool("Attack", true);

            yield return new WaitForSeconds(1f);

            Player.Die();
            Player.transform.position = GameController.playerStartingPosition;
            Player.controller.enabled = true;
            NavMeshAgent.enabled = true;

            Zombie.SetBool("Attack", false);

        }

    }


}
