using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class DotflowMenu : MonoBehaviour {


		public virtual void Open(DotflowElement[] elements)
		{
			StopAllCoroutines ();
			foreach (DotflowElement element in elements) 
			{
				StartCoroutine(EnterScreen(element));
			}
		}


		public virtual void Close(DotflowElement[] elements)
		{
			foreach (DotflowElement element in elements) 
			{
				StartCoroutine(ExitScreen(element));
			}
		}


		private IEnumerator EnterScreen(DotflowElement element)
		{
			element.amIMoving = true;
			element.transform.position = element.startScreenPosition;
			element.gameObject.SetActive (true);

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


		private IEnumerator ExitScreen(DotflowElement element)
		{
			element.amIMoving = false;
			Vector3 currentPos = element.gameObject.transform.localPosition;
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