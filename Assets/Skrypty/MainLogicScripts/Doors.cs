using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{


    public Vector3 whereGo;
    public int nextLevel;
    public bool isActive;
    public bool goexit;
    public bool canGo;
    public Sprite[] sprite;
    public bool alwaysOpen;
    

    // Use this for initialization
    void Start()
    {
        canGo = false;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameLogic.instance.LevelEnabled >= nextLevel || alwaysOpen)
        {
            isActive = true;
        }

        if (isActive)
        {
            canGo = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite[0];
        }
        else
        {
            canGo = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite[1];
        }
    }

void OnTriggerEnter2D(Collider2D collision)
    {
        if (canGo)
        {
            
            if (collision.tag == "Player")
            {
                if (goexit) { GameLogic.instance.GameQuit(); }
                else
                {
                    AudioManager.instance.PlayTeleport();
                    GameLogic.instance.LoadingScreen(1);
                    GameLogic.instance.SetLevel(nextLevel);
                    Player.instance.TeleportTo(whereGo);
                }

            }
        }
    }



    


}

