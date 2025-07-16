using System.Numerics;
using TZRaycastSystem;

namespace RaycastTests
{
	public class RaycastTests
	{
		[Fact]
		public void IsRayIntersectsAABB_WhenRayMissesAABB()
		{
			var rayOrigin = new Vector3(-1, 1, 0);
			var rayDirection = new Vector3(1, 0, 0);

			var result = RaycastSystem.IsRayIntersectsAABB(ref rayOrigin, ref rayDirection);

			Assert.False(result);
		}

		[Fact]
		public void IsRayIntersectsAABB_WhenRayOriginInsideAABB()
		{
			var rayOrigin = new Vector3(0, 0, 0);
			var rayDirection = new Vector3(1, 0, 0);

			var result = RaycastSystem.IsRayIntersectsAABB(ref rayOrigin, ref rayDirection);

			Assert.True(result);
		}

		[Fact]
		public void IsRayIntersectsAABB_WhenRayIntersectsAABB()
		{
			var rayOrigin = new Vector3(-1, 0, 0);
			var rayDirection = new Vector3(1, 0, 0);

			var result = RaycastSystem.IsRayIntersectsAABB(ref rayOrigin, ref rayDirection);

			Assert.True(result);
		}

		[Fact]
		public void IsRayIntersectsCube_WhenRayIntersectsCube()
		{
			var rayOrigin = new Vector3(0, 1, 1);
			var rayDirection = new Vector3(1, 0, 0);
			var cubeTransform = new Transform(Vector3.One, Quaternion.Identity, Vector3.One);

			var result = RaycastSystem.IsRayIntersectsCube(ref rayOrigin, ref rayDirection, ref cubeTransform);

			Assert.True(result);
		}

		[Fact]
		public void IsRayIntersectAnyCube_WhenRayMissesAnyCube()
		{
			var rayOrigin = new Vector3(5, 0, 0);
			var rayDirection = new Vector3(1, 0, 0);
			var cubes = new Transform[]
			{
				new(Vector3.Zero, Quaternion.Identity, Vector3.One),
				new(Vector3.One, Quaternion.Identity, Vector3.One)
			};

			var result = RaycastSystem.IsRayIntersectAnyCube(ref rayOrigin, ref rayDirection, cubes);

			Assert.True(!result.isHit);
			Assert.Equal(-1, result.firstHitCubeIndex);
		}

		[Fact]
		public void IsRayIntersectAnyCube_WhenRayIntersectsSecondCube()
		{
			var rayOrigin = new Vector3(0, 1, 1);
			var rayDirection = new Vector3(1, 0, 0);
			var cubes = new Transform[]
			{
				new(Vector3.Zero, Quaternion.Identity, Vector3.One),
				new(Vector3.One, Quaternion.Identity, Vector3.One)
			};

			var result = RaycastSystem.IsRayIntersectAnyCube(ref rayOrigin, ref rayDirection, cubes);

			Assert.True(result.isHit);
			Assert.Equal(1, result.firstHitCubeIndex);
		}
	}
}