using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioSource[] soundFX = new AudioSource[0];
	public AudioSource[] dotsConnecting = new AudioSource[0];
	public AudioSource[] music = new AudioSource[0];
	public AudioSource[] menuFX = new AudioSource[0];

	public void Pop()
	{
		soundFX[0].Play();
	}

	public void ButtonClick()
	{
		menuFX [0].Play ();
	}


	private void Start()
	{
		music[0].Play();
	}
}
