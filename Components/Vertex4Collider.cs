using Microsoft.Xna.Framework;
using Zen.Components;
using Zen.Util;

namespace Zen
{
    public class Vertex4Collider : Component
    {
        bool _boundsInitialized;
        public Vertex4 Vertices;

        public Vertex4Collider(Vertex4 vertices)
        {
            Vertices = vertices;
            _boundsInitialized = true;
        }

        public Vertex4Collider() { }

        public override void Mount()
        {
            if (!_boundsInitialized)
            {
                var renderer = GetComponent<SpriteRenderer>();
                System.Diagnostics.Debug.Assert(renderer != null, "No renderer found for default Collider");
                Vertices = renderer.Vertices;
                _boundsInitialized = true;
            }

            //Physics.RegisterCollider(this);
        }
    }
}