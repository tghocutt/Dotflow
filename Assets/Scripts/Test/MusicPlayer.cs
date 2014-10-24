using UnityEngine;
using System.Collections;


namespace Dotflow
{
	public class MusicPlayer : MonoBehaviour 
	{
		public MusicTrackCollection[] musicTracks = new MusicTrackCollection[0];

		private void Start()
		{
			musicTracks[0].PlayTracks();
		}
	}
}
