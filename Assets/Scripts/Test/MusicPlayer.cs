using UnityEngine;
using System.Collections;


namespace Dotflow
{
	public class MusicPlayer : MonoBehaviour 
	{
		public AudioClip[] musicTracks = new AudioClip[0];
		public AudioSource audioSource;

		public bool shuffle;

		private int trackIndex;


		private void ModuloAdd()
		{
			if (trackIndex == musicTracks.Length - 1) 
			{
				trackIndex = 0;
			} else {
				trackIndex ++;
			}
		}


		private void Update()
		{
			if (!audioSource.isPlaying) 
			{
				audioSource.clip = musicTracks[trackIndex];
				audioSource.Play();
				if (shuffle) 
				{
					trackIndex = Mathf.RoundToInt(Random.Range(0, musicTracks.Length));
				} else {
					ModuloAdd();
				}
			}
		}
	}
}
