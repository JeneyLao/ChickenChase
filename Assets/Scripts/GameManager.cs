using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public class GameManager : MonoBehaviour {

    public enum GAMEMODE
    {
        NONE,
        PLAYING,
        PAUSE
    }

    public static GameManager instance;
    string currentState;

    static int score;
    static int numberOfTurns;

    string currentDirection;

    GameObject currentEgg;
    List<String> dir;
    List<Vector2> pos;

    List<List<int>> grid;
    List<int> c;
    float pixel = 0.25f;
    int row = 28;
    int col = 49;

    public GameObject egg;
    public GameObject chicken;
    public GameObject hay;

    int cx;
    int cy;

    int prevX;
    int prevY;



    int count = 0;
    int numberOfEggs = 0;
    int targetEggs = 1;

    int level = 9;
    int count3 = 0;

    bool startSpawnEgg = true;
    GameObject[] gameObjects;

    public GameObject cow;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

	// Use this for initialization
	void Start () 
    {

        Debug.Log("GAMEMANAGER START");

        currentState = "INSTRUCTIONS";
        GameObject.FindGameObjectWithTag("NextLevel").transform.position = new Vector3(
            GameObject.FindGameObjectWithTag("NextLevel").transform.position.x,
            GameObject.FindGameObjectWithTag("NextLevel").transform.position.y,
            -6);
        numberOfTurns = 0;


        currentEgg = GameObject.FindGameObjectWithTag("Chicken");

        grid = new List<List<int>>();
        c = new List<int>();

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                c.Add(0);
            }
            grid.Add(c);
            c = new List<int>();
        }

       // Debug.Log(grid.Count);


        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                //Vector3 pos1 = new Vector3(j*pixel, i*pixel, 1f);
                //Instantiate(egg, pos1, transform.rotation);

                if (j == col / 2 && i == row / 2)
                {
                    Vector2 pos2 = new Vector2(j * pixel, i * pixel);
                    chicken.transform.position = pos2;
                    //chicken.transform.position = pos2;
                    cx = j;
                    cy = i;
                }
            }
        }

	}

    // Update is called once per frame
    void Update() 
    {
        if (Application.loadedLevelName != "GameOver" && Application.loadedLevelName != "Start" && Application.loadedLevelName != "Start2" && currentState == "INSTRUCTIONS")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentState = "PLAYING";
                GameObject.FindGameObjectWithTag("NextLevel").transform.position = new Vector3(
                    GameObject.FindGameObjectWithTag("NextLevel").transform.position.x,
                    GameObject.FindGameObjectWithTag("NextLevel").transform.position.y,
                    10);
                GameObject.FindGameObjectWithTag("LevelInstruction").transform.position = new Vector3(
                    GameObject.FindGameObjectWithTag("LevelInstruction").transform.position.x,
                    GameObject.FindGameObjectWithTag("LevelInstruction").transform.position.y,
                    GameObject.FindGameObjectWithTag("LevelInstruction").transform.position.z); 
                //Debug.Log("Current sTate: " + currentState);
            }
        }
        else
        {
            if (Application.loadedLevelName == "GameOver")
            {
                //Debug.Log("Destroyed1");
                Destroy(this.gameObject);
                //Debug.Log("Destroyed2222");
            }

            if (count == 5)
            {
                count = 0;
            }
            count++;

            if (numberOfEggs == targetEggs)
            {
                startSpawnEgg = true;
                reset();
                //if (Application.loadedLevelName == "Level1")
                if (level == 1)
                {
                    targetEggs = 5;

                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            if ((i == row / 4) && j > 10 && j < 40)
                            {
                                Vector3 pos1 = new Vector3(j * pixel, i * pixel, 0f);
                                Instantiate(hay, pos1, transform.rotation);
                                changeGrid(j, i, 1);
                            }
                        }
                    }
                }

                else if (level == 2)
                {
                    DestroyAllObjects("Hay");
                    targetEggs = 5;

                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            if ((i == row / 4 || i == row * 3 / 4) && j > 10 && j < 40)
                            {
                                Vector3 pos1 = new Vector3(j * pixel, i * pixel, 0f);
                                Instantiate(hay, pos1, transform.rotation);
                                changeGrid(j, i, 1);
                            }
                        }
                    }
                }
                else if (level == 3)
                {
                    DestroyAllObjects("Hay");
                    targetEggs = 10;
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            if ((i == row * 3/4 || i == row / 4) && j > 20 && j < 30)
                            {
                                Vector3 pos1 = new Vector3(j * pixel, i * pixel, 0f);
                                Instantiate(hay, pos1, transform.rotation);
                                changeGrid(j, i, 1);
                            }
                            if ((j == col * 3 / 4 || j == col / 4) && i > 10 && i < 18)
                            {
                                Vector3 pos1 = new Vector3(j * pixel, i * pixel, 0f);
                                Instantiate(hay, pos1, transform.rotation);
                                changeGrid(j, i, 1);
                            }
                        }
                    }
                }
                else if (level == 4)
                {
                    DestroyAllObjects("Hay");
                    targetEggs = 10;

                    Vector3 pos1 = new Vector3(5 * pixel, 5 * pixel, 0f);
                    GameObject cow1 = Instantiate(cow, pos1, transform.rotation);
                    cow1.GetComponent<CharacterAnimations>().setVariables(1, 0, 500, 0.02f, false);

                    Vector3 pos2 = new Vector3(44 * pixel, 23 * pixel, 0f);
                    GameObject cow2 = Instantiate(cow, pos2, transform.rotation);
                    cow2.GetComponent<CharacterAnimations>().setVariables(1, 0, 500, 0.02f, true);
                }
                else if (level == 5)
                {
                    DestroyAllObjects("Cow");
                    targetEggs = 10;

                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            if ((j == col * 3 / 4 || j == col / 4) && i > 10 && i < 18)
                            {
                                Vector3 pos0 = new Vector3(j * pixel, i * pixel, 0f);
                                Instantiate(hay, pos0, transform.rotation);
                                changeGrid(j, i, 1);
                            }
                        }
                    }

                    Vector3 pos1 = new Vector3(5 * pixel, 5 * pixel, 0f);
                    GameObject cow1 = Instantiate(cow, pos1, transform.rotation);
                    cow1.GetComponent<CharacterAnimations>().setVariables(1, 0, 500, 0.02f, false);

                    Vector3 pos2 = new Vector3(44 * pixel, 23 * pixel, 0f);
                    GameObject cow2 = Instantiate(cow, pos2, transform.rotation);
                    cow2.GetComponent<CharacterAnimations>().setVariables(1, 0, 500, 0.02f, true);
                }
                else if (level == 6)
                {
                    DestroyAllObjects("Hay");
                    DestroyAllObjects("Cow");
                    targetEggs = 15;

                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            if ((i == row / 4 || i == row * 3 / 4) && j > 10 && j < 40)
                            {
                                Vector3 pos1 = new Vector3(j * pixel, i * pixel, 0f);
                                Instantiate(hay, pos1, transform.rotation);
                                changeGrid(j, i, 1);
                            }
                        }
                    }

                    Vector3 pos2 = new Vector3(5 * pixel, 5 * pixel, 0f);
                    GameObject cow1 = Instantiate(cow, pos2, transform.rotation);
                    cow1.GetComponent<CharacterAnimations>().setVariables(0, 1, 170, 0.03f, false);

                    Vector3 pos3 = new Vector3(44 * pixel, 5 * pixel, 0f);
                    GameObject cow2 = Instantiate(cow, pos3, transform.rotation);
                    cow2.GetComponent<CharacterAnimations>().setVariables(0, 1, 170, 0.03f, false);
                }
                else if (level == 7)
                {
                    DestroyAllObjects("Hay");
                    DestroyAllObjects("Cow");
                    targetEggs = 15;

                    Vector3 pos0 = new Vector3(5 * pixel, 5 * pixel, 0f);
                    GameObject cow0 = Instantiate(cow, pos0, transform.rotation);
                    cow0.GetComponent<CharacterAnimations>().setVariables(1, 0, 500, 0.02f, false);

                    Vector3 pos1 = new Vector3(44 * pixel, 10 * pixel, 0f);
                    GameObject cow1 = Instantiate(cow, pos1, transform.rotation);
                    cow1.GetComponent<CharacterAnimations>().setVariables(1, 0, 500, 0.02f, true);

                    Vector3 pos2 = new Vector3(5 * pixel, 18 * pixel, 0f);
                    GameObject cow2 = Instantiate(cow, pos2, transform.rotation);
                    cow2.GetComponent<CharacterAnimations>().setVariables(1, 0, 500, 0.02f, false);

                    Vector3 pos3 = new Vector3(44 * pixel, 23 * pixel, 0f);
                    GameObject cow3 = Instantiate(cow, pos3, transform.rotation);
                    cow3.GetComponent<CharacterAnimations>().setVariables(1, 0, 500, 0.02f, true);
                
                }
                else if (level == 8)
                {
                    DestroyAllObjects("Hay");
                    DestroyAllObjects("Cow");
                    targetEggs = 15;

                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            if ((i == row / 3 || i == row * 2 / 3) && ((j > 2 && j < 12) ||
                                (j > 36 && j < 46)))
                            {
                                Vector3 pos1 = new Vector3(j * pixel, i * pixel, 0f);
                                Instantiate(hay, pos1, transform.rotation);
                                changeGrid(j, i, 1);
                            }
                            if ((j == 16 || j == 32) && ((i > 0 && i < 8) || (i > 19 && i < 27)))
                            {
                                Vector3 pos1 = new Vector3(j * pixel, i * pixel, 0f);
                                Instantiate(hay, pos1, transform.rotation);
                                changeGrid(j, i, 1);
                            }
                        }
                    }

                    Vector3 pos2 = new Vector3(14 * pixel, 23 * pixel, 0f);
                    GameObject cow1 = Instantiate(cow, pos2, transform.rotation);
                    cow1.GetComponent<CharacterAnimations>().setVariables(0, 1, 200, 0.02f, true);
                    Vector3 pos3 = new Vector3(17 * pixel, 5 * pixel, 0f);
                    GameObject cow2 = Instantiate(cow, pos3, transform.rotation);
                    cow2.GetComponent<CharacterAnimations>().setVariables(0, 1, 200, 0.02f, false);
                    Vector3 pos4 = new Vector3(31 * pixel, 23 * pixel, 0f);
                    GameObject cow3 = Instantiate(cow, pos4, transform.rotation);
                    cow3.GetComponent<CharacterAnimations>().setVariables(0, 1, 200, 0.02f, true);
                    Vector3 pos5 = new Vector3(33 * pixel, 5 * pixel, 0f);
                    GameObject cow4 = Instantiate(cow, pos5, transform.rotation);
                    cow4.GetComponent<CharacterAnimations>().setVariables(0, 1, 200, 0.02f, false);
                }
                else if (level == 9)
                {
                    DestroyAllObjects("Hay");
                    DestroyAllObjects("Cow");
                    targetEggs = 20;

                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            if ((i == row / 3 || i == row * 2 / 3) && ((j > 2 && j < 12) || (j > 20 && j < 28) ||
                                (j > 36 && j < 46)))
                            {
                                Vector3 pos1 = new Vector3(j * pixel, i * pixel, 0f);
                                Instantiate(hay, pos1, transform.rotation);
                                changeGrid(j, i, 1);
                            }
                            if ((j == 16 || j == 32) && ((i > 0 && i < 8) || (i > 10 && i < 17) || (i > 19 && i < 27)))
                            {
                                Vector3 pos1 = new Vector3(j * pixel, i * pixel, 0f);
                                Instantiate(hay, pos1, transform.rotation);
                                changeGrid(j, i, 1);
                            }
                        }
                    }

                    Vector3 pos3 = new Vector3(17 * pixel, 5 * pixel, 0f);
                    GameObject cow2 = Instantiate(cow, pos3, transform.rotation);
                    cow2.GetComponent<CharacterAnimations>().setVariables(0, 1, 170, 0.03f, false);
                    Vector3 pos4 = new Vector3(31 * pixel, 23 * pixel, 0f);
                    GameObject cow3 = Instantiate(cow, pos4, transform.rotation);
                    cow3.GetComponent<CharacterAnimations>().setVariables(0, 1, 170, 0.03f, true);
                }

                else if (level == 10)
                {
                    Debug.Log("You win!");
                }

                DestroyAllObjects("N");
                level++;
            }
        }
	}

    void DestroyAllObjects(string t)
    {
        gameObjects = GameObject.FindGameObjectsWithTag(t);

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }

    public bool returnStartSpawnEgg()
    {
        return startSpawnEgg;
    }


    public int returnLevel()
    {
        return level;
    }

    public int returnCount()
    {
        return count;
    }

    public void reset()
    {
        //Debug.Log("RESET");
        //Debug.Log("LOADED LEVEL NAME: " + Application.loadedLevelName);
        currentState = "INSTRUCTIONS";
        GameObject.FindGameObjectWithTag("NextLevel").transform.position = new Vector3(
    GameObject.FindGameObjectWithTag("NextLevel").transform.position.x,
    GameObject.FindGameObjectWithTag("NextLevel").transform.position.y,
    -6);
        numberOfEggs = 0;

        currentEgg = GameObject.FindGameObjectWithTag("Chicken");
        //Debug.Log("This is in reset: " + currentEgg.gameObject.name);

        grid = new List<List<int>>();
        c = new List<int>();

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                c.Add(0);
            }
            grid.Add(c);
            c = new List<int>();
        }

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {

                if (j == col / 2 && i == row / 2)
                {
                    Vector2 pos2 = new Vector2(j * pixel, i * pixel);
                    //Debug.Log("MOVED PLAYER!");
                    GameObject.Find("Chicken").transform.position = pos2;
                    cx = j;
                    cy = i;
                }
            }
        }
    }

    public int returnNumberOfEggs()
    {
        return numberOfEggs;
    }

    public void printGrid()
    {
       // Debug.Log("Hey");
        for (int i = 0; i < row; i++)
        {
        //    Debug.Log("NOOO");
            for (int j = 0; j < col; j++)
            {
          //      Debug.Log(grid[i][j]);
            }
        }
    }

    public int returnTargetEggs()
    {
       // Debug.Log("Return Target Eggs");
        return targetEggs;
    }

    public Vector2 returnPlayerXY()
    {
       // Debug.Log("returnPlayerXY");
        return new Vector2(cx, cy);
    }

    public void resetPoints()
    {
       // Debug.Log("Reset Points");
        numberOfTurns = 0;
        score = 0;
        targetEggs = 5;
    }

    public void move(GameObject go, string direction)
    {
       // Debug.Log("GAMEMANAGER MOVEeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee!!");
        prevX = cx;
        prevY = cy;
        switch (direction)
        {
            case "UP":
                Vector2 posUP = new Vector2(cx * pixel, (cy+1) * pixel);
                go.transform.position = posUP;
                if (cy > 26)
                {
                    //Debug.Log("YOU LOST");
                    reset();
                    Application.LoadLevel("GameOver");
                }
                //Instantiate(chicken, posUP, transform.rotation);
                changeGrid(cx, cy, 0);
                cy++;
                changeGrid(cx, cy, 1);
                break;
            case "LEFT":
                Vector2 posLEFT = new Vector2((cx-1) * pixel, cy * pixel);
                go.transform.position = posLEFT;
                if (cx < 1)
                {
                  //  Debug.Log("YOU LOST");
                    reset();
                    Application.LoadLevel("GameOver");
                }
                //Instantiate(chicken, posLEFT, transform.rotation);
                changeGrid(cx, cy, 0);
                cx--;
                changeGrid(cx, cy, 1);

                break;
            case "DOWN":
                
                Vector2 posDOWN = new Vector2(cx * pixel, (cy-1) * pixel);
                go.transform.position = posDOWN;
                if (cy < 1)
                {
                    //Debug.Log("YOU LOST");
                    reset();
                    Application.LoadLevel("GameOver");
                }
                //Instantiate(chicken, posDOWN, transform.rotation);
                changeGrid(cx, cy, 0);

                cy--;
                changeGrid(cx, cy, 1);

                break;
            case "RIGHT":
                Vector2 posRIGHT = new Vector2((cx+1) * pixel, cy * pixel);
                go.transform.position = posRIGHT;
                if (cx > 47)
                {
                   // Debug.Log("YOU LOST");
                    reset();
                    Application.LoadLevel("GameOver");
                }
                //Instantiate(chicken, posRIGHT, transform.rotation);
                changeGrid(cx, cy, 0);

                cx++;
                changeGrid(cx, cy, 1);

                break;
        }
    }

    public void changeGrid(int x, int y, int t)
    {
        
        grid[y][x] = t;
    }

    public int returnGridNum(int x, int y)
    {
        return grid[y][x];
    }

    public Vector2 returnPreviousPosition()
    {
        return new Vector2(prevX, prevY);
    }

    public string getCurrentState()
    {
        return currentState;
    }

    public void setCurrentState(string newCurrentState)
    {
        currentState = newCurrentState;
    }

    public int returnScore()
    {
        return score;
    }

    public void increaseScore()
    {
        Debug.Log("PLAY SOUND!!");
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        if (numberOfEggs+1 == targetEggs)
            startSpawnEgg = false;
        score++;
        numberOfEggs++;
        Debug.Log(score);
    }
    
    public void setCurrentEgg(GameObject go)
    {
       // Debug.Log("Set Current Egg");
        currentEgg = go;
    }

    public GameObject getCurrentEgg()
    {
       // Debug.Log("Get Current Egg");
        return currentEgg;
    }

    public Vector2 getCurrentEggPos()
    {
        //Debug.Log("Get Current Egg Pos");
        return currentEgg.transform.position;
    }

    public string returnCurrentTag()
    {
       // Debug.Log("Return Current Tag");
       // Debug.Log(currentEgg.gameObject);
        if (currentEgg.gameObject == null)
        {
            currentEgg = GameObject.FindGameObjectWithTag("Chicken");
        }
        //Debug.Log(currentEgg.gameObject);
      //  Debug.Log("Done");
        return currentEgg.gameObject.name;
    }

    public void setCurrentDirection(string d)
    {
        currentDirection = d;
    }
    public string getCurrentDirection()
    {
        return currentDirection;
    }

    public void increaseNumberOfTurns()
    {
        numberOfTurns++;
    }

    public int returnNumberOfTurns()
    {
        return numberOfTurns;
    }
}
