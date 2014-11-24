using UnityEngine;
using System.Collections;

public class character_movement : MonoBehaviour {
	
	//Player
	private int jumpHeight = 500;
	//public bool isGrounded = false; //this can be seen working in the Unity inspector
    //public Transform groundedEnd; //declares the empty game object in Unity 
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Movement ();
	}
	
	void Movement() 
	{
		if(Input.GetKey (KeyCode.D))
		{
			transform.Translate(Vector2.right * 4f * Time.deltaTime);
			//transform.eulerAngles = new Vector2(0, 0);
		}
		
		if(Input.GetKey (KeyCode.A))
		{
			transform.Translate(-Vector2.right * 4f * Time.deltaTime);
			//transform.eulerAngles = new Vector2(0, 180);
		}
		
		if(Input.GetKey(KeyCode.W))
		{
			Jump();
		}
	}
	
	void Jump()
	{
		rigidbody2D.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Force);
	}
}