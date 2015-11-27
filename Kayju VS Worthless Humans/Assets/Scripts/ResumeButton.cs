using UnityEngine;

public class ResumeButton : ButtonEffect {
	public GameObject pauseMenu;

	public override void doEffect() {
        PlaySound("Resume");
		pauseMenu.SetActive(false);
	}
}
