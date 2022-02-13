using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static AudioClip doorOpened, pickHitLock, pickBroke;
    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        doorOpened = Resources.Load<AudioClip>("SoundEffects/DoorOpen");
        pickHitLock = Resources.Load<AudioClip>("SoundEffects/PickHittingLock");
        pickBroke = Resources.Load<AudioClip>("SoundEffects/PickBreaking");
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "DoorOpen":
                audioSource.PlayOneShot(doorOpened, 0.1f);
                break;
            case "PickHit":
                audioSource.PlayOneShot(pickHitLock, 0.1f);
                break;
            case "PickBroke":
                audioSource.PlayOneShot(pickBroke, 0.1f);
                break;
        }
    }
}
