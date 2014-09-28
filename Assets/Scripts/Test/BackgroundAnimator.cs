using UnityEngine;
using System.Collections;

public class BackgroundAnimator : MonoBehaviour {


	public float maxSize;
	public float minSize;


	private IEnumerator Grow()
	{
		while (gameObject.transform.localScale.x < maxSize) 
		{
			gameObject.transform.localScale *= (1 + (Time.deltaTime / 16));
			yield return new WaitForEndOfFrame();
		}

		StartCoroutine (Shrink ());
	}


	private IEnumerator Shrink()
	{
		while (gameObject.transform.localScale.x > minSize) 
		{
			gameObject.transform.localScale *= (1 - (Time.deltaTime / 16));
			yield return new WaitForEndOfFrame();
		}
		
		StartCoroutine (Grow ());
	}


	private void Start () 
	{
		StartCoroutine (Grow ());
	}

}
