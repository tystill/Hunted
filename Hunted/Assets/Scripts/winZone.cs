using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winZone : MonoBehaviour
{

    public GameController GameController;
    


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {

            StartCoroutine(GameController.NextLevel());
        }


    }
}
