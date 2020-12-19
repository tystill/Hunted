using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealMaze : MonoBehaviour
{
    public pickRandom MazeGenerator;
    private int index;


    private void Start()
    {

        if(transform.position.x < 0f)
        {
            if(transform.position.z > 0f)
            {
                index = 0;
            }
            else
            {
                index = 3;
            }
        }
        else
        {
            if(transform.position.z > 0f)
            {
                index = 1;
            }
            else
            {
                index = 2;
            }
        }


    }


    private void OnTriggerEnter(Collider other)
    {


        if(other.tag == "Player")
        {
            MazeGenerator.MapMaze[index].SetActive(true);

        }
    }

}
