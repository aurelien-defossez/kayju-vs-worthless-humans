using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Utils : MonoBehaviour {
    // ----------------------------------------------------------------------------
    // Singleton
    // ----------------------------------------------------------------------------

    private static Utils instance;

    public void Awake() {
        instance = this;
    }

    // ----------------------------------------------------------------------------
    // Class methods
    // ----------------------------------------------------------------------------

    public static void StartExternalCoroutine(IEnumerator coroutine) {
        instance.StartCoroutine(coroutine);
    }

    private static float startTime;
    public static void tick() {
        startTime = Time.realtimeSinceStartup;
    }

    public static int tack() {
        return Mathf.RoundToInt((Time.realtimeSinceStartup - startTime) * 1000);
    }

    public static Type FindTypeInLoadedAssemblies(string typeName) {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
            Type type = assembly.GetType(typeName);

            if (type != null) {
                return type;
            }
        }

        return null;
    }

    public static Collider[] OverlapBounds(Bounds bounds) {
        Vector3 ext = bounds.extents;

        // Find all potential colliders using a sphere englobing our bounding box
        float radius = Mathf.Sqrt(ext.x * ext.x + ext.y * ext.y + ext.z * ext.z) * 0.5f;
        Collider[] sphereColliders = Physics.OverlapSphere(bounds.center, radius);

        // Check if colliders do collide with our bounding box
        Collider[] colliders = new Collider[sphereColliders.Length];
        int count = 0;
        for (int i = 0; i < sphereColliders.Length; i++) {
            if (BoundsCollide(bounds, sphereColliders[i].bounds)) {
                colliders[count++] = sphereColliders[i];
            }
        }

        // Trim the array
        Array.Resize(ref colliders, count);

        return colliders;
    }

    public static bool BoundsCollide(Bounds b1, Bounds b2) {
        Vector3 pos1 = b1.center;
        Vector3 ext1 = b1.extents;
        Vector3 pos2 = b2.center;
        Vector3 ext2 = b2.extents;

        return (pos2.x + ext2.x >= pos1.x - ext1.x
             && pos2.x - ext2.x <= pos1.x + ext1.x
             && pos2.y + ext2.y >= pos1.y - ext1.y
             && pos2.y - ext2.y <= pos1.y + ext1.y
             && pos2.z + ext2.z >= pos1.z - ext1.z
             && pos2.z - ext2.z <= pos1.z + ext1.z);
    }

    public static Transform[] FindChildrenByTag(GameObject parent, string tag) {
        return parent.transform.Cast<Transform>().Where(c => c.gameObject.tag == tag).ToArray();
    }

    public static void DestroyChildren(Transform t) {
        foreach (Transform child in t) {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static void SetLayerToChildren(GameObject go, int layer) {
        foreach (Transform child in go.GetComponentsInChildren<Transform>()) {
            child.gameObject.layer = layer;
        }
    }

    public static AudioSource PlayPitchedClipAt(AudioClip clip, GameObject gameObject, float pitch = 0.0f) {
        if (pitch == 0.0f) {
            pitch = UnityEngine.Random.Range(0.95f, 1.15f);
        }

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.pitch = pitch;
        audioSource.Play();

        GameObject.Destroy(audioSource, clip.length);

        return audioSource;
    }

    public static AudioSource PlayPitchedClipAt(AudioClip clip, Vector3 position, float pitch = 0.0f) {
        GameObject gameObject = new GameObject("TempAudio");
        gameObject.transform.position = position;

        AudioSource audioSource = PlayPitchedClipAt(clip, gameObject, pitch);

        GameObject.Destroy(gameObject, clip.length);

        return audioSource;
    }

	public static void FadeAudio(AudioSource source, float to, float fadeTime) {
		StartExternalCoroutine(FadeAudioCoroutine(source, to, fadeTime));
	}

	public static IEnumerator FadeAudioCoroutine(AudioSource source, float to, float fadeTime) {
		float from = source.volume;
		float time = 0.0F;

		while (time <= fadeTime) {
			time += Time.deltaTime;
			source.volume = Mathf.Lerp(from, to, time / fadeTime);
			yield return new WaitForEndOfFrame();
		}
	}

	public static AudioSource PlayPitchedClip(AudioClip clip) {
        return PlayPitchedClipAt(clip, Vector3.zero);
    }
}