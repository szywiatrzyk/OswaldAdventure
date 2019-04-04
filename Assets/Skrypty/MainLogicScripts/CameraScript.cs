using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {



    // Parametry kamery
    const float baseCameraHeight = 0.0f;   // Bazowa wysokość kamery
    const float heightThreshold = 2.0f;    // Wysokość, którą postać musi osiągnąć, by kamera zaczęła się podnosić
    const float cameraDistance = -10.0f;   // Oddalenie kamery od gracza (im dalej tym więcej świata widać)
    
    public float dampTime = 0.1f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 menuPosition;


    // Use this for initialization
    void Start () {
		menuPosition = new Vector3(-36.92f, 0, -10);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        
        
        // Kamera
        if(GameLogic.instance && GameLogic.instance.currentLevel == 0)
        {
            transform.position = menuPosition;
        }
        // Podążanie za graczem wzdłuż osi X z opóźnieniem dampTime
        else if (Player.instance && !GameLogic.instance.cameraLock)
        {
            Vector3 point = Camera.main.WorldToViewportPoint(Player.instance.transform.position);
            Vector3 delta = Player.instance.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            if (destination.y < 0) destination.y = 0;
            if(GameLogic.instance.currentLevel == 4)
            {
                destination.y += 3;
            }
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }

}
