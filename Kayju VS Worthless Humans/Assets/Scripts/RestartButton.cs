using UnityEngine;
using System.Collections;

public class RestartButton : ButtonEffect {
    AudioSource source;

	public override void doEffect() {
        source = PlaySound("Restart");
        StartCoroutine(WaitBeforeRestart());
	}

    IEnumerator WaitBeforeRestart() {
        while (source.isPlaying) {
            yield return null;
        }

        Application.LoadLevel("game");
    }
}
