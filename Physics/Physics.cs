namespace Zen
{
    public static class Physics
    {
        static SpatialHash _spatialHash;

        public static void Init(int spatialHashCellSize)
        {
            _spatialHash = new SpatialHash(spatialHashCellSize);
        }

        public static void RegisterCollider(Collider collider) => _spatialHash.Register(collider);

        public static void UnRegisterCollider(Collider collider) => _spatialHash.Remove(collider);
    }
}