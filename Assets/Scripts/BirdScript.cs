using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;
    public AudioSource jumpSFX;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(logic.isGameOver == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
            {
                Jump();
            }
        }

        if(logic.isGameOver == false)
        {
            if (transform.position.y > 13 || transform.position.y < -13)
            {
                logic.gameOver();
            }
        }
    }

    private void Jump()
    {
        myRigidBody.velocity = Vector2.up * flapStrength;
        jumpSFX.Play();
    }

    public void TapJump()
    {
        if(logic.isGameOver == false)
        {
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(logic.isGameOver == false)
        {
            logic.gameOver();
            birdIsAlive = false;
        }
    }
}
