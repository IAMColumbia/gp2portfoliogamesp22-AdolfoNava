using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    public static AudioClip MeleeDeath, BossDeath, RangedDeath;
    private static AudioSource AudioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        AudioPlayer = GetComponent<AudioSource>();

        MeleeDeath = Resources.Load<AudioClip>("Audio/Melee");
        BossDeath = Resources.Load<AudioClip>("Audio/Boss");
        RangedDeath = Resources.Load<AudioClip>("Audio/Ranged");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void PlayDeathAudio(string name)
    {
        switch (name)
        {
            case "Melee":
                AudioPlayer.PlayOneShot(MeleeDeath);
                break;
            case "Ranged":
                AudioPlayer.PlayOneShot(RangedDeath);
                break;
            case "Boss":
                AudioPlayer.PlayOneShot(BossDeath);
                break;
            default:
                break;
        }
    }
}
