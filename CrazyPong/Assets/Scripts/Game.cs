using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Game : MonoBehaviour
{
    private GameObject ball;
    private GameObject movBall;

    private int computerScore;
    private int playerScore;

    private GameObject hudCanvas;
    private HUD hud;

    private GameObject paddleComputer;

    public int winningScore = 10;
    public GameObject ballNav;
    int index = 0;

    public enum GameState
    {
        Playing,
        GameOver,
        Paused,
        Launched
    }

    public GameState gameState = GameState.Launched;
    // Start is called before the first frame update
    void Start()
    {
        paddleComputer = GameObject.Find("ComputerPaddle");
        ballNav = GameObject.Find("ball-green");
        ballNav.SetActive(false);

        hudCanvas = GameObject.Find("Canvas");
        hud = hudCanvas.GetComponent<HUD>();
        hud.playAgain.text = "Press Spacebar to play";

    }

    // Update is called once per frame
    void Update()
    {
        CheckScore();
        CheckInput();
    }

    void CheckInput()
    {
        if (gameState == GameState.Paused || gameState == GameState.Playing)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                PauseResumeGame();
            }
        }
        if (gameState == GameState.Launched || gameState == GameState.GameOver)
        {
            if(Input.GetKeyUp(KeyCode.Space))
            {
                StartGame();
            }
        }    
    }
    void CheckScore()
    {
        if (playerScore >= winningScore || computerScore >= winningScore)
        {
            if(playerScore >= winningScore && computerScore < playerScore - 1)
            {
                PlayerWins();
            } else if(computerScore >= winningScore && playerScore < computerScore - 1)
            {
                ComputerWins();
                
            }
        }
    }
    void SpawnBall()
    {
        // Create a temporary reference to the current scene.
         Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "Regular_Easy")
        {
            paddleComputer.GetComponent<Computer>().moveSpeed = 6f;
            ball = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/ball", typeof(GameObject)));
            ball.transform.localPosition = new Vector3(12, 0, 0);
        }
        if (sceneName == "Regular_Medium")
        {
            paddleComputer.GetComponent<Computer>().moveSpeed = 10f;
            ball = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/ball", typeof(GameObject)));
            ball.transform.localPosition = new Vector3(12, 0, 0);
        }
        if (sceneName == "Regular_Hard")
        {
            paddleComputer.GetComponent<Computer>().moveSpeed = 12f;
            ball = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/ball", typeof(GameObject)));
            ball.transform.localPosition = new Vector3(12, 0, 0);
        }
        if (sceneName == "Crazy")
        {
            ball = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/ball", typeof(GameObject)));
            ball.transform.localPosition = new Vector3(12, 0, 0);
        }
        if (sceneName == "Double Trouble")
        {
            paddleComputer.GetComponent<Computer>().moveSpeed = 12f;
            ball = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/ball", typeof(GameObject)));
            ball.transform.localPosition = new Vector3(12, 0, 0);
        }

    }

    private void PlayerWins()
    {      
        hud.winPlayer.enabled = true;
        GameOver();
    }

    private void ComputerWins()
    {
        hud.winComputer.enabled = true;
        GameOver();
    }

    public void ComputerPoint()
    {
        computerScore++;
        hud.computerScore.text = computerScore.ToString();
        NextRound();
    }

    public void PlayerPoint()
    {
        playerScore++;
        hud.playerScore.text = playerScore.ToString();
        NextRound();
    }

    private void StartGame()
    {
        playerScore = 0;
        computerScore = 0;

        hud.playerScore.text = "0";
        hud.computerScore.text = "0";
        hud.winComputer.enabled = false;
        hud.winPlayer.enabled = false;
        hud.playAgain.enabled = false;

        gameState = GameState.Playing;
        paddleComputer.transform.localPosition = new Vector3(paddleComputer.transform.localPosition.x, 0, paddleComputer.transform.localPosition.z);
        SpawnBall();
    }

    private void NextRound()
    {
        if (gameState == GameState.Playing)
        {
            paddleComputer.transform.localPosition = new Vector3(paddleComputer.transform.localPosition.x, 0, paddleComputer.transform.localPosition.z);
            GameObject.Destroy(ball.gameObject);
            SpawnBall();
        }
    }

    private void GameOver()
    {
        GameObject.Destroy(ball.gameObject);
        hud.playAgain.text = "Press Spacebar to Retry";
        hud.playAgain.enabled = true;
        gameState = GameState.GameOver;
    }

    private void PauseResumeGame()
    {
        if(gameState == GameState.Paused)
        {
            gameState = GameState.Playing;
            hud.playAgain.enabled = false;
            hud.levels.enabled = false;
            hud.difficulty.enabled = false;
            hud.quit.enabled = false;
            ballNav.SetActive(false);

        } else
        {
            gameState = GameState.Paused;
            hud.playAgain.text = "PAUSED, press Space to Unpause";
            hud.playAgain.enabled = true;
            hud.levels.enabled = true;
            hud.difficulty.enabled = true;
            hud.quit.enabled = true;
            ballNav.SetActive(true);

        }
    }
}

