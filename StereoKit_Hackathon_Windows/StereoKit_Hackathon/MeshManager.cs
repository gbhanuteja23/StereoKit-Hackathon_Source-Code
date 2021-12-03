using StereoKit;
using System;
using System.Collections.Generic;

namespace StereoKit_Hackathon
{
	public class MeshManager
	{
		public static float cubeSize;

		List<Cube> cubes;

		public MeshManager()
		{
			cubeSize = 0.1f;
			cubes = new List<Cube>();
		}

		public void Draw()
		{
			foreach (Cube _cube in cubes)
			{
				_cube.cubeModel.Draw(_cube.GetPose().ToMatrix());
			}
		}
		public void AddCube(Vec3 pinchPos)
		{
			pinchPos = pinchPos.RoundToClosest(cubeSize);

			if (CubeMeshAtPosition(pinchPos) != null) return;

			Cube cube = new Cube(pinchPos);
			cube.ChangeMaterialColor(); 
			cubes.Add(cube);
		}
		public void RemoveCube(Vec3 pinchPos)
		{
			pinchPos = pinchPos.RoundToClosest(cubeSize);

			var existingCube = CubeMeshAtPosition(pinchPos);

			if (existingCube != null)
			{
				cubes.Remove(existingCube);
			}
		}
		Cube CubeMeshAtPosition(Vec3 posToCheck)
		{
			foreach (Cube _cube in cubes)
			{
				if(_cube.GetPos().x == posToCheck.x &&
					_cube.GetPos().y == posToCheck.y &&
					_cube.GetPos().z == posToCheck.z)
				{
					return _cube;
				}
			}
			return null;
		}

		internal void HandleIsJustPinched()
		{
			for (int h = 0; h < (int)Handed.Max; h++)
			{
				// Get the pose for the index fingertip
				Hand hand = Input.Hand((Handed)h);
				if (Input.Hand((Handed)h).IsTracked && hand.IsJustPinched)
				{
					AddCube(Input.Hand((Handed)h).pinchPt);
					//Console.WriteLine($"Is tracked {hand.IsTracked} {h}");
				}
			}
		}

		internal void HandleIsJustGripped()
		{
			for (int h = 0; h < (int)Handed.Max; h++)
			{
				// Get the pose for the index fingertip
				Hand hand = Input.Hand((Handed)h);
				if (Input.Hand((Handed)h).IsTracked && hand.IsJustGripped)
				{
					RemoveCube(Input.Hand((Handed)h).pinchPt);
					//Console.WriteLine($"Is tracked {hand.IsTracked} {h}");
				}
			}
		}
	}
}