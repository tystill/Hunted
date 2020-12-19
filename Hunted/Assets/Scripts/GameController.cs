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
    //public Enemy[] Zombies;
    public Vector3 playerStartingPosition;
    public Text Message;
    public Text Lives;
    public Text Level;
    public Text PauseLives;
    public Text PauseLevel;
    public Text Deaths;
    public Text TotalDeaths;
    public int deaths;
    public Button BackButton;
    public Slider Slider;
    public Text SliderValue;

    public GameObject Pillar;
    public GameObject[] Pillars;
    public GameObject MapWall;

    public GameObject PauseMenu;
    public bool isPaused;
    public GameObject AudioListener;

    public float MaxStamina = 12f;
    private int level = 1;

    private int charges = 5;
    private int lightsPosition = 0;
    private GameObject[] Lights = new GameObject[5];
    public GameObject MagicLight;
    private float lightTimer = 0f;
    private float holdTimer = 0f;

    public Slider LightSlider;

    public GameObject BeatenWindow;
    //private bool flag = false; 


    // Start is called before the first frame update
    void Start()
    {
        
        deaths = 0;
        foreach(GameObject pillar in Pillars)
        {
            Instantiate(pillar);
        }
        PlacePillars();
        playerStartingPosition = Player.transform.position;
        StartCoroutine(GenerateLevel());
        Message.CrossFadeAlpha(0f, 0f, false);
        isPaused = false;
        /*foreach(Enemy zombie in Zombies)
        {
            zombie.navComponent = gameObject.GetComponent<NavMeshAgent>();

        }*/

    }

    // Update is called once per frame
    void Update()
    {
        Lives.text = "Lives: " + Player.lives;
        Level.text = "Level " + level;
        Deaths.text = "Deaths: " + deaths;
        TotalDeaths.text = "Total Deaths: " + deaths;
        SliderValue.text = "" + (int)(Slider.value*100);
        PauseLives.text = Lives.text;
        PauseLevel.text = Level.text;
        LightSlider.value = holdTimer * 2;
        if(holdTimer > 0)
        {
            LightSlider.gameObject.SetActive(true);
        }
        else
        {
            LightSlider.gameObject.SetActive(false);
        }


        if (Player.lives < 1)
        {
            ResetLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("Escape Pressed");

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

        if (Input.GetKey(KeyCode.E) && lightTimer > 4f && !isPaused && !Player.moving)
        {

            if(holdTimer >= 0.5f)
            {
                SpawnLight();
            }
            holdTimer += Time.deltaTime;


        }
        if (Input.GetKeyUp(KeyCode.E) || Player.moving || isPaused || lightTimer < 4f)
        {
            holdTimer = 0f;
        }

        lightTimer += Time.deltaTime;


    }


    private void ActivatePauseMenu()
    {
        //Debug.Log("Pausing");
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        PauseMenu.SetActive(true);

    }

    public void DeactivatePauseMenu()
    {
        BackButton.onClick.Invoke();

        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }


    public void NextLevel()
    {
        //generate level and increment
        StartCoroutine(GenerateLevel());

        Player.lives = 3;
        level++;

        if (level == 20)
        {
            BeatenWindow.SetActive(true);
            isPaused = true;
        }

        //display success
        //Debug.Log("Level Beaten");
        Message.text = "Level Cleared";
        Message.CrossFadeAlpha(1f, 0f, false);
        Message.CrossFadeAlpha(0f, 3f, false);





    }


    private void ResetLevel()
    {
        //generate level
        StartCoroutine(GenerateLevel());

        //display death
        //Debug.Log("Ran Out of Lives");
        Message.text = "Out  of  Lives";
        Message.CrossFadeAlpha(1f, 0f, false);
        Message.CrossFadeAlpha(0f, 3f, false);
        Player.lives = 3;


    }

    private IEnumerator GenerateLevel()
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
        yield return new WaitForEndOfFrame();
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

        for(int i = 0; i < 20; i++)
        {
            Instantiate(MapWall, new Vector3(65.5f, 12.5f, 6 * i + 7), Quaternion.identity);
            Instantiate(MapWall, new Vector3(6 * i + 11, 12.5f, 61.5f), Quaternion.Euler(0, 90, 0));

            Instantiate(MapWall, new Vector3(65.5f, 12.5f, -6 * i - 11), Quaternion.identity);
            Instantiate(MapWall, new Vector3(6 * i + 11, 12.5f, -65.5f), Quaternion.Euler(0, 90, 0));

            Instantiate(MapWall, new Vector3(-61.5f, 12.5f, -6 * i - 11), Quaternion.identity);
            Instantiate(MapWall, new Vector3(-6 * i - 7, 12.5f, -65.5f), Quaternion.Euler(0, 90, 0));

            Instantiate(MapWall, new Vector3(-61.5f, 12.5f, 6 * i + 7), Quaternion.identity);
            Instantiate(MapWall, new Vector3(-6 * i - 7, 12.5f, 61.5f), Quaternion.Euler(0, 90, 0));
        }


        for(int i = 1; i < 20; i++)
        {



            for (int j = 1; j < 20; j++)
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
        //Debug.Log("Spawning Light, lightTimer: " + lightTimer);
        //Destroy old light if applicable

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance <= 5)
            {
                //Debug.Log("Hit position: " + hit.transform.position);
                if (hit.transform.tag == "Maze" || hit.transform.tag == "Pillar")
                {
                    if (Lights[lightsPosition])
                    {
                        Destroy(Lights[lightsPosition]);
                    }

                    lightTimer = 0f;
                    holdTimer = 0f;
                    Lights[lightsPosition] = Instantiate(MagicLight, hit.point + 0.1f * hit.normal.normalized, Quaternion.identity);
                    lightsPosition = (lightsPosition + 1) % charges;

                }

            }

        }
        //Lights[lightsPosition] = Instantiate(MagicLight, Player.transform.position + new Vector3(Player.transform.forward.x, 1.03f, Player.transform.forward.z), Player.transform.rotation);
        //lightsPosition = (lightsPosition + 1) % charges;


    }

    public void Quit()
    {
        Application.Quit();
    }

    

}
