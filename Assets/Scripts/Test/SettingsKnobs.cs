using UnityEngine;
using System.Collections;

namespace Dotflow{
	public class SettingsKnobs : MonoBehaviour {

		public DotManager dotManager;

		public UIInput currentSpeedInput;
		public UIInput speedThresholdInput;
		public UIInput speedBoostInput;
		public UIInput maxDotsInput;
		public UIInput shrinkAmountInput;

		public UIInput[] inputs = new UIInput[0];
		private int[] intsLastFrame = new int[0]; 
		private int[] intsThisFrame = new int[0];

		private void Initialize()
		{
			currentSpeedInput.label.text = dotManager.dotCurrentSpeed.ToString ();
			speedThresholdInput.label.text = dotManager.speedDifficultyThreshold.ToString ();
			speedBoostInput.label.text = dotManager.dotSpeedBoostAmount.ToString ();
			maxDotsInput.label.text = dotManager.currentMaxDots.ToString ();
			shrinkAmountInput.label.text = (dotManager.dotShrinkAmount * 100f) + "";

			int[] temp = new int[inputs.Length];
			for (int i = 0; i < inputs.Length; i++) 
			{
				temp[i] = int.Parse(inputs[i].label.text.ToString());
			}
			
			intsLastFrame = temp;
			intsThisFrame = temp;
		}


		public void SetValues()
		{
			dotManager.dotCurrentSpeed = int.Parse (currentSpeedInput.label.text);
			dotManager.speedDifficultyThreshold = int.Parse (speedThresholdInput.label.text);
			dotManager.dotSpeedBoostAmount = int.Parse (speedBoostInput.label.text);
			dotManager.currentMaxDots = int.Parse (maxDotsInput.label.text);
			dotManager.dotShrinkAmount = (float.Parse (shrinkAmountInput.label.text)) / 100f;


		}


		private void Start()
		{
			Initialize ();
		}
	}
}
