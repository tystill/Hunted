using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class pickRandom : MonoBehaviour
{


    public GameObject[] Mazes;
    public NavMeshSurface Surface;

    // Start is called before the first frame update
    void Start() 
    {
        //Random rand = new Random();
        int num = Random.Range(0, Mazes.Length);

        Mazes[num].SetActive(true);

        Surface.BuildNavMesh();        
    }


}
