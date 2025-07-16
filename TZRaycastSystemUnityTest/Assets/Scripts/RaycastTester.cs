using UnityEngine;
using System.Collections.Generic;
using TZRaycastSystem;
using Random = UnityEngine.Random;
using Transform = UnityEngine.Transform;

[ExecuteInEditMode]
public class RaycastTester : MonoBehaviour
{
	[SerializeField] private int _rayCount = 100;
	[SerializeField] private float _rayGizmosLength = 10f;
	[SerializeField] private Color _hitColor = Color.green;
	[SerializeField] private Color _missColor = Color.red;
	[SerializeField] private List<Transform> _cubeTransforms = new();

	private readonly List<Vector3> _rayDirections = new();

	private void OnValidate()
	{
		_rayDirections.Clear();

		for (var i = 0; i < _rayCount; i++)
		{
			_rayDirections.Add(Random.onUnitSphere);
		}
//		_rayDirections.Add(Vector3.left);
//		_rayDirections.Add(Vector3.right);
//		_rayDirections.Add(Vector3.up);
//		_rayDirections.Add(Vector3.down);
//		_rayDirections.Add(Vector3.forward);
//		_rayDirections.Add(Vector3.back);
//		_rayDirections.Add(Vector3.one);
//		_rayDirections.Add(Vector3.zero);
	}

	private void OnDrawGizmos()
	{
		var cubesArray = new TZRaycastSystem.Transform[_cubeTransforms.Count];
		for (var i = 0; i < _cubeTransforms.Count; i++)
		{
			cubesArray[i] = new TZRaycastSystem.Transform(
				_cubeTransforms[i].position.ToNumericsVector(),
				_cubeTransforms[i].rotation.ToNumericsQuaternion(),
				_cubeTransforms[i].lossyScale.ToNumericsVector());
		}

		var rayPositionNumerics = transform.position.ToNumericsVector();

		foreach (var rayDirection in _rayDirections)
		{
			var rayDirectionNumerics = rayDirection.ToNumericsVector();

			var x = RaycastSystem.IsRayIntersectAnyCube(ref rayPositionNumerics, ref rayDirectionNumerics, cubesArray);

			Gizmos.color = x.isHit ? _hitColor : _missColor;
			Gizmos.DrawRay(transform.position, rayDirection * _rayGizmosLength);

//			Debug.Log($"{rayDirection} {(x.isHit?$"hit {x.cubeIndex}":"not hit")}");
		}
	}
}