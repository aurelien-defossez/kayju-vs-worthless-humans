using UnityEngine;
using UnityEngine.UI;

public class HumanScore : MonoBehaviour {
	public Text text;

	private int score = 0;

	public void Init(Color color) {
		text.color = color;
		transform.localScale = Vector3.one;
		SetScore(0);
	}

	public void Increment(int value) {
		SetScore(score + value);
	}

	public void SetScore(int value) {
		score = value;
		text.text = score.ToString();
	}
}
