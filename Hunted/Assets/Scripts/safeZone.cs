using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safeZone : MonoBehaviour
{

    public Enemy[] Enemies;


    private void OnTriggerStay(Collider other)
    {

        if(other.tag == "Player")
        {
            //Debug.Log("Reset enemy ");

            foreach (Enemy enemy in Enemies)
            {
                enemy.ResetPosition();
            }
        }

    }



}
