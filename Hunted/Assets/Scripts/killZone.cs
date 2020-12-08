using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killZone : MonoBehaviour
{

    public Enemy Enemy;




    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {
            //Debug.Log("Targeting Player");
            //target player

            Enemy.SetTarget();

        }


    }

    

}
