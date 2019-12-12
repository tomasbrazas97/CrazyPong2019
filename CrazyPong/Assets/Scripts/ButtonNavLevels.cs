using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonNavLevels : MonoBehaviour
{
    int index = 0;
    public int totalLevels = 3;
    public float yOffset = .88f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(index < totalLevels - 1)
            {
                Sound.PlaySound("beep");
                index++;
                Vector2 position = transform.position;
                position.y -= yOffset;
                transform.position = position;

            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (index > 0)
            {
                Sound.PlaySound("beep");
                index--;
                Vector2 position = transform.position;
                position.y += yOffset;
                transform.position = position;

            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Sound.PlaySound("peep");
            if (index == 0)
            {
                SceneManager.LoadScene ("Regular_Easy");
            }
            if (index == 1)
            {
                SceneManager.LoadScene("Double Trouble");
            }
            if (index == 2)
            {
                SceneManager.LoadScene("Crazy");
            }
        }
    }
}
