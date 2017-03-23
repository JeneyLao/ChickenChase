﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    Text t;

    GameManager gameManager;

    int turns;
    int eggs;
    int score;
    int level;

	// Use this for initialization
	void Start () {
        //Debug.Log("SCORE SCRIPT START");
        gameManager = GameManager.instance;
        t = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.loadedLevelName != "GameOver")
        {
            if (t.gameObject.tag == "NumberOfTurns")
            {
                t.text = "Turns: " + gameManager.returnNumberOfTurns();
                turns = gameManager.returnNumberOfTurns();
            }
            else if (t.gameObject.tag == "NumberOfEggs")
            {
                t.text = "" + gameManager.returnNumberOfEggs() + "/" + gameManager.returnTargetEggs();
                eggs = gameManager.returnScore();
            }
            else if (t.gameObject.tag == "Score")
            {
                t.text = "Score: " + (gameManager.returnScore() * 50 - gameManager.returnNumberOfTurns());
                score = (gameManager.returnScore() * 50 - gameManager.returnNumberOfTurns());
            }
            else if (t.gameObject.tag == "Level")
            {
                t.text = "Level: " + gameManager.returnLevel();
                level = gameManager.returnLevel();
            }
            else if (t.gameObject.tag == "LevelInstruction" )
            {
                if (gameManager.getCurrentState() == "INSTRUCTIONS")
                {
                    if (Application.loadedLevelName == "Level1")
                        t.text = "Help Cluckee as he scrambles to collect the eggs!";
                    else if (Application.loadedLevelName == "Level2")
                        t.text = "Hey, good job! Try to collect 10 more!";
                    else if (Application.loadedLevelName == "Level3")
                        t.text = "don’t disturb her! She’ll be upset if you run into her; she will also take an egg from you if the eggs get in her way, so try to maneuver around her!";
                }
                else if (gameManager.getCurrentState() == "PLAYING")
                {
                    t.text = "";
                }
            }
        }
        else
        {
            if (t.gameObject.tag == "NumberOfTurns")
            {
                t.text = "Turns: " + gameManager.returnNumberOfTurns();
                turns = gameManager.returnNumberOfTurns();
            }
            else if (t.gameObject.tag == "NumberOfEggs")
            {
                t.text = "" + gameManager.returnScore();
                eggs = gameManager.returnScore();
            }
            else if (t.gameObject.tag == "Score")
            {
                t.text = "Score: " + (gameManager.returnScore() * 50 - gameManager.returnNumberOfTurns());
                score = (gameManager.returnScore() * 50 - gameManager.returnNumberOfTurns());
            }
            else if (t.gameObject.tag == "Level")
            {
                t.text = "Level: " + gameManager.returnLevel();
                level = gameManager.returnLevel();
            }
        }
	}
}