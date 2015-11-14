using UnityEngine;

public static class ColorExtension {
    public static Color A(this Color c, float a) {
        c.a = a;
        return c;
    }
}
