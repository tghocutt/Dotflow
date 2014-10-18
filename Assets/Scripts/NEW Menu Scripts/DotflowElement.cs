using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class DotflowElement : MonoBehaviour {

		public Vector3 startScreenPosition;
		public Vector3 onScreenPosition;
		public Vector3 offScreenPosition;
		public float speedMin = 1;
		public float speedMax = 5;
		public float desiredTime = 3;
	}
}
