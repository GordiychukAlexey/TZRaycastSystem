using UnityEngine;

namespace TZRaycastSystem
{
	public static class Vector3Extensions
	{
		public static System.Numerics.Vector3 ToNumericsVector(this Vector3 vector) =>
			new(vector.x, vector.y, vector.z);

		public static System.Numerics.Quaternion ToNumericsQuaternion(this Quaternion quaternion) =>
			new(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
	}
}