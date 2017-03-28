using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour {

    int x;
    int y;
    int count2;
    float speed;

    int count = 0;
    bool turn = false;


    private Animator animator;
    private int AnimationState;

    BoxCollider2D[] bc;

	// Use this for initialization
	void Start () {
        AnimationState = 0;
        animator = this.GetComponent<Animator>();

        bc = gameObject.GetComponents<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(count);
        if (x == 1 && !turn)
        {
            //Debug.Log("X is 1! Moving right");
            this.transform.Translate(speed, 0, 0);
            count++;
            animator.SetInteger("AnimationState", 7);
            AnimationState = 7;
            bc[0].enabled = false;
            bc[1].enabled = false;
            bc[2].enabled = true;
        }
        else if (x == 1 && turn)
        {
            //Debug.Log("X is 1! Moving left");
            this.transform.Translate(-speed, 0, 0);
            count--;
            animator.SetInteger("AnimationState", 3);
            AnimationState = 3;
            bc[0].enabled = false;
            bc[1].enabled = true;
            bc[2].enabled = false;
        }
        else if (y == 1 && !turn)
        {
            //Debug.Log("Y is 1! Moving up");
            this.transform.Translate(speed, 0, 0);
            count++;
            animator.SetInteger("AnimationState", 1);
            AnimationState = 1;
            bc[0].enabled = true;
            bc[1].enabled = false;
            bc[2].enabled = false;
        }
        else if (y == 1 && turn)
        {
            //Debug.Log("Y is 1! Moving down");
            this.transform.Translate(-speed, 0, 0);
            count--;
            animator.SetInteger("AnimationState", 5);
            AnimationState = 5;
            bc[0].enabled = true;
            bc[1].enabled = false;
            bc[2].enabled = false;
        }
        if (count == count2)
        {
            //Debug.Log("Turn is true!!");
            turn = true;
        }
        if (count == 0)
        {
            //Debug.Log("Turn is false!!");
            turn = false;
        }
        
            
	}

    public void setVariables(int x1, int y1, int count3, float speed2)
    {
        //Debug.Log("x: " + x + ", x1: " + x1);
        //Debug.Log("y: " + y + ", y1: " + y1);
        //Debug.Log("count3: " + count3);
        //Debug.Log("speed: " + speed + ", speed2: " + speed2);
        x = x1;
        y = y1;
        count2 = count3;
        speed = speed2;
        //Debug.Log("AFTERRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
        //Debug.Log("x: " + x + ", x1: " + x1);
        //Debug.Log("y: " + y + ", y1: " + y1);
        //Debug.Log("count3: " + count3);
        //Debug.Log("speed: " + speed + ", speed2: " + speed2);
    }
}
