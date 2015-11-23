using UnityEngine;

public class QuitButton : ButtonEffect {
	public override void doEffect() {
		Application.Quit();
	}
}
