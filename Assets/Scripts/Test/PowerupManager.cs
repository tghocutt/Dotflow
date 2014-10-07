using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour {

	public float timePowerUp = 0f;
	public bool slowTimeIsRunning = false;


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
			yield return null;
		}
		
		Time.timeScale = 1f;
		slowTimeIsRunning = false;
	}
}
