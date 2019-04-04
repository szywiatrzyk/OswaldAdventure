using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    public  int score;
    Text text;
	// Use this for initialization
	void Start () {
        score = 0;
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate() {
        score = GameLogic.instance.score; 
        text.text = "Score: " + score;
        }
}
