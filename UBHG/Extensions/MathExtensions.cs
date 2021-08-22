using UnityEngine;

namespace UBHG.Extensions {
    internal static class MathExtensions {
        public static Vector2 FromPolarDeg(float angle, float magnitude) {
            angle *= Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * magnitude;
        }

        public static float ToAngleDeg(this Vector2 direction) {
            var (x, y) = direction;
            return Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        }
    }
}