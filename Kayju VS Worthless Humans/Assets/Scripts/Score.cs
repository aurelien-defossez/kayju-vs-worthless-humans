using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class Score : MonoBehaviour {
	public Text text;

	public void Init(Color color) {
		text.color = color;
		transform.localScale = Vector3.one;
		SetScore(0);
	}

	public void SetScore(int score) {
		text.text = score.ToString("N0", CultureInfo.CreateSpecificCulture("fr-FR"));
	}

	public void SetScore(float value) {
		SetScore((int)value);
	}
}
