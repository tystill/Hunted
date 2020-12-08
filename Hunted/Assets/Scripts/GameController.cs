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

    private int level = 1;


    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
        playerStartingPosition = Player.transform.position;
        Message.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        Lives.text = "Lives: " + Player.lives;
        Level.text = "Level " + level;
        if(Player.lives < 1)
        {
            StartCoroutine(ResetLevel());
        }



    }


    public IEnumerator NextLevel()
    {
        //generate level and increment
        GenerateLevel();

        Player.lives = 3;
        level++;

        //display success
        //Debug.Log("Level Beaten");
        Message.text = "Level Cleared";
        Message.enabled = true;
        yield return new WaitForSeconds(3f);
        Message.enabled = false;




    }


    IEnumerator ResetLevel()
    {
        //generate level
        GenerateLevel();

        //display death
        //Debug.Log("Ran Out of Lives");
        Message.text = "Out of Lives";
        Message.enabled = true;
        yield return new WaitForSeconds(3f);
        Message.enabled = false;
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



}
