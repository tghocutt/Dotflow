using UnityEngine;
using System.Collections;

public class NEWExplosionAnimation : MonoBehaviour 
{

	public Animator animator;

	void Start () 
	{
		animator.speed = 1.8f;
		StartCoroutine(WaitThenDoThings());
	}


	IEnumerator WaitThenDoThings()
	{
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
}
