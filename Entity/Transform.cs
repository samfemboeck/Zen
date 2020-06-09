using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Zen.Components;
using Zen.Util;

namespace Zen
{
    public class Transform : Component, IUpdatable
    {
        Matrix _transformMatrix;
        Matrix _originMatrix;
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
        bool _isFlipDirty = false; // TODO

        List<ITransformObserver> observers = new List<ITransformObserver>();

        public void LookAt(Vector2 pos)
        {
            var sign = Position.X > pos.X ? -1 : 1;
            var vectorToAlignTo = Vector2.Normalize(Position - pos);
            Rotation = sign * Mathf.Acos(Vector2.Dot(vectorToAlignTo, Vector2.UnitY));
        }

        public override void PreMount()
        {
            foreach (Component component in Entity.GetComponents())
            {
                if (component is ITransformObserver observer)
                {
                    observers.Add(observer);
                }
            }
        }

        public override void Mount()
        {
            UpdateMatrix();
        }

        void UpdateMatrix()
        {
            if (_isOriginDirty)
            {
                _originMatrix = Matrix.CreateTranslation(-Origin.X, -Origin.Y, 0);
                _isOriginDirty = false;
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

            _transformMatrix = _originMatrix * _scaleMatrix * _rotationMatrix * _translationMatrix;

            foreach (ITransformObserver observer in observers)
                observer.TransformChanged(_transformMatrix);
        }

        public void Update()
        {
            if (_isOriginDirty || _isScaleDirty || _isRotationDirty || _isPositionDirty || _isFlipDirty)
                UpdateMatrix();
        }
    }
}