using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using Zen.Components;
using Zen.Util;

namespace Zen
{
    // TODO testing
    // 1. collision default
    // 2. collision custom
    // 3. change radius collision/transform
    public class CircleCollider : Collider, ITransformObserver, IDrawable
    {
        public Circle Circle;
        public override RectangleF BroadphaseBounds => Circle.BroadphaseBounds;
        bool isDefaultCollider = true;
        Vector2 customCenter;
        float customRadius;
        bool customStatic;
        bool isStatic;
        Texture2D debugTexture;

        public Material Material => Material.Default;

        public CircleCollider(Vector2 localCenter, float radius) : this()
        {
            isDefaultCollider = false;
            customCenter = localCenter;
            customRadius = radius;
        }

        public CircleCollider()
        {
            debugTexture = ContentLoader.Load<Texture2D>("Mobs/Squid/circle");
        }

        public Vector2 LocalCenter
        {
            get => Circle.LocalCenter;
            set
            {
                Circle.LocalCenter = value;
            }
        }

        public float LocalRadius
        {
            get => Circle.LocalRadius;
            set
            {
                Circle.LocalRadius = value;
            }
        }

        public override void Awake()
        {
            Transform transform = GetComponent<Transform>();

            if (isDefaultCollider)
            {
                RectangleF targetRectangle = GetComponent<SpriteRenderer>().TargetRectangle;
                float radius = Math.Max(targetRectangle.Width * 0.5f, targetRectangle.Height * 0.5f);
                Circle = new Circle(targetRectangle.Center, radius, transform);
            }
            else
            {
                Circle = new Circle(customCenter, customRadius, transform);
            }

            Physics.Add(this);
        }

        public bool Intersects(Collider other)
        {
            if (other is CircleCollider circleCollider)
                return Circle.IntersectsCircle(circleCollider.Circle);
            // TODO
            return false;
        }

        public bool Encases(Collider other)
        {
            if (other is CircleCollider circleCollider)
                return Circle.EncasesCircle(circleCollider.Circle);
            // TODO
            return false;
        }

        public void TransformChanged(Transform transform)
        {
            Physics.Remove(this);

            Circle.Update(transform.Scale);

            Physics.Add(this);
        }

        public void Draw()
        {
            Core.Batcher.PushQuad(
                debugTexture,
                new RectangleF(0, 0, 1, 1),
                new Vector2(BroadphaseBounds.Left, BroadphaseBounds.Top),
                new Vector2(BroadphaseBounds.Right, BroadphaseBounds.Top),
                new Vector2(BroadphaseBounds.Right, BroadphaseBounds.Bottom),
                new Vector2(BroadphaseBounds.Left, BroadphaseBounds.Bottom),
                Color.Red
            );
        }
    }
}