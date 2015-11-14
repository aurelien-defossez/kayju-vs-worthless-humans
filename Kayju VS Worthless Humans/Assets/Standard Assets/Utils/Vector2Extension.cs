using UnityEngine;

public static class Vector2Extension {
    public static Vector2 X(this Vector2 v, float x) {
        v.x = x;
        return v;
    }

    public static Vector2 Y(this Vector2 v, float y) {
        v.y = y;
        return v;
    }

    public static Vector2 Rotate(this Vector2 v, float degrees) {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);

        float tx = v.x;
        float ty = v.y;

        return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
    }

    public static float GetRotation(this Vector2 v) {
        float angle = Vector2.Angle(Vector2.up, v);

        if (v.x < 0) {
            angle = 360 - angle;
        }

        return angle;
    }

    public static string Print(this Vector2 v) {
        return v.ToString("G4");
    }
}