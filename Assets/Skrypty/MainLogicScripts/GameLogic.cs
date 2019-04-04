using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    public int LevelEnabled;

    public GameObject[] hpups;            // Tablica do przechowywania pickupów z mapy; do resetowania pickupów (tag "hp_up")
    public GameObject[] coins;
    public GameObject[] checkpoints;       // Tablica do przechowywania checkpointów z mapy; do resetowania flag (tag "checkpoint")
    public GameObject[] enemy;
    public static GameLogic instance;
    private bool pause;
    public GameObject panelwin;           // Ekran pojawiający się po dojściu do mety
    public GameObject loadingScreen;
    public GameObject gameoverscreen;
    public GameObject BossHP;
    public int currentLevel;
    public int score;                    //Punkty do zbieranaia
    public bool cameraLock = false;
    public bool pausecanvas = false;
    private GameObject Pause;
    void Start () {
        currentLevel = 0;
        score = 0;
        pause = false;
       // panelwin = GameObject.FindWithTag("panelwin");
       // panelwin.SetActive(false);

        instance = this;
        hpups = GameObject.FindGameObjectsWithTag("hp_up");            //wyszukiwanie obiektu "hp_up" w silniku gry
        coins = GameObject.FindGameObjectsWithTag("coin");
        enemy = GameObject.FindGameObjectsWithTag("enemy");
        checkpoints = GameObject.FindGameObjectsWithTag("checkpoint");  //wyszukiwanie obiektu "checkpoint" w silniku gry
        loadingScreen = GameObject.FindGameObjectWithTag("loadingscreen");
        loadingScreen.SetActive(false);
        BossHP = GameObject.FindGameObjectWithTag("BossHP");
        BossHP.SetActive(false);
        gameoverscreen = GameObject.FindGameObjectWithTag("gameoverscreen");
        gameoverscreen.SetActive(false);
        Pause = GameObject.FindGameObjectWithTag("Pause");
        Pause.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.instance.PlayPause();
            pausecanvas = !pausecanvas;
            Pause.SetActive(pausecanvas);
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;  
            }
        }
    }

    private void FixedUpdate()
    {
       
        if (currentLevel == 4 && !cameraLock) BossHP.SetActive(true);


        if (cameraLock && Player.instance.transform.position.y < 1000f)
        {
            Vector3 playerPosition = Player.instance.transform.position;
            Player.instance.transform.position = new Vector3(playerPosition.x, playerPosition.y + 0.01f, playerPosition.z);
        }
        if (Player.instance.transform.position.y > 304 && cameraLock) {

           
            Color tmp = GameObject.FindGameObjectWithTag("panelwin").GetComponent<Image>().color;
            if (tmp.a < 1)
            {
                tmp.a = tmp.a + 0.002f;
                GameObject.FindGameObjectWithTag("panelwin").GetComponent<Image>().color = tmp;

            }
            else GameQuit();




        }


    }

    public void SetLevel(int level)
    {
        if (LevelEnabled < currentLevel)
        {
            LevelEnabled = currentLevel;
        }

        switch(currentLevel)
        {
            case 0:
                StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.MenuMusic, 1f, level));
                break;
            case 1:
                StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.Track1, 1f, level));
                break;
            case 2:
                StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.Track2, 1f, level));
                break;
            case 3:
                StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.Track3, 1f, level));
                break;
            case 4:
                StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.Track4, 1f, level));
                break;
        }

        currentLevel = level;
        switch(level){
            case 0:
                Player.instance.jumpVector = new Vector2(0, Player.instance.jumpForce);
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                Player.instance.jumpVector = new Vector2(0, 25);
                break;
            case 4:
                Player.instance.jumpVector = new Vector2(0, Player.instance.jumpForce);
                break;
        }
      
    }

    public void Resetup() //funkcja resetująca wszyskie obiekty na scenie w przypadku śmierci gracza
    {

        if (Player.instance.life == 0)
        {
            BossHP.SetActive(false);
            if (Player.instance.life == 0) Boss.instance.Reset();
            LoadingScreen(2);
            SetLevel(0);

            foreach (GameObject q in checkpoints)
            {
                q.GetComponent<CheckpointControler>().Reset();
            }
            foreach (GameObject g in hpups)
            {
                g.SetActive(true);
            }
            foreach (GameObject x in coins)
            {
                x.SetActive(true);
            }
            ScoreAdd(100);

        }
        foreach (GameObject w in enemy)
        {
            w.GetComponent<EnemyHunter>().Reset();
        }
    }

     public void ScoreAdd(int mode)
    {
        if (mode == 1) score += 100;  //Case Coin
        if (mode == 2) score += 50;   //Case Hp_UP
        if (mode == 100) score = 0;   // Case Detah
    }

    void GamePause()
    {
        Player.instance.ruch.simulated = !Player.instance.ruch.simulated;
        Player.instance.anim.enabled = !Player.instance.anim.enabled;


        //Application.Quit();
    }

    public void LoadingScreen(int type)
    {
        StartCoroutine(LoadingTime(type));

    }

    IEnumerator LoadingTime(int type)
    {
        Player.instance.ruch.simulated = false;
        if (type == 1)
        {
            loadingScreen.SetActive(true);
            yield return new WaitForSeconds(2);
            loadingScreen.SetActive(false);
        }
        if(type == 2)
        {
            gameoverscreen.SetActive(true);
            yield return new WaitForSeconds(3);
            gameoverscreen.SetActive(false);
        }
        Player.instance.ruch.simulated = true;
        Player.instance.ruch.velocity = new Vector2(0, 0);
        
    }
    public void GameQuit() {
        Application.Quit();
    }

}
