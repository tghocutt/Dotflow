using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class DotflowMenu : MonoBehaviour {
		


		public virtual void Open(DotflowElement[] elements)
		{
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
			element.transform.position = element.startScreenPosition;

			float randy = Random.Range (element.speedMin, element.speedMax);
			float timeElapsed = 0f;

			while(timeElapsed <= element.desiredTime)
			{
				element.transform.localPosition = Vector3.Lerp(element.startScreenPosition, element.onScreenPosition, randy * Time.deltaTime);
				timeElapsed += Time.deltaTime;
				yield return null;
			}
		}


		private IEnumerator ExitScreen(DotflowElement element)
		{
			float randy = Random.Range (element.speedMin, element.speedMax);

			float timeElapsed = 0f;
			
			while(timeElapsed <= element.desiredTime)
			{
				element.transform.localPosition = Vector3.Lerp(element.onScreenPosition, element.offScreenPosition, randy * timeElapsed);
				timeElapsed += Time.deltaTime;
				yield return null;
			}
		}
	}
}