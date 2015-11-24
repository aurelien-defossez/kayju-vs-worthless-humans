using UnityEngine;
using System.Collections.Generic;

public class HumanScores : MonoBehaviour {
	public float interval;
	public HumanScore humanScorePrefab;

	private int scoresCount = 0;

	public HumanScore Register(Color color) {
		HumanScore score = Instantiate(humanScorePrefab);
		score.transform.SetParent(transform);
		score.transform.localPosition = new Vector3(0, interval * scoresCount++, 0);
		score.Init(color);
		return score;
	}
}
