using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3ColiderScript : MonoBehaviour {

    public static Level3ColiderScript instance;
    public Vector3 currentPosition;
    public float height;
    public float speed;
    // Use this for initialization
    void Start () {
        instance = this;
        currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        height = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
   void FixedUpdate()
    {
        
        if (GameLogic.instance.currentLevel == 3 && Time.timeScale > 0)
        {
            if (transform.position.y < 315.8f)
            {
                transform.position = new Vector3(currentPosition.x, currentPosition.y + height, currentPosition.z);
                height = height + speed;
            }
        }
        
    }

    public void setHeight()
    {
        height = 0;
        transform.position = currentPosition = new Vector3(currentPosition.x, Player.instance.respawnPosition.y - 26, currentPosition.z);

    }
}
