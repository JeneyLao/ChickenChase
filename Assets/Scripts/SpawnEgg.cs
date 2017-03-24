using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SpawnEgg : MonoBehaviour
{

    public GameObject egg;
    public GameManager gameManager;
    public GameObject player;
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

    Random rnd;

    float pixel = 0.25f;

    int ex;
    int ey;

    int prevX;
    int prevY;

    // Use this for initialization
    void Start()
    {
        //Debug.Log("EGG START");
        gameManager = GameManager.instance;
        //player = Player2.instance;
        follow = false;

        m = false;

        dir = new List<string>();
        pos = new List<Vector2>();

        rnd = new Random();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.getCurrentState() == "PLAYING")
        {
            if (follow)
            {
                if (head == "Chicken")
                {
                    //Debug.Log("CHICKEN EGG1 POS: " + this.transform.position);
                    gameManager.changeGrid(prevX, prevY, 0);
                    prevX = ex;
                    prevY = ey;
                    this.transform.position = new Vector2(gameManager.returnPreviousPosition().x * pixel, gameManager.returnPreviousPosition().y * pixel);
                    ex = (int)gameManager.returnPreviousPosition().x;
                    ey = (int)gameManager.returnPreviousPosition().y;
                    gameManager.changeGrid(ex, ey, 1);
                    // Debug.Log("CHICKEN EGG1 POS - prevx: " + prevX + ", prevy: " + prevY);
                    // Debug.Log("CHICKEN EGG1 POS - ex: " + ex + ", ey: " + ey);
                }
                else
                {
                    //Debug.Log(gameManager.returnCount());
                    //Debug.Log("HEAD NOT CHICKEN POS: " + this.transform.position);
                    if (gameManager.returnCount() == 5)
                    {
                        gameManager.changeGrid(prevX, prevY, 1);
                        prevX = ex;
                        prevY = ey;
                        this.transform.position = new Vector2(GameObject.Find(head).GetComponent<SpawnEgg>().returnPreviousPosition().x * pixel, GameObject.Find(head).GetComponent<SpawnEgg>().returnPreviousPosition().y * pixel);

                        ex = (int)GameObject.Find(head).GetComponent<SpawnEgg>().returnPreviousPosition().x;
                        ey = (int)GameObject.Find(head).GetComponent<SpawnEgg>().returnPreviousPosition().y;
                        gameManager.changeGrid(ex, ey, 1);
                        // Debug.Log("EGG2 POS - prevx: " + prevX + ", prevy: " + prevY);
                        //  Debug.Log("EGG2 POS - ex: " + ex + ", ey: " + ey);

                    }
                }
            }
            if (GameObject.FindGameObjectWithTag("Egg2") == null && gameManager.returnStartSpawnEgg())
            {
                bool doNotExitLoop = true;
                Vector2 pos1 = new Vector2(Random.Range(2, 46), Random.Range(2, 26));
                while (doNotExitLoop)
                {
                    pos1 = new Vector2(Random.Range(1, 49), Random.Range(1, 28));
                    if (gameManager.returnGridNum((int)pos1.x, (int)pos1.y) == 0)
                    {
                        break;
                    }
                }
                //Vector2 pos1 = new Vector2(Random.Range(2, 47), Random.Range(2, 26));
                //Debug.Log(pos1);
                Instantiate(egg, new Vector2(pos1.x * pixel, pos1.y * pixel), transform.rotation);
                GameObject.FindGameObjectWithTag("Egg2").GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
                ex = (int)pos1.x;
                ey = (int)pos1.y;
                //Debug.Log("ex: " + ex + ", ey: " + ey);
                gameManager.changeGrid(ex, ey, 1);
                Debug.Log("EGG IS SPAWNEDDDDDDDDDDD");
            }
        }
    }


    public Vector2 returnPreviousPosition()
    {
        return new Vector2(prevX, prevY);
    }

    public void addDir(string d, Vector2 v)
    {
       // Debug.Log("This is: " + this.gameObject.name + ", Added Direction: " + d);
        dir.Add(d);
        pos.Add(v);
    }

    public void removeDir(string d, Vector2 v)
    {
       // Debug.Log("This is: " + this.gameObject.name + ", Removed Direction: " + d);
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
        if (this.gameObject.name == "Egg2(Clone)")
        {
            head = gameManager.returnCurrentTag();
            follow = true;
            this.gameObject.tag = "N";// +gameManager.returnScore();
            this.gameObject.name = "N" + gameManager.returnScore();

            gameManager.setCurrentEgg(this.gameObject);
        }

        //gameManager.setCurrentEgg(this.gameObject);        
    }
}
