using UnityEngine;

public static class Vector3Extension {
	public static Vector3 X(this Vector3 v, float x) {
		v.x = x;
		return v;
	}

	public static Vector3 Y(this Vector3 v, float y) {
		v.y = y;
		return v;
	}

	public static Vector3 Z(this Vector3 v, float z) {
		v.z = z;
		return v;
	}

	public static float GetRotation(this Vector3 v) {
		float angle = Vector3.Angle(Vector3.forward, v);

		if (v.x < 0) {
			angle = 360 - angle;
		}

		return angle;
	}

	public static string Print(this Vector3 v) {
		return v.ToString("G4");
	}
}