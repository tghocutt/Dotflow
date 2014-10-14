using UnityEngine;
using System.Collections;

public class Complement : MonoBehaviour {

	public UILabel self;
	public float desiredTime;


	private float elapsedTime = 0f;


	// Use this for initialization
	void Start () {
		StartCoroutine (Wait ());
	}


	private IEnumerator Wait()
	{
		yield return new WaitForSeconds (desiredTime);
		StartCoroutine (Fade ());
	}


	private IEnumerator Fade()
	{
		while(elapsedTime < desiredTime)
		{
			self.alpha -= elapsedTime/desiredTime;

			elapsedTime += Time.deltaTime;

			yield return null;
		}
	}
	

}
