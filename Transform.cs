using Microsoft.Xna.Framework;
using Zen.Util;

namespace Zen
{
    public class Transform
    {
        public float Rotation;
        public Vector2 Position;
        public float Scale = 1;

		public void LookAt(Vector2 pos)
		{
			var sign = Position.X > pos.X ? -1 : 1;
			var vectorToAlignTo = Vector2.Normalize(Position - pos);
			Rotation = sign * Mathf.Acos(Vector2.Dot(vectorToAlignTo, Vector2.UnitY));
		}
    }
}