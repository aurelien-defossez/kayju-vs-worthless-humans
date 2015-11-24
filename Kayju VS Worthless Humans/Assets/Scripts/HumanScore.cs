using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class HumanScore : MonoBehaviour {
	public Text text;

	private int score = 0;

	public void Init(Color color) {
		text.color = color;
		transform.localScale = Vector3.one;
		SetScore(0);
	}

	public void SetScore(int value) {
		score = value;
		text.text = score.ToString("N0", CultureInfo.CreateSpecificCulture("fr-FR"));
	}

	public void SetScore(float value) {
		SetScore((int)value);
	}
}
