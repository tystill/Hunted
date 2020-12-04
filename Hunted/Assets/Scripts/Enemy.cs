using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{


    public float DeathDistance = 100f;
    public float DistanceAway;
    public Transform thisObject;
    public Transform target;
    private NavMeshAgent navComponent;


    // Start is called before the first frame update
    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player").transform;
        navComponent = gameObject.GetComponent<NavMeshAgent>();

        
    }

    // Update is called once per frame
    void Update()
    {


        float dist = Vector3.Distance(target.position, transform.position);

        if (target)
        {
            navComponent.SetDestination(target.position);
        }
        else
        {
            if(target == null)
            {
                target = gameObject.GetComponent<Transform>();
            }
            else
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        /*if(dist <= DeathDistance)
        {

            Debug.Log("kill player");
            //Application.Quit();
        }*/



        
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("kill player");

            //Application.Quit();
        }


    }
}
