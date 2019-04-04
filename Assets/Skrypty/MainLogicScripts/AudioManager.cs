using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    // SFX
    public AudioSource Jump;
    public AudioSource Coin;
    public AudioSource Heart;
    public AudioSource Pause;
    public AudioSource Teleport;
    public AudioSource Checkpoint;
    public AudioSource Death;
    public AudioSource Laser;
    public AudioSource SmallExplosion;
    public AudioSource Alarm;
    public AudioSource BossHit;
    public AudioSource UfoSpawn;

    // Muzyka
    public AudioSource MenuMusic;
    public AudioSource Track1;
    public AudioSource Track2;
    public AudioSource Track3;
    public AudioSource Track4;

    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	public void PlayJump()
    {
        Jump.Play();
    }

    public void PlayCoin()
    {
        Coin.Play();
    }

    public void PlayHeart()
    {
        Heart.Play();
    }

    public void PlayPause()
    {
        Pause.Play();
    }

    public void PlayTeleport()
    {
        Teleport.Play();
    }

    public void PlayCheckpoint()
    {
        Checkpoint.Play();
    }

    public void PlayDeath()
    {
        Death.Play();
    }

    public void PlayLaser()
    {
        Laser.Play();
    }

    public void PlaySmallExplosion()
    {
        SmallExplosion.Play();
    }

    public void PlayAlarm()
    {
        Alarm.Play();
    }

    public void PlayBossHit()
    {
        BossHit.Play();
    }

    public void PlayUfoSpawn()
    {
        UfoSpawn.Play();
    }

    public IEnumerator FadeOut(AudioSource audio, float FadeTime, int level)
    {
        float startVolume = audio.volume;

        while(audio.volume > 0)
        {
            audio.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audio.Stop();
        audio.volume = startVolume;

        switch (level)
        {
            case 0:
                MenuMusic.Play();
                break;
            case 1:
                Track1.Play();
                break;
            case 2:
                Track2.Play();
                break;
            case 3:
                Track3.Play();
                break;
            case 4:
                Track4.Play();
                break;
        }
    }
}
