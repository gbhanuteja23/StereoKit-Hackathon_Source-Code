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

			if (CheckIfCubeMeshAtPosition(pinchPos)) return;

			Cube cube = new Cube(pinchPos);
			cube.ChangeMaterialColor(); 
			cubes.Add(cube);
		}
		bool CheckIfCubeMeshAtPosition(Vec3 posToCheck)
		{
			foreach (Cube _cube in cubes)
			{
				if(_cube.GetPos().x == posToCheck.x &&
					_cube.GetPos().y == posToCheck.y &&
					_cube.GetPos().z == posToCheck.z)
				{
					return true;
				}
			}
			return false;
		}

		public void HandleIsJustPinched()
		{
			for (int h = 0; h < (int)Handed.Max; h++)
			{
				// Get the pose for the index fingertip
				Hand hand = Input.Hand((Handed)h);
				if (Input.Hand((Handed)h).IsTracked && hand.IsJustPinched)
				{
					AddCube(Input.Hand((Handed)h).pinchPt);
					Console.WriteLine($"Is tracked {hand.IsTracked} {h}");
				}
			}
		}
	}
}