using Microsoft.Xna.Framework;
using Zen.Util;

namespace Zen
{
    public class Transform
    {
        public Matrix Matrix;
        public Entity Entity;

        public Transform(Entity entity)
        {
            Entity = entity;
        }

        public float Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                _isScaleDirty = true;
                UpdateMatrix();
            }
        }

        public float Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                _isRotationDirty = true;
                UpdateMatrix();
            }
        }

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                _isPositionDirty = true;
                UpdateMatrix();
            }
        }

        float _scale = 1;
        float _rotation;
        Vector2 _position;
        bool _isRotationDirty;
        bool _isPositionDirty;
        bool _isScaleDirty;

		public void LookAt(Vector2 pos)
		{
			var sign = Position.X > pos.X ? -1 : 1;
			var vectorToAlignTo = Vector2.Normalize(Position - pos);
			Rotation = sign * Mathf.Acos(Vector2.Dot(vectorToAlignTo, Vector2.UnitY));
		}

        void UpdateMatrix()
        {
            Matrix transform = Matrix.Identity;

            if (_isScaleDirty)
                transform *= Matrix.CreateScale(_scale);

            if (_isRotationDirty)
                transform *= Matrix.CreateRotationZ(_rotation);

            if (_isPositionDirty)
                transform *= Matrix.CreateTranslation(_position.X, _position.Y, 0);
            
            Matrix = transform;
            Entity.UpdateTransform();
        }
    }
}