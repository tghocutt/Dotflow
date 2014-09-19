﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dotflow
{
	public class DotManager : MonoBehaviour {

		public GUIManager guiManager;
		public int maxLives;

		public int maxDots; /* maximum amount of dots allowed on screen, works as a difficulty adjust */
		public int dotCount; /* current ammount of dots on screen */

		public float dotMaxSpeed; /* maximum speed a dot can achieve */
		public float dotSpeedBoostAmount; /* speed boost to dots's max speed, to adjust the difficulty */
		public float dotShrinkAmount; /* how much the dots shrink, to adjust the difficulty */

		public LineManager lineManager; /* lineManager is the game object that holds the line renderer, and has the script that keeps the collision boxes up to date */
		public LineRenderer lineRenderer; /* the Renderer component for the lines */

		public GameObject dotContainer; /* the parent for all the dots, a simple container for them */
		public GameObject[] dotPrefabs; /* array of all dot prefabs */
		public Main main; /* instance of the Main script (you can find it on the scripts folder), not used anywhere yet */
		public AudioManager audioManager; /* the object that holds all the audio and plays it */

		public List<Dot> allDots = new List<Dot>(); /* list containing all dot objects that exist */
		public List<Dot> dotsInLine = new List<Dot> (); /* list containing all dot objects currently forming the line, in order of connection */
		public List<Transform> listOfLineVertices = new List<Transform>(); /* list that holds all the vertices's positions */

		public bool lineBeingDrawn = false; /* boolean value that tells if the line is being currently drawn or not */

		private float spawnSize = 1.0f; /* starting size for the dots, in terms of unity scale */
		private string lineColor = ""; /* the current color of the line, with "" meaning no color, or no line at all */
		private int livesCount = 0; /* tracks the number of accidental line collisions the player has made */

		//spawn dot instantiates a new random dot from prefab array(manuall assignment)
		public void SpawnDot()
		{
			//picks random location
			Vector2 spawnPos = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));

			//picks random prefab
			int randy = Mathf.RoundToInt(Random.Range (0, dotPrefabs.Length));
			GameObject dotObject = Instantiate(dotPrefabs[randy]) as GameObject;

			//sets parent and scale
			dotObject.transform.parent = dotContainer.transform;
			dotObject.transform.localScale = new Vector3 (spawnSize, spawnSize, spawnSize);

			//sets random speed vector based on max speed
			float randyX = Random.Range(0, dotMaxSpeed);
			float randyY = dotMaxSpeed - randyX;

			//applies all those parameters to the new dot
			Vector2 randomVector = new Vector2(randyX, randyY);
			dotObject.transform.localPosition = spawnPos;
			dotObject.rigidbody2D.AddForce(randomVector);
			dotObject.GetComponent<Dot>().dotManager = this;

			dotCount += 1;
		}


		//increases the difficulty of the game
		public void IncreaseDifficulty()
		{

			//increases game complexity
			maxDots++;
			spawnSize = spawnSize/(1f + dotShrinkAmount);
			dotMaxSpeed = dotMaxSpeed * ((1f + dotSpeedBoostAmount));
			foreach(Dot d in allDots)
			{
				if(d != null) d.gameObject.transform.localScale = new Vector3(spawnSize, spawnSize, spawnSize);
			}
		}
	

		//takes a list of dots, destroys and removes them from the dotlist, 
		private void DestroyDots(List<Dot> ds)
		{
			//loops through dots in line, destroys them, removes them from dotlist
			foreach(Dot d in ds)
			{
				for(int i = 0; i < allDots.Count; i++)
				{
					if(allDots[i] == d)
					{
						Destroy(allDots[i].gameObject);
						allDots.RemoveAt(i);
						dotCount -= 1;
						audioManager.Pop();
					}
				}
			}
			ClearLine ();

			IncreaseDifficulty ();
		}


		void ClearLine(){
			listOfLineVertices.Clear();
			dotsInLine.Clear();
			lineColor = "";
		}

		//draw line draws a line between all of the dots in the line, using the dot locations as vertices
		private IEnumerator DrawLine()
		{
			lineRenderer.SetVertexCount(listOfLineVertices.Count + 1);

			//gets mouse position to draw the raycast
			Vector3 raycastMousePos = Input.mousePosition;
			raycastMousePos.x = Mathf.Clamp01(raycastMousePos.x / Screen.width);
			raycastMousePos.y = Mathf.Clamp01(raycastMousePos.y / Screen.height);
			raycastMousePos.z = 0f;
			
			//gets mouse position to draw the line
			Vector3 linedrawMousePos = new Vector3();
			linedrawMousePos.x = raycastMousePos.x;
			linedrawMousePos.y = raycastMousePos.y;
			linedrawMousePos.z = 1.0f;

			//sets line vertex locations
			for(int i = 0; i < listOfLineVertices.Count; i++)
			{
				listOfLineVertices[i].position = new Vector3(listOfLineVertices[i].position.x, listOfLineVertices[i].position.y, 1.0f);
				lineRenderer.SetPosition(i, listOfLineVertices[i].position);
			}

			//draws the line
			lineRenderer.SetPosition(listOfLineVertices.Count, Camera.main.ViewportToWorldPoint(linedrawMousePos));
			if(dotsInLine.Count > 0) 
				lineRenderer.renderer.material.color = dotsInLine[0].GetComponentInChildren<UIButton>().defaultColor;

			//adds new dot to the end of the line
			if (lineBeingDrawn) 
			{
				RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ViewportToWorldPoint(raycastMousePos), Vector3.forward, 300f);
				//Debug.DrawRay(Camera.main.ViewportToWorldPoint(raycastMousePos), Vector3.forward,Color.red,60.0f,true);

				foreach(RaycastHit2D h in hits)
				{
					if(h.collider.gameObject.layer == 9)
					{
						if(!dotsInLine.Contains(h.collider.GetComponentInParent<Dot>()))
						{
							if (lineColor == "" || lineColor == h.collider.GetComponentInParent<Dot>().color)
							{
								Dot newDot = h.collider.GetComponentInParent<Dot>();
								lineColor = h.collider.GetComponentInParent<Dot>().color;

								dotsInLine.Add(newDot);
								listOfLineVertices.Add(newDot.transform);
								if(dotsInLine.Count < audioManager.dotsConnecting.Length)
								{
									audioManager.dotsConnecting[dotsInLine.Count - 1].Play();
								} else {
									audioManager.dotsConnecting[audioManager.dotsConnecting.Length - 1].Play();
								}
							}
						}
					}
					
				}
			}
			lineManager.updateColliders (listOfLineVertices);

			yield return null;
		}


		//detects input, and tracks dot count, and starts coroutine
		private void Update()
		{
			if(Input.GetMouseButtonDown(0))
			{
				lineBeingDrawn = true;
				audioManager.soundFX[1].Play();
			}

			if(Input.GetMouseButtonUp(0))
			{
				DestroyDots(dotsInLine);
				lineBeingDrawn = false;
				audioManager.soundFX[1].Stop();
			}

			if(dotCount < maxDots)
			{
				SpawnDot();
			}

			StartCoroutine (DrawLine ());
			lineManager.updateColliders (listOfLineVertices);
		}

		public void CollisionWithLine (Dot collidedDot)
		{

			if (dotsInLine.Count > 0 && collidedDot.color != lineColor){

				if(livesCount == maxLives)
				{
					audioManager.soundFX[2].Play();
					Time.timeScale = 0.0f;
					guiManager.deathMenuRoot.SetActive(true);
				} else {
					guiManager.lives.lives[livesCount].SetActive(true);
					audioManager.soundFX[2].Play();
					ClearLine();
					lineBeingDrawn = false;
					livesCount ++;
				}
			}
		}

		//adds listener to UI elements
		private void Start()
		{
			//lineRenderer = lineManager.GetLineRenderer();
		}
	}
}
