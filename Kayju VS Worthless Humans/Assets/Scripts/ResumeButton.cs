using UnityEngine;

public class ResumeButton : ButtonEffect {
	public GameObject pauseMenu;

	public override void doEffect() {
		pauseMenu.SetActive(false);
	}
}
