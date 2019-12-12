using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static AudioClip beepSound, peepSound, plopSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        beepSound = Resources.Load<AudioClip>("Sounds/beep");
        peepSound = Resources.Load<AudioClip>("Sounds/peep");
        plopSound = Resources.Load<AudioClip>("Sounds/plop");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "beep":
                audioSrc.PlayOneShot(beepSound);
                break;
            case "peep":
                audioSrc.PlayOneShot(peepSound);
                break;
            case "plop":
                audioSrc.PlayOneShot(plopSound);
                break;
        }
    }
}
