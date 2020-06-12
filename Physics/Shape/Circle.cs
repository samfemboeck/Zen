using Microsoft.Xna.Framework;

namespace Zen
{
    public static class Circle
    {
        public static bool IntersectsCircle(Vector2 centerA, float radiusA, Vector2 centerB, float radiusB)
        {
            Vector2 direction = centerB - centerA;
            return direction.Length() - radiusB <= radiusA;
        }
    }
}