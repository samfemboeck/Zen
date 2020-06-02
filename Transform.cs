using System;
using Microsoft.Xna.Framework;

namespace Zen.EC
{
    public class Transform
    {
        public Vector2 Position;
        public float Scale = 1;
        public float Rotation;

        /// <summary>
		/// Rotate so the top of the sprite is facing <see cref="pos"/>
		/// </summary>
		/// <param name="pos">The position to look at</param>
		public void LookAt(Vector2 pos)
		{
			var sign = Position.X > pos.X ? -1 : 1;
			var vectorToAlignTo = Vector2.Normalize(Position - pos);
			Rotation = sign * Mathf.Acos(Vector2.Dot(vectorToAlignTo, Vector2.UnitY));
		}
    }
}