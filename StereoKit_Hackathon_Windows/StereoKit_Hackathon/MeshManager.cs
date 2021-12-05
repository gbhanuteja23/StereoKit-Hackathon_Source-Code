using StereoKit;
using System;
using System.Collections.Generic;

namespace StereoKit_Hackathon
{
	public class MeshManager
	{
		public static float cubeSize;

		List<Cube> cubes;

		public Action<Vec3> OnPinch;
		public Action<Vec3> OnUnPinch;
		public Action<Vec3> OnGrip;

		Cube _cubeToManipulate;

		public MeshManager()
		{
			cubeSize = 0.1f;
			cubes = new List<Cube>();

			_cubeToManipulate = null;

			OnPinch = (Vec3 pinchPos) => { };
			OnUnPinch = (Vec3 pinchPos) => { };
			OnGrip = (Vec3 pinchPos) => { };
		}

		#region Cube-Related-Functions
		public void Draw()
		{
			foreach (Cube _cube in cubes)
			{
				_cube.cubeModel.Draw(_cube.GetPose().ToMatrix());
			}
		}

		//Cube Handling
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
		public void ChangeCubeColor(Vec3 pinchPos)
		{
			pinchPos = pinchPos.RoundToClosest(cubeSize);

			var existingCube = CubeMeshAtPosition(pinchPos);

			if (existingCube != null)
			{
				existingCube.ChangeMaterialColor();
			}
		}

		//transform tools
		void MoveCubeStarted(Vec3 pinchPos)
		{
			pinchPos = pinchPos.RoundToClosest(cubeSize);

			_cubeToManipulate = CubeMeshAtPosition(pinchPos);
		}
		void MoveCubeEnded(Vec3 pinchPos)
		{
			pinchPos = pinchPos.RoundToClosest(cubeSize);

			_cubeToManipulate = CubeMeshAtPosition(pinchPos);
		}

		//cube utility
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
		#endregion
	}
}