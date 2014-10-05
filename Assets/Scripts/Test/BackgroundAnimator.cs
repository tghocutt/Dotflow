using UnityEngine;
using System.Collections;

public class BackgroundAnimator : MonoBehaviour {


	public float maxSize;
	public float minSize;

	public Color[] colors = new Color[0];


	private IEnumerator Grow()
	{
		while (gameObject.transform.localScale.x < maxSize) 
		{
			gameObject.transform.localScale *= (1 + (Time.deltaTime / 16));
			yield return new WaitForEndOfFrame();
		}

		StartCoroutine (Shrink ());
	}


	private IEnumerator ShiftColor()
	{
		for(int i = 0; i < (colors.Length - 1); i++)
		{
			float num = 0f;
			while(num < 1f)
			{
				gameObject.renderer.material.color = Color.Lerp(colors[i], colors[i + 1], num);
				num += 0.1f;
			}
		}
		yield return null;
		StartCoroutine(ShiftColor());
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
		StartCoroutine (ShiftColor ());
		StartCoroutine (Grow ());
	}

}
