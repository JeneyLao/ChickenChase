using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
public class Player : MonoBehaviour
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

    //default is 0.03f
    public float speed;

    // Use this for initialization
    void Start()
    {
        gameManager = GameManager.instance;
        AnimationState = 0;
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName != "GameOver" && Application.loadedLevelName != "Won")
        {
            if (gameManager.getCurrentState() == "INSTRUCTIONS")
            {
                direction = "";
                animator.SetInteger("AnimationState", 1);
                AnimationState = 1;
            }
            else if (gameManager.getCurrentState() == "PLAYING")
            {
                if (gameManager.getCurrentEgg() != this.gameObject)
                {
                    if (Input.GetKeyDown(KeyCode.W) && direction != "DOWN")
                    {
                        direction = "UP";
                        animator.SetInteger("AnimationState", 1);
                        AnimationState = 1;
                        gameManager.increaseNumberOfTurns();
                    }
                    else if (Input.GetKeyDown(KeyCode.A) && direction != "RIGHT")
                    {
                        direction = "LEFT";
                        animator.SetInteger("AnimationState", 3);
                        AnimationState = 3;
                        gameManager.increaseNumberOfTurns();
                    }
                    else if (Input.GetKeyDown(KeyCode.S) && direction != "UP")
                    {
                        direction = "DOWN";
                        animator.SetInteger("AnimationState", 5);
                        AnimationState = 5;
                        gameManager.increaseNumberOfTurns();

                    }
                    else if (Input.GetKeyDown(KeyCode.D) && direction != "LEFT")
                    {
                        direction = "RIGHT";
                        animator.SetInteger("AnimationState", 7);
                        AnimationState = 7;
                        gameManager.increaseNumberOfTurns();
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.W) && direction != "DOWN")
                    {
                        direction = "UP";
                        animator.SetInteger("AnimationState", 1);
                        AnimationState = 1;
                        gameManager.increaseNumberOfTurns();
                    }
                    else if (Input.GetKeyDown(KeyCode.A) && direction != "RIGHT")
                    {
                        direction = "LEFT";
                        animator.SetInteger("AnimationState", 3);
                        AnimationState = 3;
                        gameManager.increaseNumberOfTurns();
                    }
                    else if (Input.GetKeyDown(KeyCode.S) && direction != "UP")
                    {
                        direction = "DOWN";
                        animator.SetInteger("AnimationState", 5);
                        AnimationState = 5;
                        gameManager.increaseNumberOfTurns();

                    }
                    else if (Input.GetKeyDown(KeyCode.D) && direction != "LEFT")
                    {
                        direction = "RIGHT";
                        animator.SetInteger("AnimationState", 7);
                        AnimationState = 7;
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
                    }
                }
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Egg")
        {
            gameManager.increaseScore();
            gameManager.returnScore();
        }
        else
        {
            gameManager.reset();
            Destroy(this.gameObject);
            Application.LoadLevel("GameOver");
        }
    }
}
