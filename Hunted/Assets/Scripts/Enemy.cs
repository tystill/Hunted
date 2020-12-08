using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{


    [SerializeField] private Vector3 target;
    private NavMeshAgent navComponent;
    private Vector3 startingPosition;
    public Player Player;
    public NavMeshAgent NavMeshAgent;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        navComponent = gameObject.GetComponent<NavMeshAgent>();

        
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

        navComponent.SetDestination(target);


    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Debug.Log("kill player");
            StartCoroutine(Player.Die());
        }


    }


}
