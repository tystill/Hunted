using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
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
    public Text PauseLives;
    public Text PauseLevel;
    public Text Deaths;
    public int deaths;
    public Button BackButton;
    public Slider Slider;
    public Text SliderValue;

    public GameObject Pillar;
    public GameObject[] Pillars;

    public GameObject PauseMenu;
    private bool isPaused;
    public GameObject AudioListener;

    public float MaxStamina = 6f;
    private int level = 1;

    private int charges = 4;
    private int lightsPosition = 0;
    private GameObject[] Lights = new GameObject[4];
    public GameObject MagicLight;
    private float lightTimer = 0f;
    private bool flag = false; 


    // Start is called before the first frame update
    void Start()
    {
        
        deaths = 0;
        foreach(GameObject pillar in Pillars)
        {
            Instantiate(pillar);
        }
        PlacePillars();
        GenerateLevel();
        Surface.BuildNavMesh();
        playerStartingPosition = Player.transform.position;
        Message.CrossFadeAlpha(0f, 0f, false);
        isPaused = false;

    }

    // Update is called once per frame
    void Update()
    {
        Lives.text = "Lives: " + Player.lives;
        Level.text = "Level " + level;
        Deaths.text = "Deaths: " + deaths;
        SliderValue.text = "Mouse Sensitivity: " + (int)(Slider.value*100);
        PauseLives.text = Lives.text;
        PauseLevel.text = Level.text;


        if (Player.lives < 1)
        {
            ResetLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (BackButton)
            {
                BackButton.onClick.Invoke();
            }
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            ActivatePauseMenu();
        }

        else
        {
            DeactivatePauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.E) && lightTimer > 4f && !isPaused)
        {
            SpawnLight();
        }
        lightTimer += Time.deltaTime;

        if (flag)
        {
            Surface.BuildNavMesh();
            flag = false;
        }
    }


    private void ActivatePauseMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        AudioListener.SetActive(false);
    }

    public void DeactivatePauseMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        AudioListener.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
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
        Surface.RemoveData();


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
        if (Lights[0])
        {
            foreach(GameObject Light in Lights)
            {
                Destroy(Light);
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
        flag = true;

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


    public void SpawnLight()
    {
        lightTimer = 0f;
        Debug.Log("Spawning Light, lightTimer: " + lightTimer);
        //Destroy old light if applicable
        if (Lights[lightsPosition])
        {
            Destroy(Lights[lightsPosition]);
        }
        Lights[lightsPosition] = Instantiate(MagicLight, Player.transform.position + new Vector3(Player.transform.forward.x, 1.03f, Player.transform.forward.z), Player.transform.rotation);
        lightsPosition = (lightsPosition + 1) % charges;


    }

}
