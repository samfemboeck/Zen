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
        public Vector2[] Vertices;
        Vector2[] _originalVertices;

        public Vector2[] LeftNormals
        {
            get
            {
                if (_areLeftNormalsDirty)
                {
                    _leftNormals = Polygon.GetLeftNormals(Vertices);
                    _areLeftNormalsDirty = false;
                }
                
                return _leftNormals;
            }
        }
        Vector2[] _leftNormals;
        bool _areLeftNormalsDirty = true;

        public Vector2 Center
        {
            get
            {
                if (_isCenterDirty)
                {
                    _center = Polygon.GetCenter(Vertices);
                    _isCenterDirty = false;
                }

                return _center;
            }
        }
        Vector2 _center;
        bool _isCenterDirty;

        public override RectangleF BroadphaseBounds
        {
            get
            {
                if (_areBroadphaseBoundsDirty)
                {
                    _broadphaseBounds = Polygon.GetSurroundingRectangle(Vertices);
                    _areBroadphaseBoundsDirty = false;
                }

                return _broadphaseBounds;
            }
        }
        RectangleF _broadphaseBounds;
        bool _areBroadphaseBoundsDirty = true;

        public Material Material => Material.Default;

        Texture2D _debugRectangle;
        RectangleF _debugRectangleUv = new RectangleF(0, 0, 1, 1);

        bool _areVerticesInitalized;

        public PolygonCollider(Vector2[] vertices) : this()
        {
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
            Physics.RemoveWithinBounds(this);

            Polygon.TransformVertices(_originalVertices, Vertices, transform.TransformMatrix);
            _areBroadphaseBoundsDirty = true;
            _isCenterDirty = true;
            _areLeftNormalsDirty = true;

            Physics.Add(this);
        }

        public override bool Intersects(Collider other)
        {
            if (other is PolygonCollider polygonCollider)
                return Polygon.IntersectsPolygon(Vertices, LeftNormals, polygonCollider.Vertices, polygonCollider.LeftNormals);
            else if (other is CircleCollider circleCollider)
                return Polygon.IntersectsCircle(Vertices, Center, circleCollider.Center, circleCollider.Radius);

            return false;
        }

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

                 Vertices = new Vector2[_originalVertices.Length];
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