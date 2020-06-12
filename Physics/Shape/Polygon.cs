using System;
using Microsoft.Xna.Framework;
using Zen.Util;

namespace Zen
{
    public static class Polygon
    {
		public static void TransformVertices(Vector2[] vertices, Vector2[] transformedVertices, Matrix transformMatrix)
		{
			for (int i = 0; i < vertices.Length; i++)
                transformedVertices[i] = Vector2.Transform(vertices[i], transformMatrix);
		}

        public static Vector2[] GetLeftNormals(Vector2[] vertices)
        {
			Vector2[] ret = new Vector2[vertices.Length];

			Vector2 p2;
			for (var i = 0; i < vertices.Length; i++)
			{
				var p1 = vertices[i];
				if (i + 1 >= vertices.Length)
					p2 = vertices[0];
				else
					p2 = vertices[i + 1];

				Vector2 normal = GetLeftNormal(p1, p2);
				ret[i] = normal;
			}

			return ret;
        }

        public static Vector2 GetLeftNormal(Vector2 point1, Vector2 point2)
		{
			Vector2 normal = new Vector2(-1f * (point2.Y - point1.Y), point2.X - point1.X);
			normal.Normalize();
			return normal;
		}

        public static Vector2 GetCenter(Vector2[] vertices)
		{
			float x = 0, y = 0;

			for (var i = 0; i < vertices.Length; i++)
			{
				x += vertices[i].X;
				y += vertices[i].Y;
			}

			return new Vector2(x / vertices.Length, y / vertices.Length);
		}

		public static bool IntersectsPolygon(Vector2[] verticesA, Vector2[] normalsA, Vector2[] verticesB, Vector2[] normalsB)
		{
			float minA;
			float maxA;
			float minB;
			float maxB;

			foreach (Vector2 normal in normalsA)
			{
				GetMinMaxProjections(normal, verticesA, verticesB, out minA, out maxA, out minB, out maxB);

				if (maxA < minB || maxB < minA)
					return false;
			}

			foreach (Vector2 normal in normalsB)
			{
				GetMinMaxProjections(normal, verticesA, verticesB, out minA, out maxA, out minB, out maxB);

				if (maxA < minB || maxB < minA)
					return false;
			}

			return true;
		}

		public static bool IntersectsCircle(Vector2[] polygonVertices, Vector2 polygonCenter, Vector2 circleCenter, float circleRadius)
		{
			Vector2 direction = circleCenter - polygonCenter;
			Vector2 directionNormalized = Vector2.Normalize(direction);

			float maxProjection = GetMaxProjectionFromCenter(directionNormalized, polygonVertices, polygonCenter);
			return direction.Length() - maxProjection - circleRadius <= 0;
		}

		public static float GetMaxProjectionFromCenter(Vector2 axis, Vector2[] vertices, Vector2 center)
		{
			float max = float.MinValue;

			foreach (Vector2 vertex in vertices)
			{
				Vector2 fromCenter = vertex - center;
				float projection = Vector2.Dot(fromCenter, axis);

				if (projection > max)
					max = projection;
			}

			return max;
		}

		public static void GetMinMaxProjections(Vector2 normal, Vector2[] verticesA, Vector2[] verticesB, out float minA, out float maxA, out float minB, out float maxB)
		{
			minA = float.MaxValue;
			maxA = 0;
			minB = float.MaxValue;
			maxB = 0;

			foreach (Vector2 vertex in verticesA)
			{
				float projection = Vector2.Dot(vertex, normal);

				if (projection > maxA)
					maxA = projection;

				if (projection < minA)
					minA = projection;
			}

			foreach (Vector2 vertex in verticesB)
			{
				float projection = Vector2.Dot(vertex, normal);

				if (projection > maxB)
					maxB = projection;

				if (projection < minB)
					minB = projection;
			}
		}

		public static RectangleF GetSurroundingRectangle(Vector2[] vertices)
		{

            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;

            foreach (Vector2 vertex in vertices)
            {
                if (vertex.X < minX)
                    minX = vertex.X;
                if (vertex.X > maxX)
                    maxX = vertex.X;
                
                if (vertex.Y < minY)
                    minY = vertex.Y;
                if (vertex.Y > maxY)
                    maxY = vertex.Y;
            }

            return new RectangleF(minX, minY, maxX - minX, maxY - minY);
		}
    }
}