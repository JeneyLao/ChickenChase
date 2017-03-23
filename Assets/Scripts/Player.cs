using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

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

    //default is 0.03f
    public float speed;

	// Use this for initialization
	void Start () 
    {
        gameManager = GameManager.instance;
        AnimationState = 0;
        animator = this.GetComponent<Animator>();

        
	}
	
	// Update is called once per frame
    void Update()
    {
        //Debug.Log("PLAYER'S POSITIONNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN: " + this.transform.position);
        if (gameManager.getCurrentState() == "PLAYING")
        {
            //Debug.Log("DIRECTION : " + direction);
            
            if (gameManager.getCurrentEgg() != this.gameObject)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    //this.transform.Translate(0, speed, 0);
                    direction = "UP";
                    //this.GetComponent<BoxCollider2D>().size = new Vector2(0.2774534f, 0.61f);
                    if (gameManager.returnDirCount() > 0 && gameManager.returnListDir()[gameManager.returnDirCount() - 1] != direction && gameManager.returnListDir()[gameManager.returnDirCount() - 1] != "DOWN")
                    {
                        gameManager.addDir(direction, this.gameObject.transform.position);
                        animator.SetInteger("AnimationState", 1);
                        AnimationState = 1;
                        gameManager.setCurrentDirection("UP");
                    }
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    //this.transform.Translate(-speed, 0, 0);
                    direction = "LEFT";
                    //this.GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.61f);
                    if (gameManager.returnDirCount() > 0 && gameManager.returnListDir()[gameManager.returnDirCount() - 1] != direction && gameManager.returnListDir()[gameManager.returnDirCount() - 1] != "RIGHT")
                    {
                        gameManager.addDir(direction, this.gameObject.transform.position);
                        animator.SetInteger("AnimationState", 3);
                        AnimationState = 3;
                        gameManager.setCurrentDirection("LEFT");
                    }
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    //this.transform.Translate(0, -speed, 0);
                    direction = "DOWN";
                    //this.GetComponent<BoxCollider2D>().size = new Vector2(0.2774534f, 0.61f);
                    if (gameManager.returnDirCount() > 0 && gameManager.returnListDir()[gameManager.returnDirCount() - 1] != direction && gameManager.returnListDir()[gameManager.returnDirCount() - 1] != "UP")
                    {
                        gameManager.addDir(direction, this.gameObject.transform.position);
                        animator.SetInteger("AnimationState", 5);
                        AnimationState = 5;
                        gameManager.setCurrentDirection("DOWN");
                    }
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    //this.transform.Translate(speed, 0, 0);
                    direction = "RIGHT";
                    if (gameManager.returnDirCount() > 0 && gameManager.returnListDir()[gameManager.returnDirCount() - 1] != direction && gameManager.returnListDir()[gameManager.returnDirCount() - 1] != "LEFT")
                    {
                        gameManager.addDir(direction, this.gameObject.transform.position);
                        animator.SetInteger("AnimationState", 7);
                        AnimationState = 7;
                        gameManager.setCurrentDirection("RIGHT");
                    }
                    //this.GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.61f);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.W) && direction != "DOWN")
                {
                    animator.SetInteger("AnimationState", 1);
                    AnimationState = 1;
                    //this.transform.Translate(0, speed, 0);
                    direction = "UP";
                    //this.GetComponent<BoxCollider2D>().size = new Vector2(0.2774534f, 0.61f);
                }
                else if (Input.GetKeyDown(KeyCode.A) && direction != "RIGHT")
                {
                    animator.SetInteger("AnimationState", 3);
                    AnimationState = 3;
                    //this.transform.Translate(-speed, 0, 0);
                    direction = "LEFT";
                    //this.GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.61f);
                }
                else if (Input.GetKeyDown(KeyCode.S) && direction != "DOWN")
                {
                    animator.SetInteger("AnimationState", 5);
                    AnimationState = 5;
                    //this.transform.Translate(0, -speed, 0);
                    direction = "DOWN";
                    //this.GetComponent<BoxCollider2D>().size = new Vector2(0.2774534f, 0.61f);
                }
                else if (Input.GetKeyDown(KeyCode.D) && direction != "LEFT")
                {
                    animator.SetInteger("AnimationState", 7);
                    AnimationState = 7;
                    //this.transform.Translate(speed, 0, 0);
                    direction = "RIGHT";

                    //this.GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.61f);
                }
            }
            ContinuedMovement(direction);
        }
               
        else
        {

            // UP LEFT DOWN RIGHT IN THAT ORDER
            if (target == "DownCollider")
            {
                animator.SetInteger("AnimationState", 0);
                AnimationState = 0;
            }
            else if (target == "RightCollider")
            {
                animator.SetInteger("AnimationState", 2);
                AnimationState = 2;
            }
            else if (target == "UpCollider")
            {
                animator.SetInteger("AnimationState", 4);
                AnimationState = 4;
            }
            else if (target == "LeftCollider")
            {
                animator.SetInteger("AnimationState", 6);
                AnimationState = 6;
            }
        }
    }

    void ContinuedMovement(string direction)
    {
        if (gameManager.getCurrentEgg() == this.gameObject)
        {
            if (count == 0)
            {
                gameManager.addDir("UP", this.gameObject.transform.position);
                count = 1;
            }
            switch (direction)
            {
                case "UP":
                    this.transform.Translate(0, speed, 0);
                    gameManager.setDir(direction, this.gameObject.transform.position);
                    break;
                case "LEFT":
                    this.transform.Translate(-speed, 0, 0);
                    gameManager.setDir(direction, this.gameObject.transform.position);
                    break;
                case "DOWN":
                    this.transform.Translate(0, -speed, 0);
                    gameManager.setDir(direction, this.gameObject.transform.position);
                    break;
                case "RIGHT":
                    this.transform.Translate(speed, 0, 0);
                    gameManager.setDir(direction, this.gameObject.transform.position);
                    break;
            }
        }
        else
        {
            switch (direction)
            {
                case "UP":
                    this.transform.Translate(0, speed, 0);
                    //gameManager.addDir(direction);
                    //gameManager.addDir(direction, this.gameObject.transform.position);
                    break;
                case "LEFT":
                    this.transform.Translate(-speed, 0, 0);
                    //gameManager.addDir(direction);
                    //gameManager.addDir(direction, this.gameObject.transform.position);
                    break;
                case "DOWN":
                    this.transform.Translate(0, -speed, 0);
                   // gameManager.addDir(direction);
                    //gameManager.addDir(direction, this.gameObject.transform.position);
                    break;
                case "RIGHT":
                    this.transform.Translate(speed, 0, 0);
                   // gameManager.addDir(direction);
                    //gameManager.addDir(direction, this.gameObject.transform.position);
                    break;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Egg" || other.gameObject.name == "Egg(Clone)")
        {
            gameManager.increaseScore();
            gameManager.returnScore();
        }
    }
}
