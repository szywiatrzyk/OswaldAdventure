using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
        StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.Track4, 1f, 0));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
