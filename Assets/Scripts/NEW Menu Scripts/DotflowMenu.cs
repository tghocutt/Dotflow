using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class DotflowMenu : MonoBehaviour {

		public bool isGameInProgress = false;
		//this is the base open function which tells elements to
		//enter the screen. it must be implemented in all classes
		//which inheret from DotflowMenu.
		public virtual void Open(DotflowElement[] elements)
		{
			StopAllCoroutines ();
			foreach (DotflowElement element in elements) 
			{
				StartCoroutine(EnterScreen(element));
			}
		}

		//this is the base close function which tells elements to
		//exit the screen. it must be implemented in all classes
		//which inheret from DotflowMenu.
		public virtual void Close(DotflowElement[] elements)
		{
			foreach (DotflowElement element in elements) 
			{
				StartCoroutine(ExitScreen(element));
			}
		}

		//This coroutine takes a single dotflow element which has a
		//pre defined "start position" and "on screen position"
		//it will lerp from the start to the finish in order to bring
		//the elements from off screen to on screen as if they were
		//blown in from the side.
		private IEnumerator EnterScreen(DotflowElement element)
		{
			element.amIMoving = true;
			element.transform.position = element.startScreenPosition;
			element.gameObject.SetActive (true);

			//generates a random number to determine the speed of the element
			float randy = Random.Range (element.speedMin, element.speedMax);
			float timeElapsed = 0f;

			while(timeElapsed <= element.desiredTime || element.gameObject.transform.localPosition != element.onScreenPosition)
			{
				element.gameObject.SetActive (true);
				element.transform.localPosition = Vector3.Lerp(element.startScreenPosition, element.onScreenPosition, randy * timeElapsed);
				timeElapsed += Time.deltaTime;
				if(element.gameObject.transform.localPosition == element.onScreenPosition)  break;
				
				yield return new WaitForEndOfFrame();
			}
			element.amIMoving = false;
		}

		//this coroutine takes elements at whatever their current
		//location is and moves them to their pre-defined off screen
		//position. note that it does not disable those elements, it jsut
		//moves them off screen.
		private IEnumerator ExitScreen(DotflowElement element)
		{
			element.amIMoving = false;
			Vector3 currentPos = element.gameObject.transform.localPosition;

			//generates a random number to determine the speed of the element
			float randy = Random.Range (element.speedMin, element.speedMax);

			float timeElapsed = 0f;
			
			while(timeElapsed <= element.desiredTime)
			{
				element.transform.localPosition = Vector3.Lerp(currentPos, element.offScreenPosition, randy * timeElapsed);
				timeElapsed += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
		}
	}
}