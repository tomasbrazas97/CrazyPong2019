﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 8.0f;
    public float topBounds = 8.3f;
    public float bottomBounds = -8.3f;
    public Vector2 startingPosition = new Vector2(-6.0f, 0.0f);

    private Game game;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("Game").GetComponent<Game>();
        transform.localPosition = (Vector3)startingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (game.gameState == Game.GameState.Playing)
        {
            CheckUserInput();
        }
    }

    void CheckUserInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.localPosition.y >= topBounds)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, topBounds, transform.localPosition.z);
            } else
            {
                transform.localPosition += Vector3.up * moveSpeed * Time.deltaTime;
            }

        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.localPosition.y <= bottomBounds)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, bottomBounds, transform.localPosition.z);
            }
            else
            {
                transform.localPosition += Vector3.down * moveSpeed * Time.deltaTime;
            }
        } 
    }
}
