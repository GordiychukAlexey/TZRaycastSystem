using System;
using System.Numerics;

namespace TZRaycastSystem
{
	public static class RaycastSystem
	{
		private static readonly Vector3 aabbMin = new(-0.5f);
		private static readonly Vector3 aabbMax = new(0.5f);

		public static (bool isHit, int firstHitCubeIndex) IsRayIntersectAnyCube(
			ref Vector3 rayOrigin,
			ref Vector3 direction,
			Transform[] cubes)
		{
			for (var i = 0; i < cubes.Length; i++)
			{
				if (IsRayIntersectsCube(ref rayOrigin, ref direction, ref cubes[i]))
				{
					return (true, i);
				}
			}

			return (false, -1);
		}

		public static bool IsRayIntersectsCube(ref Vector3 rayOrigin, ref Vector3 rayDirection,
			ref Transform cubeTransform)
		{
			if (!cubeTransform.TryGetWorldToLocalMatrix(out var worldToLocalMatrix)) return false;

			var localOrigin = Vector3.Transform(rayOrigin, worldToLocalMatrix);
			var localDirection = Vector3.TransformNormal(rayDirection, worldToLocalMatrix);
			localDirection = Vector3.Normalize(localDirection);

			return IsRayIntersectsAABB(ref localOrigin, ref localDirection);
		}

		/// <summary>
		/// https://gamedev.stackexchange.com/questions/18436/most-efficient-aabb-vs-ray-collision-algorithms
		/// </summary>
		public static bool IsRayIntersectsAABB(ref Vector3 rayOrigin, ref Vector3 rayDirection)
		{
			//начало внутри куба
			if (rayOrigin.X >= aabbMin.X && rayOrigin.X <= aabbMax.X &&
			    rayOrigin.Y >= aabbMin.Y && rayOrigin.Y <= aabbMax.Y &&
			    rayOrigin.Z >= aabbMin.Z && rayOrigin.Z <= aabbMax.Z)
			{
				return true;
			}

			var close = (aabbMin - rayOrigin) / rayDirection;
			var far = (aabbMax - rayOrigin) / rayDirection;

			var tmin = Math.Max(Math.Max(Math.Min(close.X, far.X), Math.Min(close.Y, far.Y)), Math.Min(close.Z, far.Z));
			var tmax = Math.Min(Math.Min(Math.Max(close.X, far.X), Math.Max(close.Y, far.Y)), Math.Max(close.Z, far.Z));

			// tmax < 0 - луч пересекает AABB, но в отрицательном направлении
			// tmin > tmax - луч не пересекает AABB
			return tmax >= 0 && tmin <= tmax;
		}
	}
}