using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerGenerator : MonoBehaviour {
    public static TowerGenerator instance;
    public List<GameObject> platforms;
    public List<GameObject> spawnOnPlatform;
    float elapsedTime = 0f;
    public float platformSpawnTime;
    public float downSpeed;
    public float width;
    public float odl;
    float speed = 1f;
    System.Random rand;
    UnityEngine.Object platformPrefab;
    UnityEngine.Object heartPrefab;
    UnityEngine.Object coinPrefab;
    UnityEngine.Object spikePrefab;
    float currentYPosition = 0;
    // Use this for initialization
    void Start () {
        instance = this;
        platforms = new List<GameObject>();
        spawnOnPlatform = new List<GameObject>();
        rand = new System.Random(Guid.NewGuid().GetHashCode());
        platformPrefab = Resources.Load("ground3");
        coinPrefab = Resources.Load("Coin");
        heartPrefab = Resources.Load("Heart");
        spikePrefab = Resources.Load("Spike");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if(GameLogic.instance.currentLevel == 3 && Time.timeScale > 0)
        {
            
            for(int i = 0; i < platforms.Count; ++i)
            {
                if (platforms[i] == null) continue;
                if(platforms[i].transform.position.y < Player.instance.transform.position.y - 10)
                {
                    Destroy(platforms[i]);
                    platforms.Remove(platforms[i]);
                    continue;
                }
                platforms[i].transform.position = new Vector3(platforms[i].transform.position.x, platforms[i].transform.position.y - downSpeed * speed, platforms[i].transform.position.z);
            }

            for (int i = 0; i < spawnOnPlatform.Count; ++i)
            {
                if (spawnOnPlatform[i] == null) continue;
                if (spawnOnPlatform[i].transform.position.y < Player.instance.transform.position.y - 10)
                {
                    Destroy(spawnOnPlatform[i]);
                    spawnOnPlatform.Remove(spawnOnPlatform[i]);
                    continue;
                }
                spawnOnPlatform[i].transform.position = new Vector3(spawnOnPlatform[i].transform.position.x, spawnOnPlatform[i].transform.position.y - downSpeed * speed, spawnOnPlatform[i].transform.position.z);
            }





            //speed += 0.01f;
            elapsedTime += Time.fixedDeltaTime;
            if(elapsedTime >= platformSpawnTime)
            {
                elapsedTime = 0f;
                float xPosition = (float)rand.NextDouble() * width + 1000;
                int probSpawn = (int)rand.Next(1, 100);
                if (probSpawn < 60)
                {
                    int randSpawn = (int)rand.Next(1, 11);
                    switch (randSpawn)
                    {
                        case 1:
                            spawnOnPlatform.Add(Instantiate(coinPrefab, new Vector3(xPosition, currentYPosition+odl, 0.24f), transform.rotation, transform) as GameObject);
                            break; //1-3 spawn coina
                        case 2:
                            spawnOnPlatform.Add(Instantiate(coinPrefab, new Vector3(xPosition, currentYPosition + odl, 0.24f), transform.rotation, transform) as GameObject);
                            break;
                        case 3:
                            spawnOnPlatform.Add(Instantiate(coinPrefab, new Vector3(xPosition, currentYPosition + odl, 0.24f), transform.rotation, transform) as GameObject);
                            break;

                        case 4:
                            spawnOnPlatform.Add(Instantiate(heartPrefab, new Vector3(xPosition, currentYPosition + odl, -1f), transform.rotation, transform) as GameObject);
                            break;//4-spawn serduszka

                        case 5:
                            spawnOnPlatform.Add(Instantiate(spikePrefab, new Vector3(xPosition-0.6f, currentYPosition + 0.4f, -1f), transform.rotation, transform) as GameObject);
                            spawnOnPlatform.Add(Instantiate(spikePrefab, new Vector3(xPosition - 0.6f, currentYPosition - 0.4f, -1f), Quaternion.Euler(new Vector3(0, 0, 180)), transform) as GameObject);
                            break;//5-6 spawn spikes
          
                        case 6:
                            spawnOnPlatform.Add(Instantiate(spikePrefab, new Vector3(xPosition + 0.6f, currentYPosition + 0.4f, -1f), transform.rotation, transform) as GameObject);
                            spawnOnPlatform.Add(Instantiate(spikePrefab, new Vector3(xPosition+ 0.6f, currentYPosition-0.4f, -1f), Quaternion.Euler(new Vector3(0, 0, 180)), transform) as GameObject);
                            break;

                        case 7:
                            spawnOnPlatform.Add(Instantiate(coinPrefab, new Vector3(xPosition, currentYPosition + odl, 0.24f), transform.rotation, transform) as GameObject);
                            break;
                        case 8:
                            spawnOnPlatform.Add(Instantiate(coinPrefab, new Vector3(xPosition, currentYPosition + odl, 0.24f), transform.rotation, transform) as GameObject);
                            break;

                        case 9:
                            spawnOnPlatform.Add(Instantiate(coinPrefab, new Vector3(xPosition, currentYPosition + odl, 0.24f), transform.rotation, transform) as GameObject);
                            break;

                        case 10:
                            spawnOnPlatform.Add(Instantiate(heartPrefab, new Vector3(xPosition, currentYPosition + odl, -1f), transform.rotation, transform) as GameObject);
                            break;//4-spawn serduszka
                    }

                }
                platforms.Add(Instantiate(platformPrefab, new Vector3(xPosition, currentYPosition, 0.24f), transform.rotation, transform) as GameObject);
                if (currentYPosition < 327) currentYPosition += 2;
                
            }
        }
    }

    public void Reset()
    {
        currentYPosition = Player.instance.respawnPosition.y;
        if (GameLogic.instance.currentLevel == 0) currentYPosition = 0;
        while (platforms.Count != 0)
        {
            for (int i = 0; i < platforms.Count; ++i)
            {
                if (platforms[i] == null) continue;
                Destroy(platforms[i]);
                platforms.Remove(platforms[i]);
            }

        }

        while (spawnOnPlatform.Count != 0)
        {
            for (int i = 0; i < spawnOnPlatform.Count; ++i)
            {
                if (spawnOnPlatform[i] == null) continue;
                Destroy(spawnOnPlatform[i]);
                spawnOnPlatform.Remove(spawnOnPlatform[i]);
            }

        }


    }

}
