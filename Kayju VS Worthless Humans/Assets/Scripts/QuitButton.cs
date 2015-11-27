using UnityEngine;
using System.Collections;

public class QuitButton : ButtonEffect {
    AudioSource source;

	public override void doEffect() {
        source = PlaySound("Quit");

        StartCoroutine(WaitBeforeQuit());
	}

    IEnumerator WaitBeforeQuit() {
        while (source.isPlaying) {
            yield return null;
        }

        Application.Quit();
    }
}
