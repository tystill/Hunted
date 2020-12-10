using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public pickRandom RandomGenerator;
    public NavMeshSurface Surface;
    public GameObject[] Doors = new GameObject[4];
    public Player Player;
    private Vector3 playerStartingPosition;
    public Text Message;
    public Text Lives;
    public Text Level;

    public GameObject Pillar;
    public GameObject[] Pillars;

    private int level = 1;


    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject pillar in Pillars)
        {
            Instantiate(pillar);
        }
        PlacePillars();
        GenerateLevel();
        playerStartingPosition = Player.transform.position;
        Message.CrossFadeAlpha(0f, 0f, false);


    }

    // Update is called once per frame
    void Update()
    {
        Lives.text = "Lives: " + Player.lives;
        Level.text = "Level " + level;
        if(Player.lives < 1)
        {
            ResetLevel();
        }



    }


    public void NextLevel()
    {
        //generate level and increment
        GenerateLevel();

        Player.lives = 3;
        level++;

        //display success
        //Debug.Log("Level Beaten");
        Message.text = "Level Cleared";
        Message.CrossFadeAlpha(1f, 0f, false);
        Message.CrossFadeAlpha(0f, 3f, false);




    }


    private void ResetLevel()
    {
        //generate level
        GenerateLevel();

        //display death
        //Debug.Log("Ran Out of Lives");
        Message.text = "Out of Lives";
        Message.CrossFadeAlpha(1f, 0f, false);
        Message.CrossFadeAlpha(0f, 3f, false);
        Player.lives = 3;


    }

    private void GenerateLevel()
    {

        ResetPosition();

        //reset the level if one exists
        if (RandomGenerator.MazeInstances[0])
        {
            foreach (GameObject maze in RandomGenerator.MazeInstances)
            {
                Destroy(maze);
            }
            foreach (GameObject door in Doors)
            {
                door.SetActive(true);
            }
        }

        int rightDoor = Random.Range(0, 4);

        for (int i = 0; i < 4; i++)
        {
            if (i == rightDoor)
            {
                Doors[i].SetActive(false);

            }
        }
        RandomGenerator.GenerateLevel();

        Surface.RemoveData();
        Surface.BuildNavMesh();

    }

    public void ResetPosition()
    {
        //reset player position
        Player.controller.enabled = false;
        Player.transform.position = playerStartingPosition;
        Player.controller.enabled = true;
    }



    private void PlacePillars()
    {


        for(int i = 1; i < 20; i++)
        {

            for(int j = 1; j < 20; j++)
            {
                //(6f * i + 6, -0.835f, 6f * j + 2) for shiny pillars
                //(6f * i + 6, -0.3f, 6f * j + 2) for marble pillars
                //Euler(0, Random.Range(0f, 360f), 0) for random rotation

                Instantiate(Pillar, new Vector3(6f * i + 5, -0.835f, 6f * j + 1), Quaternion.identity);
                Instantiate(Pillar, new Vector3(-6f * i - 1, -0.835f, 6f * j + 1), Quaternion.identity);
                Instantiate(Pillar, new Vector3(-6f * i - 1, -0.835f, -6f * j - 5), Quaternion.identity);
                Instantiate(Pillar, new Vector3(6f * i + 5, -0.835f, -6f * j - 5), Quaternion.identity);


            }
        }
    }

}
