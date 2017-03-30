using System.Collections;
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
        gameManager = GameManager.instance;
        t = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.loadedLevelName != "GameOver" && Application.loadedLevelName != "Won")
        {
            if (t.gameObject.tag == "NumberOfTurns")
            {
                t.text = "Turns: " + gameManager.returnNumberOfTurns();
                turns = gameManager.returnNumberOfTurns();
            }
            else if (t.gameObject.tag == "NumberOfEggs")
            {
                if (Application.loadedLevelName != "Endless")
                {
                    t.text = "" + gameManager.returnNumberOfEggs() + "/" + gameManager.returnTargetEggs();
                    eggs = gameManager.returnScore();
                }
                else
                {
                    t.text = "" + gameManager.returnNumberOfEggs();
                    eggs = gameManager.returnScore();
                }
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
                    if (gameManager.returnLevel() == 1)
                        t.text = "Level 1\nHelp Cluckee as he scrambles to collect the eggs!";
                    else if (gameManager.returnLevel() == 2)
                        t.text = "Level 2\nHey, good job! Be careful, don't hit the hay!";
                    else if (gameManager.returnLevel() == 3)
                        t.text = "Level 3\nThere's some more hay. Be sure not to hit any of them!";
                    else if (gameManager.returnLevel() == 4)
                        t.text = "Level 4\nAnd even more hay! I wonder where they're coming from.";
                    else if (gameManager.returnLevel() == 5)
                        t.text = "Level 5\nLooks like some cows are on their lunch break. Don't bother them!";
                    else if (gameManager.returnLevel() == 6)
                        t.text = "Level 6\nJust your regular day with cows and piles of hay.";
                    else if (gameManager.returnLevel() == 7)
                        t.text = "Level 7\nLooks like these cows and piles of hay just love to ruin your egg-collecting days.";
                    else if (gameManager.returnLevel() == 8)
                        t.text = "Level 8\nThank goodness! No more hay, but there seems to be more cows!";
                    else if (gameManager.returnLevel() == 9)
                        t.text = "Level 9\nOh no, the piles of hay are back!";
                    else if (gameManager.returnLevel() == 10)
                        t.text = "Level 10\nWow, you made it to the last level! Can you beat it?";
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
