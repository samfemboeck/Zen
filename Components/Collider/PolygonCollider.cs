using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Zen.Components;
using Zen.Util;

namespace Zen
{
    public class PolygonCollider : Collider, ITransformObserver, IDrawable
    {
        Matrix _curTransformMatrix;
        Vector2[] _originalVertices;
        Vector2[] _vertices;
        bool _areVerticesLive;
        bool _areVerticesInitalized = false;

        public Vector2[] Vertices
        {
            get
            {
                if (!_areVerticesLive)
                {
                    Polygon.TransformVertices(_originalVertices, _vertices, _curTransformMatrix);
                }

                return _vertices;
            }
        }

        // cached for Collision Detection
        public Vector2[] LeftNormals;
        public Vector2 Center;

        public override RectangleF BroadphaseBounds
        {
            get
            {
                if (!_areVerticesLive)
                {
                    _broadphaseBounds = Polygon.GetSurroundingRectangle(Vertices);
                }

                return _broadphaseBounds;
            }
        }
        RectangleF _broadphaseBounds;

        public Material Material => Material.Default;

        Texture2D _debugRectangle;
        RectangleF _debugRectangleUv = new RectangleF(0, 0, 1, 1);

        public PolygonCollider(Vector2[] vertices) : this()
        {
            _originalVertices = new Vector2[vertices.Length];
            _vertices = new Vector2[vertices.Length];

            Array.Copy(vertices, _originalVertices, vertices.Length);
            Array.Copy(vertices, Vertices, vertices.Length);

            _areVerticesInitalized = true;
        }

        public PolygonCollider() 
        {
            _debugRectangle = ContentLoader.Load<Texture2D>("rectangle");
        }

        public void TransformChanged(Transform transform)
        {
            Physics.Remove(this);
            Physics.Add(this);
        }

        /*public override bool Intersects(Collider other)
        {
            if (other is PolygonCollider polygonCollider)
                return Polygon.IntersectsPolygon(Vertices, LeftNormals, polygonCollider.Vertices, polygonCollider.LeftNormals);
            else if (other is CircleCollider circleCollider)
                return false;
        }*/

        public void TransformInitialized(Transform transform)
        {
            if (!_areVerticesInitalized)
            {
                var renderer = GetComponent<SpriteRenderer>();
                System.Diagnostics.Debug.Assert(renderer != null, "No renderer found for default Collider");
                RectangleF targetRectangle = renderer.TargetRectangle;

                _originalVertices = new Vector2[]{
                    new Vector2(targetRectangle.Left, targetRectangle.Top),
                    new Vector2(targetRectangle.Right, targetRectangle.Top),
                    new Vector2(targetRectangle.Right, targetRectangle.Bottom),
                    new Vector2(targetRectangle.Left, targetRectangle.Bottom)
                 };

                 //Vertices = new Vector2[_originalVertices.Length];
                 Array.Copy(_originalVertices, Vertices, _originalVertices.Length);
                _areVerticesInitalized = true;
            }

            Polygon.TransformVertices(_originalVertices, Vertices, transform.TransformMatrix);
            Physics.Add(this);
        }

        public void Draw()
        {
            Core.Batcher.PushQuad(_debugRectangle, _debugRectangleUv, Vertices[0], Vertices[1], Vertices[2], Vertices[3], Color.Red);
            //_broadphaseBounds = BroadphaseBounds;
            //Core.Batcher.PushQuad(_debugRectangle, _debugRectangleUv, new Vector2(_broadphaseBounds.Left, _broadphaseBounds.Top), new Vector2(_broadphaseBounds.Right, _broadphaseBounds.Top), new Vector2(_broadphaseBounds.Right, _broadphaseBounds.Bottom), new Vector2(_broadphaseBounds.Left, _broadphaseBounds.Bottom), Color.Red);
        }
    }
}