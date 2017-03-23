using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SpawnEgg : MonoBehaviour {

    public GameObject egg;
    public GameManager gameManager;
    public float speed;

    string currentDirection;
    bool m;

    bool follow;

    int count = 0;

    string head;

    string d;

    int c = 0;

    int frames = 0;

    List<string> dir;
    List<Vector2> pos;


	// Use this for initialization
	void Start () {
        gameManager = GameManager.instance;
        follow = false;

        m = false;

        dir = new List<string>();
        pos = new List<Vector2>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("FRAMES COUNT: " + frames + ", SPEED: " + speed);
        if (frames > 10)
        {
            frames = 0;
            speed = 0.03f;
        }
        if (follow)
        {
            if (gameManager.getCurrentEgg() != this.gameObject)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (dir[dir.Count - 1] != "UP")
                    {
                        addDir("UP", this.gameObject.transform.position);
                    }
                    //currentDirection = "UP";
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    if (dir[dir.Count - 1] != "LEFT")
                    {
                        addDir("LEFT", this.gameObject.transform.position);
                    }
                    //currentDirection = "LEFT";
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    if (dir[dir.Count - 1] != "DOWN")
                    {
                        addDir("DOWN", this.gameObject.transform.position);
                    }
                    //currentDirection = "DOWN";
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    if (dir[dir.Count - 1] != "RIGHT")
                    {
                        addDir("RIGHT", this.gameObject.transform.position);
                    }
                    //currentDirection = "RIGHT";
                }
                else
                {
                    if (head == "Player" && c == 0)
                    {
                        //Debug.Log("Current Direction: " + gameManager.getCurrentDirection());
                        setDir(gameManager.getCurrentDirection(), this.gameObject.transform.position);
                        c = 1;
                    }
                    else if (c == 0)
                    {
                       // Debug.Log("Set direction to: " + GameObject.Find(head).GetComponent<SpawnEgg>().returnDir());
                        Debug.Log("Current DIRECITOTTIOTTTNTNTN: " + GameObject.Find(head).GetComponent<SpawnEgg>().getCurrentDirection());
                        Debug.Log("This is: " + this.gameObject.name + ", HEADDDDDDD: " + head);
                        //if (currentDirection != "UP" && currentDirection != "LEFT" && currentDirection != "DOWN" && currentDirection != "RIGHT")
                        //{
                        //    currentDirection = 
                        //}
                        setDir(GameObject.Find(head).GetComponent<SpawnEgg>().getCurrentDirection(), this.gameObject.transform.position);
                        c = 1;
                    }
                }
            }

            if (head == "Player")
            {
                string direction = gameManager.returnDir();
                Vector2 pos = gameManager.returnPos();
                //string t = gameManager.returnCurrentTag();
                Vector2 g = GameObject.FindGameObjectWithTag("Player").transform.position;

                if (!m)
                {
                    switch (direction)
                    {
                        case "UP":
                            transform.position = new Vector2(g.x, g.y - 0.3f);
                            currentDirection = "UP";
                            break;
                        case "LEFT":
                            transform.position = new Vector2(g.x + 0.3f, g.y);
                            currentDirection = "LEFT";
                            break;
                        case "DOWN":
                            transform.position = new Vector2(g.x, g.y + 0.3f);
                            currentDirection = "DOWN";
                            break;
                        case "RIGHT":
                            transform.position = new Vector2(g.x - 0.3f, g.y);
                            currentDirection = "RIGHT";
                            break;
                    }
                    m = true;
                }
                else
                {

                    switch (direction)
                    {
                        case "UP":
                            this.transform.Translate(0, speed, 0);
                            currentDirection = "UP";
                            //Debug.Log("UP: " + this.transform.position.y + ", " + GameObject.FindGameObjectWithTag("Player").transform.position.y);
                            if (this.transform.position.y >= GameObject.FindGameObjectWithTag("Player").transform.position.y && gameManager.returnDirCount() != 0)
                            {
                                gameManager.removeDir(direction, pos);
                                speed = 0.0303f;
                                frames++;
                            }
                            break;
                        case "LEFT":
                            this.transform.Translate(-speed, 0, 0);
                            currentDirection = "LEFT";
                            //Debug.Log("LEFT: " + this.transform.position.x + ", " + GameObject.FindGameObjectWithTag("Player").transform.position.x);
                            if (this.transform.position.x <= GameObject.FindGameObjectWithTag("Player").transform.position.x && gameManager.returnDirCount() != 0)
                            {
                                gameManager.removeDir(direction, pos);
                                speed = 0.0303f;
                                frames++;
                            }
                            break;
                        case "DOWN":
                            this.transform.Translate(0, -speed, 0);
                            currentDirection = "DOWN";
                            //Debug.Log("DOWN: " + this.transform.position.y + ", " + GameObject.FindGameObjectWithTag("Player").transform.position.y);
                            if (this.transform.position.y <= GameObject.FindGameObjectWithTag("Player").transform.position.y && gameManager.returnDirCount() != 0)
                            {
                                gameManager.removeDir(direction, pos);
                                speed = 0.0303f;
                                frames++;
                            }
                            break;
                        case "RIGHT":
                            this.transform.Translate(speed, 0, 0);
                            currentDirection = "RIGHT";
                            //Debug.Log("RIGHT: " + this.transform.position.x + ", " + GameObject.FindGameObjectWithTag("Player").transform.position.x);
                            if (this.transform.position.x >= GameObject.FindGameObjectWithTag("Player").transform.position.x && gameManager.returnDirCount() != 0)
                            {
                                gameManager.removeDir(direction, pos);
                                speed = 0.0303f;
                                frames++;
                            }
                            break;
                    }

                }
            }
            else
            {
                string direction = GameObject.Find(head).GetComponent<SpawnEgg>().returnDir();
                Vector2 pos = GameObject.Find(head).GetComponent<SpawnEgg>().returnPos();
                Vector2 g = GameObject.Find(head).GetComponent<SpawnEgg>().transform.position;

                if (!m)
                {
                    switch (direction)
                    {
                        case "UP":
                            transform.position = new Vector2(g.x, g.y - 0.3f);
                            currentDirection = "UP";
                            break;
                        case "LEFT":
                            transform.position = new Vector2(g.x + 0.3f, g.y);
                            currentDirection = "LEFT";
                            break;
                        case "DOWN":
                            transform.position = new Vector2(g.x, g.y + 0.3f);
                            currentDirection = "DOWN";
                            break;
                        case "RIGHT":
                            transform.position = new Vector2(g.x - 0.3f, g.y);
                            currentDirection = "RIGHT";
                            break;
                    }
                    m = true;
                }
                else
                {
                    Debug.Log("This is: " + this.gameObject.name + ", DirCOUNT = " + dir.Count);
                    Debug.Log("THIS POSITION: " + this.transform.position + ", HEAD POSITION: " + GameObject.Find(head).transform.position);
                    switch (direction)
                    {
                        case "UP":
                            //Debug.Log("UP: " + this.transform.position.y + ", " + GameObject.Find(head).transform.position.y);
                            this.transform.Translate(0, speed, 0);
                            currentDirection = "UP";
                            if (this.transform.position.y >= GameObject.Find(head).transform.position.y && dir.Count > 0)
                                //Mathf.Abs(this.transform.position.x - GameObject.Find(head).transform.position.x) < 1f && dir.Count > 0)
                            {
                                GameObject.Find(head).GetComponent<SpawnEgg>().removeDir(direction, pos);
                            }
                            break;
                        case "LEFT":
                           // Debug.Log("LEFT: " + this.transform.position.x + ", " + GameObject.Find(head).transform.position.x);

                            this.transform.Translate(-speed, 0, 0);
                            currentDirection = "LEFT";
                            if (this.transform.position.x <= GameObject.Find(head).transform.position.x && dir.Count > 0)
                                //Mathf.Abs(this.transform.position.y - GameObject.Find(head).transform.position.y) < 1f && dir.Count > 0)
                            {
                                GameObject.Find(head).GetComponent<SpawnEgg>().removeDir(direction, pos);
                            }
                            break;
                        case "DOWN":
                           // Debug.Log("DOWN: " + this.transform.position.y + ", " + GameObject.Find(head).transform.position.y);
                            this.transform.Translate(0, -speed, 0);
                            currentDirection = "DOWN";
                            if (this.transform.position.y <= GameObject.Find(head).transform.position.y && dir.Count > 0)
                                //Mathf.Abs(this.transform.position.x - GameObject.Find(head).transform.position.x) < 1f && dir.Count > 0)
                            {
                                GameObject.Find(head).GetComponent<SpawnEgg>().removeDir(direction, pos);
                            }
                            break;
                        case "RIGHT":
                          //  Debug.Log("RIGHT: " + this.transform.position.x + ", " + GameObject.Find(head).transform.position.x);
                            this.transform.Translate(speed, 0, 0);
                            currentDirection = "RIGHT";
                            if (this.transform.position.x >= GameObject.Find(head).transform.position.x && dir.Count > 0)
                                //Mathf.Abs(this.transform.position.y - GameObject.Find(head).transform.position.y) < 1f && dir.Count > 0)
                            {
                                GameObject.Find(head).GetComponent<SpawnEgg>().removeDir(direction, pos);
                            }
                            break;
                    }

                }
            }
        }
        if (GameObject.FindGameObjectWithTag("Egg") == null)
        {
            Vector2 pos1 = new Vector2(Random.Range(-5, 5), Random.Range(-3, 3));
            Instantiate(egg, pos1, transform.rotation);
        }
	}

    public void addDir(string d, Vector2 v)
    {
        Debug.Log("This is: " + this.gameObject.name + ", Added Direction: " + d);
        dir.Add(d);
        pos.Add(v);
    }

    public void removeDir(string d, Vector2 v)
    {
        Debug.Log("This is: " + this.gameObject.name + ", Removed Direction: " + d);
        dir.Remove(d);
        pos.Remove(v);
    }

    public void setDir(string d, Vector2 v)
    {
        dir[0] = d;
        pos[0] = v;
    }

    public string returnDir()
    {
        if (dir.Count == 0)
        {
            return "NULL";
        }
        return dir[0];
    }

    public Vector2 returnPos()
    {
        if (pos.Count == 0)
        {
            return new Vector2(0f, 0f);
        }
        return pos[0];
    }

    public void setCurrentDirection(string d)
    {
        currentDirection = d;
    }

    public string getCurrentDirection()
    {
        return currentDirection;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Destroy(this.gameObject);
        if (this.gameObject.name == "Egg(Clone)")
        {
            head = gameManager.returnCurrentTag();
            follow = true;
            this.gameObject.tag = "N";
            this.gameObject.name = "N" + gameManager.returnScore();

            if (count == 0 && head == "Player")
            {
                Debug.Log("COUNT IS 0, HEAD IS PLAYER");
                addDir(gameManager.getCurrentDirection(), GameObject.FindGameObjectWithTag("Player").transform.position);
                count = 1;
            }

            if (count == 0 && head != "Player")
            {
                Debug.Log("HEAD IS NOT PLAYER");
                //d = GameObject.Find(head).GetComponent<SpawnEgg>().returnDir();
                addDir(GameObject.Find(head).GetComponent<SpawnEgg>().getCurrentDirection(), GameObject.Find(head).transform.position);
                count = 1;
            }
            gameManager.setCurrentEgg(this.gameObject);
            Debug.Log(head);
        }
        
        //gameManager.setCurrentEgg(this.gameObject);        
    }
}
