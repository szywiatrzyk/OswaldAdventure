using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntDetector : MonoBehaviour {

    private float coliderRadius;

    void Start () {
        coliderRadius = GetComponent<CircleCollider2D>().radius /6.76f;
	}
	
	
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.tag == "Player")
        {
            this.GetComponentInParent<EnemyHunter>().Target(coliderRadius);
        }
    }
 }
