using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour
{


    public Vector3 startPosition;
    public Vector3 endPosition;
    public float startOffset, currentOffset;
    public float distance;
    public float playerDistance;
    public float temporal;

    public Sprite[] moonSprite;
    public Sprite currentMoonSprite;
    
    void Start()
    {
        startOffset = currentOffset = 8f;
        distance = endPosition.x - startPosition.x;     
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixedUpdate()
    {
        playerDistance = Player.instance.transform.position.x - startPosition.x;
        currentOffset = startOffset - 30 * (playerDistance / (distance*10));
        transform.position = new Vector3(/*Player.instance.transform.position.x*/ Camera.main.transform.position.x + currentOffset, Camera.main.transform.position.y +3, 0);

        if (GameLogic.instance.currentLevel == 1) currentMoonSprite = moonSprite[0];
        if (GameLogic.instance.currentLevel == 2) currentMoonSprite = moonSprite[1];
        if (GameLogic.instance.currentLevel == 3) currentMoonSprite = moonSprite[2];
        if (GameLogic.instance.currentLevel == 4) currentMoonSprite = moonSprite[3];
        gameObject.GetComponent<SpriteRenderer>().sprite = currentMoonSprite;

    }
}
