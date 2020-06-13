using System;
using Microsoft.Xna.Framework;
using Zen.Components;
using Zen.Util;

namespace Zen
{
    [Flags]
    public enum CollisionLayer
    {
        Jellyfish = 1,
        Player = 2
    }

    public abstract class Collider : Component
    {
        public event Action<Collider> OnCollide;
        public void CollideWith(Collider collider) => OnCollide?.Invoke(collider);
        public CollisionLayer CollisionLayer;
        public abstract RectangleF BroadphaseBounds { get; }
        public RectangleF RegisteredBroadphaseBounds;
    }
}