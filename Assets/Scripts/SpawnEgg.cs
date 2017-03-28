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

    Random rnd;
    float pixel = 0.25f;

    int ex;
    int ey;

    int prevX;
    int prevY;

    // Use this for initialization
    void Start()
    {
        gameManager = GameManager.instance;
        follow = false;
        m = false;
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
                    gameManager.changeGrid(prevX, prevY, 0);
                    prevX = ex;
                    prevY = ey;
                    this.transform.position = new Vector2(gameManager.returnPreviousPosition().x * pixel, gameManager.returnPreviousPosition().y * pixel);
                    ex = (int)gameManager.returnPreviousPosition().x;
                    ey = (int)gameManager.returnPreviousPosition().y;
                    gameManager.changeGrid(ex, ey, 1);
                }
                else
                {
                    if (gameManager.returnCount() == 5)
                    {
                        gameManager.changeGrid(prevX, prevY, 1);
                        prevX = ex;
                        prevY = ey;
                        this.transform.position = new Vector2(GameObject.Find(head).GetComponent<SpawnEgg>().returnPreviousPosition().x * pixel, GameObject.Find(head).GetComponent<SpawnEgg>().returnPreviousPosition().y * pixel);

                        ex = (int)GameObject.Find(head).GetComponent<SpawnEgg>().returnPreviousPosition().x;
                        ey = (int)GameObject.Find(head).GetComponent<SpawnEgg>().returnPreviousPosition().y;
                        gameManager.changeGrid(ex, ey, 1);
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
                Instantiate(egg, new Vector2(pos1.x * pixel, pos1.y * pixel), transform.rotation);
                GameObject.FindGameObjectWithTag("Egg2").GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
                ex = (int)pos1.x;
                ey = (int)pos1.y;
                gameManager.changeGrid(ex, ey, 1);
            }
        }
    }


    public Vector2 returnPreviousPosition()
    {
        return new Vector2(prevX, prevY);
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
