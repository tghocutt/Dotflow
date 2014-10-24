using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class ParticleRendererRotater : MonoBehaviour {

		public ParticleSystem particleSystem;

		private Rigidbody2D rigidBody2D;
		
		private void Update()
		{
			Vector3 directionOfMotion = new Vector3 (rigidBody2D.velocity.x, rigidBody2D.velocity.y, 0);
			Quaternion rotation = Quaternion.LookRotation(directionOfMotion);


			particleSystem.startRotation = rotation.eulerAngles.z;
		}
		
		private void Start()
		{
			rigidBody2D = transform.parent.rigidbody2D;
		}
	}
}
