using StereoKit;
using System;
using System.Collections.Generic;

namespace StereoKit_Hackathon
{
	public class MeshManager
	{
		public static Material cubeMaterial;
		List<Cube> cubes;

		public MeshManager()
		{
			cubeMaterial = Default.Material;
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
			Cube cube = new Cube(pinchPos);
			Console.WriteLine($"pinch pos z = {pinchPos.z}");
			cubes.Add(cube);
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