using System.Numerics;
using TZRaycastSystem;

public class Program
{
	public static void Main(string[] args)
	{
		var rayOrigin = new Vector3(-5f, 0f, 0f);
		var rayDirection = new Vector3(1f, 0f, 0f);
		Transform[] cubes =
		{
			new(new Vector3(0, 0, 0), Quaternion.Identity, Vector3.One),
			new(new Vector3(5, 0, 0), Quaternion.Identity, Vector3.One),
			new(new Vector3(0, 5, 0), Quaternion.Identity, Vector3.One)
		};

		var result = RaycastSystem.IsRayIntersectAnyCube(ref rayOrigin, ref rayDirection, cubes);
		Console.WriteLine(result.isHit ? $"Попал в куб {result.firstHitCubeIndex}" : "Не попал никуда");
	}
}