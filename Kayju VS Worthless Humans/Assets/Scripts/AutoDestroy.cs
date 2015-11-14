using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {
	public bool parent;

	void OnBecameInvisible() {
		Destroy(parent ? transform.parent.gameObject : gameObject);
	}
}
