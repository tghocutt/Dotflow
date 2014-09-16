using UnityEngine;
using System.Collections;

public class ExplosionAnimation : MonoBehaviour {

	public Material[] frames = new Material[0];

	private int index = 0;



	public IEnumerator Animate()
	{
		while(index < frames.Length)
		{
			gameObject.renderer.material = frames[index];
			yield return new WaitForSeconds(0.1f);
			index++;
		}

		Destroy (gameObject);
	}


	private void Start()
	{
		StartCoroutine(Animate());
	}
}
