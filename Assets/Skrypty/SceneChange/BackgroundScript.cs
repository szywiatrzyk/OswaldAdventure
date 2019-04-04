using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {
    public Sprite[] backGrounds;
    public Sprite currentBackGround;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (GameLogic.instance.currentLevel == 1) currentBackGround = backGrounds[0];
        if (GameLogic.instance.currentLevel == 2) currentBackGround = backGrounds[1];
        if (GameLogic.instance.currentLevel == 3) currentBackGround = backGrounds[2];
        if (GameLogic.instance.currentLevel == 4) currentBackGround = backGrounds[3];
        gameObject.GetComponent<SpriteRenderer>().sprite = currentBackGround;
    }
}
