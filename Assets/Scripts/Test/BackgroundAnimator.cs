using UnityEngine;
using System.Collections;

public class BackgroundAnimator : MonoBehaviour {


	public float maxSize;
	public float minSize;

	public Color[] colors = new Color[0];

	private float desiredTime = 6f;
	private float timeElapsed = 0f;
	private int randy = 0;
	private Color c;

	private IEnumerator Grow()
	{
		while (gameObject.transform.localScale.x < maxSize) 
		{
			gameObject.transform.localScale *= (1 + (Time.deltaTime / 16));
			yield return new WaitForEndOfFrame();
		}

		StartCoroutine (Shrink ());
	}


	private void ShiftColor()
	{
		if (timeElapsed >= desiredTime) 
		{
			c = gameObject.renderer.material.color;
			timeElapsed = 0;
			randy = Mathf.RoundToInt (Random.Range (0, colors.Length));
		}

		gameObject.renderer.material.color = Color.Lerp (c, colors [randy], (timeElapsed/desiredTime));
		timeElapsed += Time.deltaTime;
	}

	private void Update()
	{
		ShiftColor ();
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
		c = gameObject.renderer.material.color;
		ShiftColor ();
		StartCoroutine (Grow ());
	}

}
