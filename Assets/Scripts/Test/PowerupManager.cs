using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour {

	public GameObject dotManager;

	public float timePowerUp = 0f;
	public bool slowTimeIsRunning = false;
	public AudioManager audioManager;
	private float tracker = 0;

	private bool isPlaying = false;

	public void SlowTimeGo(float f)
	{
		StartCoroutine (SlowTime (f));
	}

	public IEnumerator SlowTime(float f)
	{
		tracker = f;
		slowTimeIsRunning = true;
		timePowerUp += 5f;
		Time.timeScale = f;
		
		while (timePowerUp >= 0f)
		{
			timePowerUp -= Time.deltaTime;
			yield return null;
		}
		
		Time.timeScale = 1f;
		isPlaying = false;
		slowTimeIsRunning = false;
	}

	private void Update()
	{
		if (slowTimeIsRunning && audioManager.soundFX [5].clip.length >= timePowerUp * 2.5f  && !isPlaying) 
		{
			audioManager.soundFX [5].Play ();
			Debug.Log("im playing now : " + audioManager.soundFX[5].isPlaying);
			isPlaying = true;
		}
		//if(audioManager.soundFX[5].time < timePowerUp) audioManager.soundFX[5].Stop();
	}
}
