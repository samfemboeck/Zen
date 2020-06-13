using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zen.Components;
using Zen.Util;

namespace Zen
{
    public class Transform : Component
    {
        public Matrix TransformMatrix => OriginMatrix * FlipMatrix * ScaleMatrix * RotationMatrix * TranslationMatrix;

        public Matrix OriginMatrix
        {
            get
            {
                if (isOriginMatrixDirty)
                {
                    originMatrix = Matrix.CreateTranslation(-Origin.X, -Origin.Y, 0);
                    isOriginMatrixDirty = false;
                }

                return originMatrix;
            }
        }

        public Matrix FlipMatrix
        {
            get
            {
                if (isFlipMatrixDirty)
                {
                    flipMatrix = new Matrix(
                        flip == Flip.X || Flip == Flip.XY ? -1 : 1, 0, 0, 0,
                        0, flip == Flip.Y || Flip == Flip.XY ? -1 : 1, 0, 0,
                        0, 0, 1, 0,
                        0, 0, 0, 1
                    );
                    isFlipMatrixDirty = false;
                }

                return flipMatrix;
            }
        }

        public Matrix ScaleMatrix
        {
            get
            {
                if (isScaleMatrixDirty)
                {
                    scaleMatrix = Matrix.CreateScale(scale);
                    isScaleMatrixDirty = false;
                }

                return scaleMatrix;
            }
        }

        public Matrix RotationMatrix
        {
            get
            {
                if (isRotationMatrixDirty)
                {
                    rotationMatrix = Matrix.CreateRotationZ(rotation);
                    isRotationMatrixDirty = false;
                }

                return rotationMatrix;
            }
        }

        public Matrix TranslationMatrix
        {
            get
            {
                if (isTranslationMatrixDirty)
                {
                    translationMatrix = Matrix.CreateTranslation(position.X, position.Y, 0);
                    isTranslationMatrixDirty = false;
                }

                return translationMatrix;
            }
        }

        Matrix transformMatrix;
        Matrix originMatrix;
        Matrix flipMatrix;
        Matrix scaleMatrix;
        Matrix rotationMatrix;
        Matrix translationMatrix;

        bool isOriginMatrixDirty = true;
        bool isFlipMatrixDirty = true;
        bool isScaleMatrixDirty = true;
        bool isRotationMatrixDirty = true;
        bool isTranslationMatrixDirty = true;

        public float Scale
        {
            get => scale;
            set
            {
                scale = value;
                isScaleMatrixDirty = true;
                NotifyObservers();
            }
        }

        public float Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
                isRotationMatrixDirty = true;
                NotifyObservers();
            }
        }

        public Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                isTranslationMatrixDirty = true;
                NotifyObservers();
            }
        }

        public Vector2 Origin
        {
            get => origin;
            set
            {
                origin = value;
                isOriginMatrixDirty = true;
                NotifyObservers();
            }
        }

        public Flip Flip
        {
            get => flip;
            set
            {
                flip = value;
                isFlipMatrixDirty = true;
                NotifyObservers();
            }
        }

        float scale = 1;
        float rotation;
        Vector2 position;
        Vector2 origin;
        Flip flip;

        List<ITransformObserver> observers = new List<ITransformObserver>();

        public void LookAt(Vector2 pos)
        {
            var sign = Position.X > pos.X ? -1 : 1;
            var vectorToAlignTo = Vector2.Normalize(Position - pos);
            Rotation = sign * Mathf.Acos(Vector2.Dot(vectorToAlignTo, Vector2.UnitY));
        }

        public override void Awake()
        {
            foreach (Component component in Entity.GetComponents())
            {
                if (component is ITransformObserver observer)
                {
                    observers.Add(observer);
                }
            }
        }

        void NotifyObservers()
        {
            foreach (ITransformObserver observer in observers)
                observer.TransformChanged(this);
        }
    }
}