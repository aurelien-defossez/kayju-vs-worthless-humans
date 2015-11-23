using UnityEngine;

public class RestartButton : ButtonEffect {
	public override void doEffect() {
		Application.LoadLevel("game");
	}
}
