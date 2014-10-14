using UnityEngine;
using System.Collections;

public class TrackPicker : MonoBehaviour {

	public GameObject songButton1;
	public GameObject songButton2;
	public GameObject songButton3;
	public GameObject songButton4;
	public AudioManager audioManager;

	public void Track1(GameObject go)
	{
		foreach (AudioSource audio in audioManager.music) 
		{
			audio.Stop();
		}

		audioManager.music [0].Play ();
	}


	public void Track2(GameObject go)
	{
		foreach (AudioSource audio in audioManager.music) 
		{
			audio.Stop();
		}
		
		audioManager.music [1].Play ();
	}


	public void Track3(GameObject go)
	{
		foreach (AudioSource audio in audioManager.music) 
		{
			audio.Stop();
		}
		
		audioManager.music [2].Play ();
	}


	public void Track4(GameObject go)
	{
		foreach (AudioSource audio in audioManager.music) 
		{
			audio.Stop();
		}
		
		audioManager.music [3].Play ();
	}


	private void Start()
	{
		UIEventListener.Get (songButton1).onClick += Track1;
		UIEventListener.Get (songButton2).onClick += Track2;
		UIEventListener.Get (songButton3).onClick += Track3;
		UIEventListener.Get (songButton4).onClick += Track4;
	}
}
