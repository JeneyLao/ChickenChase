using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
public class Player2 : MonoBehaviour
{

    private GameManager gameManager;

    private Animator animator;
    private int AnimationState;

    private int upIdle = 0;
    private int upWalk = 1;
    private int leftIdle = 2;
    private int leftWalk = 3;
    private int downIdle = 4;
    private int downWalk = 5;
    private int rightIdle = 6;
    private int rightWalk = 7;

    private string target;
    private string direction;

    int count = 0;


    float pixel = 0.25f;

    float cx;
    float cy;

   // public static Player2 instance;
    

    //default is 0.03f
    public float speed;

    
    /*
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
     */
     


    // Use this for initialization
    void Start()
    {
        //Debug.Log("PLAYER START");
        gameManager = GameManager.instance;
        AnimationState = 0;
        animator = this.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName != "GameOver")
        {
            if (gameManager.getCurrentState() == "PLAYING")
            {
                // && gameManager.returnPlayerXY().y*pixel != this.gameObject.transform.position.y
                //gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), 1);
                if (gameManager.getCurrentEgg() != this.gameObject)
                {
                    //Debug.Log((int)gameManager.returnPlayerXY().x + ", " +   (int)gameManager.returnPlayerXY().y);
                    if (Input.GetKeyDown(KeyCode.W) && direction != "DOWN")//&& gameManager.returnGridNum((int)gameManager.returnPlayerXY().x, (int)gameManager.returnPlayerXY().y+1) == 0)
                    {
                        direction = "UP";
                        animator.SetInteger("AnimationState", 1);
                        AnimationState = 1;
                        //   Debug.Log("Pressed W");
                        gameManager.increaseNumberOfTurns();
                        if (gameManager.returnDirCount() > 0 && gameManager.returnListDir()[gameManager.returnDirCount() - 1] != direction && gameManager.returnListDir()[gameManager.returnDirCount() - 1] != "DOWN")
                        {
                            gameManager.addDir(direction, this.gameObject.transform.position);
                            animator.SetInteger("AnimationState", 1);
                            AnimationState = 1;
                            gameManager.setCurrentDirection("UP");
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.A) && direction != "RIGHT")//&& gameManager.returnGridNum((int)gameManager.returnPlayerXY().x-1, (int)gameManager.returnPlayerXY().y) == 0)
                    {
                        direction = "LEFT";
                        animator.SetInteger("AnimationState", 3);
                        AnimationState = 3;
                        //  Debug.Log("Pressed A");
                        gameManager.increaseNumberOfTurns();
                        if (gameManager.returnDirCount() > 0 && gameManager.returnListDir()[gameManager.returnDirCount() - 1] != direction && gameManager.returnListDir()[gameManager.returnDirCount() - 1] != "RIGHT")
                        {
                            gameManager.addDir(direction, this.gameObject.transform.position);
                            animator.SetInteger("AnimationState", 3);
                            AnimationState = 3;
                            gameManager.setCurrentDirection("LEFT");
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.S) && direction != "UP")//&& gameManager.returnGridNum((int)gameManager.returnPlayerXY().x, (int)gameManager.returnPlayerXY().y-1) == 0)
                    {
                        direction = "DOWN";
                        animator.SetInteger("AnimationState", 5);
                        AnimationState = 5;
                        //  Debug.Log("Pressed S");
                        gameManager.increaseNumberOfTurns();
                        if (gameManager.returnDirCount() > 0 && gameManager.returnListDir()[gameManager.returnDirCount() - 1] != direction && gameManager.returnListDir()[gameManager.returnDirCount() - 1] != "UP")
                        {
                            gameManager.addDir(direction, this.gameObject.transform.position);
                            animator.SetInteger("AnimationState", 5);
                            AnimationState = 5;
                            gameManager.setCurrentDirection("DOWN");
                        }

                    }
                    else if (Input.GetKeyDown(KeyCode.D) && direction != "LEFT")//&& gameManager.returnGridNum((int)gameManager.returnPlayerXY().x+1, (int)gameManager.returnPlayerXY().y) == 0)
                    {
                        direction = "RIGHT";
                        animator.SetInteger("AnimationState", 7);
                        AnimationState = 7;
                        // Debug.Log("Pressed D");
                        gameManager.increaseNumberOfTurns();
                        if (gameManager.returnDirCount() > 0 && gameManager.returnListDir()[gameManager.returnDirCount() - 1] != direction && gameManager.returnListDir()[gameManager.returnDirCount() - 1] != "LEFT")
                        {
                            gameManager.addDir(direction, this.gameObject.transform.position);
                            animator.SetInteger("AnimationState", 7);
                            AnimationState = 7;
                            gameManager.setCurrentDirection("RIGHT");
                        }
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.W) && direction != "DOWN")
                    {
                        direction = "UP";
                        animator.SetInteger("AnimationState", 1);
                        AnimationState = 1;
                        //  Debug.Log("Pressed W");
                        //Debug.Log(gameManager.returnScore());
                        //gameManager.printGrid();

                        //gameManager.move(this.gameObject, "UP");
                        gameManager.increaseNumberOfTurns();
                    }
                    else if (Input.GetKeyDown(KeyCode.A) && direction != "RIGHT")
                    {
                        direction = "LEFT";
                        animator.SetInteger("AnimationState", 3);
                        AnimationState = 3;
                        //   Debug.Log("Pressed A");
                        //gameManager.printGrid();
                        //gameManager.move(this.gameObject, "LEFT");
                        gameManager.increaseNumberOfTurns();
                    }
                    else if (Input.GetKeyDown(KeyCode.S) && direction != "UP")
                    {
                        direction = "DOWN";
                        animator.SetInteger("AnimationState", 5);
                        AnimationState = 5;
                        //   Debug.Log("Pressed S");
                        // gameManager.printGrid();gameManager.move(this.gameObject, "UP");
                        // gameManager.move(this.gameObject, "DOWN");
                        gameManager.increaseNumberOfTurns();

                    }
                    else if (Input.GetKeyDown(KeyCode.D) && direction != "LEFT")
                    {
                        direction = "RIGHT";
                        animator.SetInteger("AnimationState", 7);
                        AnimationState = 7;
                        //   Debug.Log("Pressed D");
                        //gameManager.printGrid();
                        //gameManager.move(this.gameObject, "RIGHT");
                        gameManager.increaseNumberOfTurns();
                    }
                }


                if (gameManager.returnCount() == 5)
                {
                    try
                    {
                        gameManager.move(this.gameObject, direction);
                    }
                    catch (Exception e)
                    {
                        //Debug.Log("UGHH ERRORRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
                    }
                }
                // Debug.Log("PLAYERRRR COUNT: " + count);
            }
        }
        else
        {
            Debug.Log("Destroyed Player!");
            Destroy(this.gameObject);
        }
       
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("I COLLIDED WITH... " + other.gameObject.name);
        if (other.gameObject.name == "Egg2" || other.gameObject.name == "Egg2(Clone)")
        {
            gameManager.increaseScore();
            gameManager.returnScore();
        }
        else
        {
            Debug.Log("YOU LOST");
            gameManager.reset();
            Destroy(this.gameObject);
            Application.LoadLevel("GameOver");
        }
    }
}
