using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float moveSpeed = 12.0f;
    public Vector2 ballDirection = Vector2.left;
    public float topBounds = 10.4f;
    public float bottomBounds = -10.4f;

    private float playerPaddleHeight, playerPaddleWidth, computerPaddleHeight, computerPaddleWidth, playerPaddleMaxX, playerPaddleMaxY, playerPaddleMinX, playerPaddleMinY,
                    computerPaddleMaxX, computerPaddleMaxY, computerPaddleMinX, computerPaddleMinY, ballWidth, ballHeight;
    private GameObject paddlePlayer, paddleComputer;

    private float bounceAngle;
    private float vx, vy;
    private float maxAngle = 45.0f;

    private bool collidedWithPlayer, collidedWithComputer, collidedWithWall;

    private Game game;

    private bool assignedPoint;
    // Start is called before the first frame update
    void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "Regular_Easy")
        {
            moveSpeed = 15.0f;
        }
        if (sceneName == "Regular_Medium")
        {
            moveSpeed = 15.0f;
        }
        if (sceneName == "Regular_Hard")
        {
            moveSpeed = 20.0f;
        } 

        game = GameObject.Find("Game").GetComponent<Game>();

        if (moveSpeed < 0)
        {
            moveSpeed = -1 * moveSpeed;
        }

        paddlePlayer = GameObject.Find("PlayerPaddle");
        paddleComputer = GameObject.Find("ComputerPaddle");

        playerPaddleHeight = paddlePlayer.transform.GetComponent<SpriteRenderer>().bounds.size.y;
        playerPaddleWidth = paddlePlayer.transform.GetComponent<SpriteRenderer>().bounds.size.x;
        computerPaddleHeight = paddleComputer.transform.GetComponent<SpriteRenderer>().bounds.size.y;
        computerPaddleWidth = paddleComputer.transform.GetComponent<SpriteRenderer>().bounds.size.x;

        ballHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y;
        ballWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x;

        playerPaddleMaxX = paddlePlayer.transform.localPosition.x + playerPaddleWidth / 2;
        playerPaddleMinX = paddlePlayer.transform.localPosition.x - playerPaddleWidth / 2;

        computerPaddleMaxX = paddleComputer.transform.localPosition.x - computerPaddleWidth / 2;
        computerPaddleMinX = paddleComputer.transform.localPosition.x + computerPaddleWidth / 2;

        bounceAngle = GetRandomBounceAngle();

        vx = moveSpeed * Mathf.Cos(bounceAngle);
        vy = moveSpeed * -Mathf.Sin(bounceAngle);
    }

    // Update is called once per frame
    void Update()
    {
        if (game.gameState != Game.GameState.Paused)
        {
            Move();
        }
    }

    bool CheckCollision()
    {
        playerPaddleMaxY = paddlePlayer.transform.localPosition.y + playerPaddleHeight / 2;
        playerPaddleMinY = paddlePlayer.transform.localPosition.y - playerPaddleHeight / 2;

        computerPaddleMaxY = paddleComputer.transform.localPosition.y + computerPaddleHeight / 2;
        computerPaddleMinY = paddleComputer.transform.localPosition.y - computerPaddleHeight / 2;

        if (transform.localPosition.x - ballWidth / 2 < playerPaddleMaxX && transform.localPosition.x + ballWidth / 2 > playerPaddleMinX)
        {
            if (transform.localPosition.y - ballHeight / 2 < playerPaddleMaxY && transform.localPosition.y + ballHeight / 2 > playerPaddleMinY)
            {
                Sound.PlaySound("beep");
                ballDirection = Vector2.right;
                collidedWithPlayer = true;
                transform.localPosition = new Vector3(playerPaddleMaxX + ballWidth / 2, transform.localPosition.y, transform.localPosition.z);
                return true;
            } else
            {

                if (!assignedPoint)
                {
                    assignedPoint = true;
                    game.ComputerPoint();
                }
            }
        }

        if (transform.localPosition.x + ballWidth / 2 > computerPaddleMaxX && transform.localPosition.x - ballWidth / 2 < computerPaddleMinX)
        {
            if (transform.localPosition.y - ballHeight / 2 < computerPaddleMaxY && transform.localPosition.y + ballHeight / 2 > computerPaddleMinY)
            {
                Sound.PlaySound("beep");
                ballDirection = Vector2.left;
                collidedWithComputer = true;
                transform.localPosition = new Vector3(computerPaddleMaxX - ballWidth / 2, transform.localPosition.y, transform.localPosition.z);
                return true;
            }
            else
            {
                if (!assignedPoint)
                {
                    assignedPoint = true;
                    game.PlayerPoint();
                }
            }
        }

        if (transform.localPosition.y > topBounds)
        {
            Sound.PlaySound("beep");
            transform.localPosition = new Vector3(transform.localPosition.x, topBounds, transform.localPosition.z);
            collidedWithWall = true;
            return true;
        }

        if (transform.localPosition.y < bottomBounds)
        {
             Sound.PlaySound("beep");
            transform.localPosition = new Vector3(transform.localPosition.x, bottomBounds, transform.localPosition.z);
            collidedWithWall = true;
            return true;
        }
        return false;
    }

    void Move()
    {
        if (!CheckCollision())
        {
            vx = moveSpeed * Mathf.Cos(bounceAngle);
            if (moveSpeed > 0)
            {
                vy = moveSpeed * -Mathf.Sin(bounceAngle);
            }
            else
            {
                vy = moveSpeed * Mathf.Sin(bounceAngle);
            }

            transform.localPosition += new Vector3(ballDirection.x * vx * Time.deltaTime, vy * Time.deltaTime, 0);
        }
        else
        {
            if (moveSpeed < 0)
            {
                moveSpeed = -1 * moveSpeed;
            }

            if (collidedWithPlayer)
            {
                collidedWithPlayer = false;
                float relativeIntersectY = paddlePlayer.transform.localPosition.y - transform.localPosition.y;
                float normalizedRelativeIntersectionY = (relativeIntersectY / (playerPaddleHeight / 2));

                bounceAngle = normalizedRelativeIntersectionY * (maxAngle * Mathf.Deg2Rad);
            } else if (collidedWithComputer)
            {
                collidedWithComputer = false;
                float relativeIntersectY = paddleComputer.transform.localPosition.y - transform.localPosition.y;
                float normalizedRelativeIntersectionY = (relativeIntersectY / (computerPaddleHeight / 2));

                bounceAngle = normalizedRelativeIntersectionY * (maxAngle * Mathf.Deg2Rad);
            } else if (collidedWithWall)
            {
                collidedWithWall = false;
                bounceAngle = -bounceAngle;
            }
        }

    }

    float GetRandomBounceAngle (float minDegrees = 160f, float maxDegrees = 260f)
    {
        float minRad = minDegrees * Mathf.PI / 180;
        float maxRad = maxDegrees * Mathf.PI / 180;

        return Random.Range(minRad, maxRad);
    }
}
