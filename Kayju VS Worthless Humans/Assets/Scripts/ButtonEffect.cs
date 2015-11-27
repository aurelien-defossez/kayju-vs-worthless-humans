using UnityEngine;

public abstract class ButtonEffect : MonoBehaviour {
    public abstract void doEffect();

    public AudioSource PlaySound(string effect) {
        Debug.Log(GameObject.Find("Canvas"));
        Transform player = GameObject.Find("Canvas").transform.FindChild("Audio").FindChild(effect);
        AudioSource source = null;
        if (player != null)
            source = player.gameObject.GetComponent<AudioSource>();
        if (source != null) {
            source.Play();
        }

        return source;
    }
}
