using System.Collections.Generic;

namespace Zen
{
    public static class Physics
    {
        static SpatialHash _spatialHash;

        public static void Init(int spatialHashCellSize)
        {
            _spatialHash = new SpatialHash(spatialHashCellSize);
        }

        public static void Add(Collider collider) => _spatialHash.Add(collider);

        public static void RemoveWithinBounds(Collider collider) => _spatialHash.RemoveWithinBounds(collider);

        public static void RemoveBruteForce(Collider collider) => _spatialHash.RemoveBruteForce(collider);

        public static bool BroadphaseCast(Collider collider, out HashSet<Collider> collisions, int collisionLayer = 0) => _spatialHash.BoxCast(collider.BroadphaseBounds, collider, out collisions, collisionLayer);
    }
}