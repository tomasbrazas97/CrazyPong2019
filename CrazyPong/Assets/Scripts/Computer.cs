using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    public float topBounds = 10.3f;
    public float bottomBounds = -10.0f;
    public Vector2 startingPosition = new Vector2(13.0f, 0.0f);

    private GameObject ball;
    private Vector2 ballPos;

    private Game game;

    // Start is called before the first frame update
    void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "Regular_Easy")
        {
            moveSpeed = 12.0f;
        }
        if (sceneName == "Regular_Medium")
        {
            moveSpeed = 15.0f;
        }
        if (sceneName == "Regular_Hard")
        {
            moveSpeed = 20.0f;
        }
        if (sceneName == "Crazy")
        {
            moveSpeed = 20.0f;
        }

        game = GameObject.Find("Game").GetComponent<Game>();
        transform.localPosition = (Vector3)startingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (game.gameState == Game.GameState.Playing)
        {
            Move();
        }
    }

    void Move()
    {
        if (!ball)
        {
            ball = GameObject.FindGameObjectWithTag("ball");
        }

        if(ball.GetComponent<Ball>().ballDirection == Vector2.right)
        {
            ballPos = ball.transform.localPosition;

            if(transform.localPosition.y > bottomBounds && ballPos.y < transform.localPosition.y)
            {
                transform.localPosition += new Vector3(0, -moveSpeed * Time.deltaTime, 0);
            }

            if (transform.localPosition.y < topBounds && ballPos.y > transform.localPosition.y)
            {
                transform.localPosition += new Vector3(0, moveSpeed * Time.deltaTime, 0);
            }
        }
    }
    
}

