using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class UpperButtonBehavior : MonoBehaviour 
	{
		public BoosterSelectMenuScript boosterScript;
		public UIButton button;
		public int cost;

		private void GoAway(GameObject go)
		{
			PlayerPrefs.SetInt ("gemTotal", PlayerPrefs.GetInt ("gemTotal") + cost);
			boosterScript.gemLabel.text = PlayerPrefs.GetInt ("gemTotal").ToString ();
			boosterScript.currentBoostersSelected -= 1;
			Destroy (gameObject);
		}


		private void Start()
		{
			UIEventListener.Get (button.gameObject).onClick += GoAway;
		}
	}
}