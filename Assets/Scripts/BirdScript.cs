using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapForce;
    public LogicScript logic;
    public bool birdIsAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        // Insert LogicScript into logic reference
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set upward velocity
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && birdIsAlive)
        {
            myRigidbody.velocity = Vector2.up * flapForce;
        }

        // Pause game
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }

        // Restart game
        if (Input.GetKeyDown(KeyCode.R))
        {
            logic.gameOver();
            birdIsAlive = false;
        }

        // Kill bird upon falling off the screen
        if (transform.position.y < -14)
        {
            logic.gameOver();
            birdIsAlive = false;
            Destroy(gameObject);
        }
    }

    // Kill bird upon collision with other rigidbody
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (birdIsAlive == true)
        {
            logic.gameOver();
        }
        birdIsAlive = false;
    }
}