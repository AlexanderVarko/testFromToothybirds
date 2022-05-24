using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager singleton { get; private set; }
    #region Variables
    public AudioClip walkingSound;
    public AudioClip jumpingSound;
    public AudioClip pickUpSound;
    public AudioSource musicPlayer;
    public AudioClip levelMusic;
    public AudioClip[] music;
    private AudioSource soundFx;
    #endregion
    void Start()
    {
        singleton = this;
        soundFx = Camera.main.GetComponent<AudioSource>();
        levelMusic = music[Random.Range(0, music.Length)];
        musicPlayer.PlayOneShot(levelMusic);
    }
    private void Update()
    {
        if (!musicPlayer.isPlaying)
        {
            musicPlayer.PlayOneShot(levelMusic);
        }
    }
    public void WalkingSound()=> soundFx.PlayOneShot(walkingSound);
    public void JumpingSound()
    {
        if (!soundFx.isPlaying)
        {
            soundFx.PlayOneShot(jumpingSound);
        }
    }
    public void PickUpSound()
    {
        soundFx.PlayOneShot(pickUpSound);
    }


}
