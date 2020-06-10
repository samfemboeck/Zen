using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zen.Components;
using Zen.Util;

namespace Zen
{
    public class Transform : Component, IUpdatable
    {
        public Matrix TransformMatrix;
        Matrix _originMatrix;
        Matrix _flipMatrix;
        Matrix _scaleMatrix;
        Matrix _rotationMatrix;
        Matrix _translationMatrix;

        public float Scale
        {
            get => _scale;
            set
            {
                if (value != _scale)
                {
                    _scale = value;
                    _isScaleDirty = true;
                    UpdateMatrix();
                    NotifyObservers();
                }
            }
        }

        public float Rotation
        {
            get => _rotation;
            set
            {
                if (value != _rotation)
                {
                    _rotation = value;
                    _isRotationDirty = true;
                    UpdateMatrix();
                    NotifyObservers();
                }
            }
        }

        public Vector2 Position
        {
            get => _position;
            set
            {
                if (value != _position)
                {
                    _position = value;
                    _isPositionDirty = true;
                    UpdateMatrix();
                    NotifyObservers();
                }
            }
        }

        public Vector2 Origin
        {
            get => _origin;
            set
            {
                if (value != _origin)
                {
                    _origin = value;
                    _isOriginDirty = true;
                    UpdateMatrix();
                    NotifyObservers();
                }
            }
        }

        public bool FlipX
        {
            get => _flipX;
            set
            {
                if (value != _flipX)
                {
                    _flipX = value;
                    _isFlipDirty = true;
                    UpdateMatrix();
                    NotifyObservers();
                }
            }
        }

        public bool FlipY
        {
            get => _flipY;
            set
            {
                if (value != _flipY)
                {
                    _flipY = value;
                    _isFlipDirty = true;
                }
            }
        }

        float _scale = 1;
        float _rotation;
        Vector2 _position;
        Vector2 _origin;
        bool _flipX;
        bool _flipY;

        bool _isScaleDirty = true;
        bool _isRotationDirty = true;
        bool _isPositionDirty = true;
        bool _isOriginDirty = true;
        bool _isFlipDirty = true;

        List<ITransformObserver> observers = new List<ITransformObserver>();

        public void LookAt(Vector2 pos)
        {
            var sign = Position.X > pos.X ? -1 : 1;
            var vectorToAlignTo = Vector2.Normalize(Position - pos);
            Rotation = sign * Mathf.Acos(Vector2.Dot(vectorToAlignTo, Vector2.UnitY));
        }

        public override void Mount()
        {
            foreach (Component component in Entity.GetComponents())
            {
                if (component is ITransformObserver observer)
                {
                    observers.Add(observer);
                }
            }

            UpdateMatrix();
            NotifyObservers();
        }

        void UpdateMatrix()
        {
            if (_isOriginDirty)
            {
                _originMatrix = Matrix.CreateTranslation(-Origin.X, -Origin.Y, 0);
                _isOriginDirty = false;
            }

            if (_isFlipDirty)
            {
                _flipMatrix = new Matrix(
                    _flipX ? -1 : 1, 0, 0, 0,
                    0, _flipY ? -1 : 1, 0, 0,
                    0, 0, 1, 0,
                    0, 0, 0, 1
                );
                _isFlipDirty = false;
            }

            if (_isScaleDirty)
            {
                _scaleMatrix = Matrix.CreateScale(_scale);
                _isScaleDirty = false;
            }

            if (_isRotationDirty)
            {
                _rotationMatrix = Matrix.CreateRotationZ(_rotation);
                _isRotationDirty = false;
            }

            if (_isPositionDirty)
            {
                _translationMatrix = Matrix.CreateTranslation(_position.X, _position.Y, 0);
                _isPositionDirty = false;
            }

            TransformMatrix = _originMatrix * _flipMatrix * _scaleMatrix * _rotationMatrix * _translationMatrix;
        }

        void NotifyObservers()
        {
            foreach (ITransformObserver observer in observers)
                observer.TransformChanged(this);
        }

        public void Update()
        {
            if (_isOriginDirty || _isScaleDirty || _isRotationDirty || _isPositionDirty || _isFlipDirty)
                UpdateMatrix();
        }
    }
}