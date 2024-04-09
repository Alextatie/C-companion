using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

	public static AudioClip moveSound, attackSound, buttonClick, gameWon, gameLost, wrong, correct;
	static AudioSource audioSrc;

	private void Start()
	{
		audioSrc = GetComponent<AudioSource>();
		/*moveSound = Resources.Load<AudioClip>("moveS");
		attackSound = Resources.Load<AudioClip>("attackS");
		gameWon = Resources.Load<AudioClip>("gameWon");
		gameLost = Resources.Load<AudioClip>("gameLost");*/
		buttonClick = Resources.Load<AudioClip>("buttonClick");
        wrong = Resources.Load<AudioClip>("wrong");
        correct = Resources.Load<AudioClip>("correct");
    }

	public static void PlaySound(string clip)
	{
		switch (clip)
		{
/*			case "moveSound":
				audioSrc.PlayOneShot(moveSound);
				break;
			case "attackSound":
				audioSrc.PlayOneShot(attackSound);
				break;
			case "gameWon":
				audioSrc.PlayOneShot(gameWon);
				break;
			case "gameLost":
				audioSrc.PlayOneShot(gameLost);
				break;*/
			case "buttonClick":
				audioSrc.PlayOneShot(buttonClick);
				break;
            case "wrong":
                audioSrc.PlayOneShot(wrong);
                break;
            case "correct":
                audioSrc.PlayOneShot(correct);
                break;
        }
	}
}
