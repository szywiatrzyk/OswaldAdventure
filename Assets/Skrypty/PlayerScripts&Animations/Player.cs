using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasa <c>Player</c>.
/// Zawiera parametry i metody dot. postaci kierowanej przez gracza.
/// </summary>
public class Player : MonoBehaviour
{
    // Parametry gracza
    public int life = 3;                   // Początkowa liczba żyć
    public float speed;                    // Szybkość poruszania (10)
    public float jumpForce;                // Siła skoku (15)
    public Vector3 respawnPosition;               // Przechowuje obecną pozycję respawnu gracza po utracie życia
    Vector3 startPosition;                 // Przechowuje pozycję startową gracza; jeśli gracz straci wszystkie życia, ustawiana jest jako respawnPosition
    public Vector2 jumpVector;                    // Wektor nadawany postaci po wciśnięciu przycisku skoku
    public static Player instance; // Przy pomocy instance inne skrypty mogą się odwołać do parametrów lub metod klasy Player, bo jest singletonem
    public Rigidbody2D ruch;              // Komponent odpowiadający za fizykę postaci
    private SpriteRenderer character;      // Odpowiada za wyświetlany obrazek gracza
    private bool goRight = true;           // Czy gracz idzie w prawo; obrót sprite'a
    public bool grounded;                 // Czy gracz znajduje się na ziemi; nie pozwala na double jumpy
    public Transform groundCheck;          // Zmienna pozwalająca silnikowi sprawdzić czy gracz stoi na ziemi (tylko w tym momencie można skoczyć)
    float move;                            // Nadaje prędkość postaci w poziomie (oś X)
    private float checkpointTest;          // Współrzędna 'x' ostatniego aktywowanego checkpointa
    public float groundradius;             // 
    public LayerMask czyground;            // 
    public Vector3 currentPosition;        // Przechowuje wartość wektora pozycji gracza 
    public bool movementAllowed;
    
    
    // Animacje 
    private GameObject death;              // Umieranie - zmienianie sprite
    public Animator anim;                         // Animacje ruchu

    
 

    /// <summary>
    /// Inicjacja postaci, ustawianie parametrów.
    /// </summary>
    void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
        respawnPosition = new Vector3(transform.position.x, 4, 0);
        ruch = GetComponentInChildren<Rigidbody2D>();
        character = GetComponentInChildren<SpriteRenderer>();
        startPosition = new Vector3(transform.position.x, transform.position.y+2, 0);
        checkpointTest = transform.position.x;
        transform.position = startPosition;
        jumpVector = new Vector2(0, jumpForce);
        death = GameObject.FindWithTag("death"); //animacja śmierci
        death.SetActive(false);                  //bool sprawdzający czy animacja śmierci ma się odpalić
        movementAllowed = true;
    }
    
    /// <summary>
    /// Funkcja wywoływana co każdą klatkę.
    /// </summary>
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundradius, czyground);
        if (ruch.velocity.y == 0)
        {
            grounded = true;
        }

        if (!movementAllowed)
        {
            if (ruch.velocity.y == 0) movementAllowed = true;
        }

        if (grounded == false)
        {
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }
        if (movementAllowed && Time.timeScale > 0)
        {
            // Ruch postaci
            // Skok klawiszem 'Z' jeśli gracz jest na ziemi
            if (Input.GetKeyDown(KeyCode.Z) && grounded)
            {
                AudioManager.instance.PlayJump();
                ruch.velocity = jumpVector;
            }

            // Ruch na boki
            move = Input.GetAxis("Horizontal");                         // Pobieranie wartości przycisku
            ruch.velocity = new Vector2(move * speed, ruch.velocity.y); // Przemieszczenie / Nadanie postaci prędkości w osi X
                                                                        //anim.SetFloat("spd", Mathf.Abs(move));                    // Animator Move

            // Obrót sprite'a zgodnie z kierunkiem poruszania
            if (ruch.simulated)
            {
                if ((move > 0) && !goRight) FlipCharacterSprite();
                else if ((move < 0) && goRight) FlipCharacterSprite();
            }
        }

        if (Mathf.Abs(move) > 0 && grounded) anim.SetBool("move", true);
        else anim.SetBool("move", false);
    }
    
    /// <summary>
    /// Funkcja wywoływana co ustalony framerate.
    /// </summary>
    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P) && grounded)
        {
            respawnPosition = transform.position;
            life = 3;
        }

        currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z); // co klatkowe aktualizowanie pozycji      
    }

    /// <summary>
    /// Funkcja obsługująca akcje gracza przy zderzeniu się z różnymi obiektami na mapie.
    /// </summary>
    /// <param name="collision">Collider, z którym zderzył się gracz.</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Spadnięcie pod mapę lub wejście na przeciwnika
        if (collision.tag == "falldetector" || collision.tag == "enemycolider" || collision.tag == "beam")
        {
            anim.SetBool("death", true); //wystartowanie animacji śmierci
            StartCoroutine(Slowdeath());
        }

        if (collision.tag == "spikes")
        {
            anim.SetBool("death", true); //wystartowanie animacji śmierci
            StartCoroutine(Slowdeath());
        }

        // Aktywacja kolejnego checkpointa
        if (collision.tag == "checkpoint")
        {
            if (collision.GetComponent<CheckpointControler>().passed == false)
            {
                AudioManager.instance.PlayCheckpoint();
                collision.gameObject.GetComponent<CheckpointControler>().passed = true;
                checkpointTest = transform.position.x;
                respawnPosition = new Vector3(transform.position.x, transform.position.y, 0);
            }
        }

        // Podniesienie serduszka
        if (collision.tag == "hp_up")
        {
            if(life != 3)  life += 1;
        }
/*
        // Dojście do końca planszy/mety
        if (collision.tag == "endscene")
        {
            GameLogic.instance.panelwin.SetActive(true);              
            ruch.simulated = false;
        }
        */
    }

    /// <summary>
    /// Obrót sprite'a postaci.
    /// </summary>
    void FlipCharacterSprite()
    {
        goRight = !goRight;
        character.flipX = goRight;
    }    

    /// <summary>
    /// Funkcja pomocnicza respawnu.
    /// </summary>
    IEnumerator Slowdeath()
    {
        // Deaktywacja ruchu, zmiana liczby żyć, przeniesienie gracza do checkpointa
        AudioManager.instance.PlayDeath();
        ruch.simulated = false;
        grounded = false;
        life -= 1;
        death.SetActive(true);
        yield return new WaitForSeconds(1);
        anim.SetBool("death", false);
        GameLogic.instance.Resetup();
        Boss.instance.elapsedTime = 0;
        Boss.instance.attackType = 0;
        Boss.instance.attackMode = false;
        if (life == 0)
        {
            respawnPosition = startPosition;
            checkpointTest = startPosition.x;
            life = 3;
            
        }
        Level3ColiderScript.instance.setHeight();
        TowerGenerator.instance.Reset();
        death.SetActive(false);
        transform.position = respawnPosition;
        ruch.simulated = true;
        ruch.velocity = new Vector2(0, -0.01f);
        movementAllowed = false;
    }


    public void TeleportTo(Vector3 destination)
    {
        transform.position = destination;

    }



}
