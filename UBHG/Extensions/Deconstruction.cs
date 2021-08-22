using UnityEngine;

namespace UBHG.Extensions {
    internal static class Deconstruction {
        public static void Deconstruct(this Vector2 vector, out float x, out float y) => (x, y) = (vector.x, vector.y);
    }
}