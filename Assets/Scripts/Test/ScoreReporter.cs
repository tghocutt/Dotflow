using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class ScoreReporter : MonoBehaviour 
	{
		public UILabel scoreReadout;
		public DotManager dotManager;

		public void Start()
		{
			scoreReadout.text = dotManager.mostRecentScore.ToString();
		}
	}
}
