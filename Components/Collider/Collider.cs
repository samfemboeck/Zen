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
        public CollisionLayer CollisionLayer;
        public abstract RectangleF BroadphaseBounds { get; }
        public abstract bool Intersects(Collider other);
    }
}