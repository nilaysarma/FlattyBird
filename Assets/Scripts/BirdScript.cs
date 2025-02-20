using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;
    public AudioSource jumpSFX;
    public AudioSource useHeartSFX;
    public AudioSource useShieldSFX;
    public AudioSource backgroundMusic;
    public int heartsLeft = 3;
    public bool hasHearts = true;
    // public GameObject heartMenu;
    public bool intangible = false;
    private CircleCollider2D circleCollider;
    public float intangibleTimeLeft = 3f;
    public Text heartsLeftText;
    public GameObject heartLostEffect;
    public GameObject heartImage;
    public GameObject heartGIF;
    public GameObject noHeartImage;
    public GameObject shieldEffect;
    public float shieldTime = 10f;
    public bool isShieldInUse = false;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        heartsLeftText.text = heartsLeft.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(intangible)
        {
            circleCollider.isTrigger = true;
            // circleCollider.enabled = false;
        }
        else
        {
            circleCollider.isTrigger = false;
            // circleCollider.enabled = true;
        }

        if(logic.isGameOver == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
            {
                Jump();
            }
        }

        if(logic.isGameOver == false && intangible == false)
        {
            if (transform.position.y > 13 || transform.position.y < -13)
            {
                logic.gameOver();
                birdIsAlive = false;
            }
        }

        /*
        if(logic.isGameOver == true && hasHearts)
        {
            if(Input.GetKeyDown(KeyCode.X) && birdIsAlive == false)
            {
                UseHeart();
            }
        }
        */
    }

    private void Jump()
    {
        myRigidBody.linearVelocity = Vector2.up * flapStrength;
        jumpSFX.Play();
    }

    public void TapJump()
    {
        if(logic.isGameOver == false)
        {
            Jump();
        }
    }

    public void UseHeart()
    {
        intangible = true;  // cannot be dead by touching pillars or anything else.
        // heartMenu.SetActive(false);
        // Time.timeScale = 1f;
        birdIsAlive = true;
        transform.position = new Vector3(0, 0, 0);  // Alternately: Vector3.zero;
        myRigidBody.linearVelocity = new Vector2(0, 0);
        useHeartSFX.Play();
        heartLostEffect.SetActive(true);

        if (PlayerPrefs.GetInt("vibrate", 1) == 1)
        {
            Vibrator.Vibrate(150);
        }

        // Debug.Log("Bird Revived");
        heartsLeft--;
        heartsLeftText.text = heartsLeft.ToString();

        if(heartsLeft < 2)
        {
            heartImage.SetActive(false);
            heartGIF.SetActive(true);
        }

        if(heartsLeft < 1)
        {
            hasHearts = false;
            heartGIF.SetActive(false);
            noHeartImage.SetActive(true);
        }

        Invoke("DisableIntangiblity", intangibleTimeLeft);
    }

    private void DisableIntangiblity()
    {
        heartLostEffect.SetActive(false);
        if (isShieldInUse == false)
        {
            intangible = false;
            // Debug.Log("Disabled Intangibility");
        }
    }

    public void UseShield()
    {
        isShieldInUse = true;
        intangible = true;
        backgroundMusic.volume = 0.4f;
        useShieldSFX.Play();
        shieldEffect.SetActive(true);
        Invoke("DisableShield", shieldTime);
    }

    private void DisableShield()
    {
        isShieldInUse = false;
        intangible = false;
        shieldEffect.SetActive(false);
        backgroundMusic.volume = 0.8f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(logic.isGameOver == false)
        {
            if (intangible == false)
            {
                logic.gameOver();
                birdIsAlive = false;
            }
        }
    }
}
