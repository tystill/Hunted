using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class pickRandom : MonoBehaviour
{


    public GameObject[] Mazes;
    public GameObject[] MazeInstances = new GameObject[4];




    public void GenerateLevel() 
    {

        int maze1 = Random.Range(0, Mazes.Length);
        int maze2 = Random.Range(0, Mazes.Length);
        int maze3 = Random.Range(0, Mazes.Length);
        int maze4 = Random.Range(0, Mazes.Length);

        MazeInstances[0] = Instantiate(Mazes[maze1], new Vector3(-1, 0, 1), Quaternion.Euler(-90, 0, 0));
        MazeInstances[1] = Instantiate(Mazes[maze2], new Vector3(5, 0, 1), Quaternion.Euler(-90, 0, 90));
        MazeInstances[2] = Instantiate(Mazes[maze3], new Vector3(5, 0, -5), Quaternion.Euler(-90, 0, 180));
        MazeInstances[3] = Instantiate(Mazes[maze4], new Vector3(-1, 0, -5), Quaternion.Euler(-90, 0, 270));


        

    }


}
