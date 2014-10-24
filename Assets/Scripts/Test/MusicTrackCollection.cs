using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class MusicTrackCollection : MonoBehaviour 
	{
		public AudioClip intro;
		public AudioClip middle;
		public AudioClip outro;

		public float trackCollectionLength;

		public AudioSource song;

		public void PlayTracks()
		{
			StartCoroutine (PlayAll());
		}


		private IEnumerator PlayAll()
		{
			song.clip = intro;
			song.Play ();
			yield return new WaitForSeconds (intro.length);
			song.clip = middle;
			song.Play ();
			yield return new WaitForSeconds (middle.length);
			song.clip = outro;
			song.Play ();
		}


		private void Start()
		{
			trackCollectionLength = intro.length + middle.length + outro.length;
		}
	}
}
