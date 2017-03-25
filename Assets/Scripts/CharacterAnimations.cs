using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour {

    public int x;
    public int y;
    float speed = 0.03f;

    int count = 0;
    bool turn = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (x == 1 && !turn)
        {
            this.transform.Translate(speed, 0, 0);
            count++;
        }
        else if (x == 1 && turn)
        {
            this.transform.Translate(-speed, 0, 0);
            count--;
        }
        if (count == 80)
        {
            Debug.Log("Turn is true!!");
            turn = true;
        }
        if (count == 0)
        {
            Debug.Log("Turn is false!!");
            turn = false;
        }
        
            
	}
}
