using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public static AudioClip moveSound, attackSound, buttonClick, gameWon, gameLost, wrong, correct, finish2, click, clock, b1, b2, b3,finishwin,finishlose;
    static AudioSource audioSrc;
    public Slider volumeSlider;
    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        /*moveSound = Resources.Load<AudioClip>("moveS");
		attackSound = Resources.Load<AudioClip>("attackS");
		gameWon = Resources.Load<AudioClip>("gameWon");
		gameLost = Resources.Load<AudioClip>("gameLost");*/
        //buttonClick = Resources.Load<AudioClip>("mouseclick");
        buttonClick = Resources.Load<AudioClip>("buttonClick");
        wrong = Resources.Load<AudioClip>("wrong");
        correct = Resources.Load<AudioClip>("correct");
        finish2 = Resources.Load<AudioClip>("levelwin");
        finishwin = Resources.Load<AudioClip>("finishwin");
        finishlose = Resources.Load<AudioClip>("finishlose");
        click = Resources.Load<AudioClip>("click");
        clock = Resources.Load<AudioClip>("clock");
        b1 = Resources.Load<AudioClip>("bubble1");
        b2 = Resources.Load<AudioClip>("bubble2");
        b3 = Resources.Load<AudioClip>("bubble3");
        if (PlayerPrefs.HasKey("soundVolume"))
        {
            LoadVolume();
        }
        else
        {
            PlayerPrefs.SetFloat("soundVolume", 1);
            LoadVolume();
        }
    }

    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveVolume();
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("soundVolume",volumeSlider.value);
    }
    public void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("soundVolume");
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "buttonClick":
                audioSrc.PlayOneShot(buttonClick, 0.92f);
                break;
            case "wrong":
                audioSrc.PlayOneShot(wrong);
                break;
            case "correct":
                audioSrc.PlayOneShot(correct);
                break;
            case "finish2":
                audioSrc.PlayOneShot(finish2, 0.35f);
                break;
            case "finishwin":
                audioSrc.PlayOneShot(finishwin, 0.35f);
                break;
            case "finishlose":
                audioSrc.PlayOneShot(finishlose, 0.35f);
                break;
            case "click":
                audioSrc.PlayOneShot(click);
                break;
            case "clock":
                audioSrc.PlayOneShot(clock);
                break;
            case "b1":
                audioSrc.PlayOneShot(b1);
                break;
            case "b2":
                audioSrc.PlayOneShot(b2);
                break;
            case "b3":
                audioSrc.PlayOneShot(b3);
                break;
        }
    }
    public static void StopSound()
    {
        audioSrc.Stop();
    }
}
