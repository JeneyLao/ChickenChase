using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour {

    private GameManager gameManager;

    public string sceneName;
	// Use this for initialization
	void Start () {
        gameManager = GameManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (sceneName == "Start2")
                gameManager.resetPoints();
            Application.LoadLevel(sceneName);
        }
	}
}
