using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	
	//functions largely as a data class holding all of the
	//audio sources.

	public AudioSource[] soundFX = new AudioSource[0];
	public AudioSource[] dotsConnecting = new AudioSource[0];
	public AudioSource[] music = new AudioSource[0];
	public AudioSource[] menuFX = new AudioSource[0];

	//play the pop noise
	public void Pop()
	{
		soundFX[0].Play();
	}

	//play the button click noise
	public void ButtonClick()
	{
		menuFX [0].Play ();
	}


	private void Start()
	{
		//music[0].Play();
	}
}
