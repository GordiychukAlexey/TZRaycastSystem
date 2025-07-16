using System;
using System.Numerics;

namespace TZRaycastSystem
{
	public struct Transform
	{
		public Vector3 position;
		public Quaternion rotation;
		public Vector3 scale;

		public Transform(in Vector3 position, in Quaternion rotation, in Vector3 scale)
		{
			this.position = position;
			this.rotation = rotation;
			this.scale = scale;
		}

		public Matrix4x4 GetLocalToWorldMatrix() =>
			Matrix4x4.CreateScale(scale) *
			Matrix4x4.CreateFromQuaternion(rotation) *
			Matrix4x4.CreateTranslation(position);

		public bool IsInvertible() => MathF.Abs(GetLocalToWorldMatrix().GetDeterminant()) >= float.Epsilon;

		public bool TryGetWorldToLocalMatrix(out Matrix4x4 worldToLocalMatrix) =>
			Matrix4x4.Invert(GetLocalToWorldMatrix(), out worldToLocalMatrix);
	}
}