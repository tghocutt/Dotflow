using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour {

	public GameObject dotManager;

	public float timePowerUp = 0f;
	public bool slowTimeIsRunning = false;
	public AudioManager audioManager;

	public void SlowTimeGo(float f)
	{
		StartCoroutine (SlowTime (f));
	}

	public IEnumerator SlowTime(float f)
	{
		slowTimeIsRunning = true;
		timePowerUp += 5f;
		Time.timeScale = f;
		
		while (timePowerUp >= 0f)
		{
			timePowerUp -= Time.deltaTime;
			Debug.Log (audioManager.soundFX[5].clip.length + " : " + timePowerUp);
			yield return null;
		}
		
		Time.timeScale = 1f;
		slowTimeIsRunning = false;
	}

	private void Update()
	{
		if (slowTimeIsRunning && audioManager.soundFX [5].clip.length >= timePowerUp && audioManager.soundFX [5].isPlaying == false) 
		{
			audioManager.soundFX [5].Play ();
			Debug.Log("im playing now");
		}
		if(audioManager.soundFX[5].time < timePowerUp) audioManager.soundFX[5].Stop();
	}
}
