using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class pickRandom : MonoBehaviour
{


    public GameObject[] Mazes;
    public GameObject[] Door;
    public NavMeshSurface Surface;
    public GameObject Zombie;

    // Start is called before the first frame update
    void Start() 
    {
        int maze1 = Random.Range(0, Mazes.Length);
        int maze2 = Random.Range(0, Mazes.Length);
        int maze3 = Random.Range(0, Mazes.Length);
        int maze4 = Random.Range(0, Mazes.Length);

        Instantiate(Mazes[maze1], Mazes[maze1].transform.position, Quaternion.Euler(-90, 0, 0));
        Instantiate(Mazes[maze2], Mazes[maze2].transform.position + new Vector3(4, 0, 0), Quaternion.Euler(-90, 0, 90));
        Instantiate(Mazes[maze3], Mazes[maze3].transform.position + new Vector3(4, 0, -4), Quaternion.Euler(-90, 0, 180));
        Instantiate(Mazes[maze4], Mazes[maze4].transform.position + new Vector3(0, 0, -4), Quaternion.Euler(-90, 0, 270));

        int rightDoor = Random.Range(0, 4);

        for (int i = 0; i < 4; i++)
        {
            if (i == rightDoor)
            {
                Door[i].SetActive(false);
                Zombie.transform.position = Door[i].transform.position + new Vector3(0, -5, 0);

            }
        }
        

        Surface.BuildNavMesh();        
    }


}
