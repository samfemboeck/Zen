using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Components;
using Zen.Util;

namespace Zen
{
    public class CircleCollider : Collider, ITransformObserver, IDrawable
    {
        Vector2[] _originalVertices;
        public Vector2[] Vertices;

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
        bool _isCenterDirty = true;

        public float Radius
        {
            get
            {
                if (_isRadiusDirty)
                {
                    Vector2 centerTop = Vertices[0] + 0.5f * (Vertices[1] - Vertices[0]);
                    _radius = (centerTop - Center).Length();
                    _isRadiusDirty = false;
                }

                return _radius;
            }
        }
        float _radius;
        bool _isRadiusDirty = true;

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
        bool _areBroadphaseBoundsDirty = true;
        RectangleF _broadphaseBounds;

        bool _areVerticesInitialized;

        Texture2D _debugTexture;
        RectangleF _debugTextureUv = new RectangleF(0, 0, 1, 1);
        public Material Material => Material.Default;

        public CircleCollider(Vector2 leftTop, Vector2 rightTop, Vector2 rightBottom, Vector2 leftBottom) : this()
        {
            InitializeVertices(leftTop, rightTop, rightBottom, leftBottom);
        }

        public CircleCollider()
        {
            _debugTexture = ContentLoader.Load<Texture2D>("Mobs/Squid/circle");
        }

        public override bool Intersects(Collider other)
        {
            if (other is PolygonCollider polygonCollider)
                return Polygon.IntersectsCircle(polygonCollider.Vertices, polygonCollider.Center, Center, Radius);
            else if (other is CircleCollider circleCollider)
                return Circle.IntersectsCircle(Center, Radius, circleCollider.Center, circleCollider.Radius);

            return false;
        }

        public void TransformChanged(Transform transform)
        {
            Physics.RemoveWithinBounds(this);

            Polygon.TransformVertices(_originalVertices, Vertices, transform.TransformMatrix);
            _isCenterDirty = true;
            _isRadiusDirty = true;
            _areBroadphaseBoundsDirty = true;
            
            Physics.Add(this);
        }

        void InitializeVertices(Vector2 leftTop, Vector2 rightTop, Vector2 rightBottom, Vector2 leftBottom)
        {
            _originalVertices = new Vector2[4];
            Vertices = new Vector2[4];

            _originalVertices[0] = leftTop;
            _originalVertices[1] = rightTop;
            _originalVertices[2] = rightBottom;
            _originalVertices[3] = leftBottom;

            Array.Copy(_originalVertices, Vertices, _originalVertices.Length);
            _areVerticesInitialized = true;
        }

        public void TransformInitialized(Transform transform)
        {
            if (!_areVerticesInitialized)
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                Debug.Assert(spriteRenderer != null, "Can't add default collider without SpriteRenderer");

                RectangleF targetRectangle = spriteRenderer.TargetRectangle;
                InitializeVertices(
                    new Vector2(0, 0), 
                    new Vector2(targetRectangle.Width, 0), 
                    new Vector2(targetRectangle.Width, targetRectangle.Height), 
                    new Vector2(0, targetRectangle.Height)
                );
            }

            Polygon.TransformVertices(_originalVertices, Vertices, transform.TransformMatrix);
            Physics.Add(this);
        }

        public void Draw()
        {
            Core.Batcher.PushQuad(_debugTexture, _debugTextureUv, Vertices[0], Vertices[1], Vertices[2], Vertices[3], Color.Red);
        }
    }
}