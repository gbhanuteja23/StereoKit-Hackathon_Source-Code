using StereoKit;

namespace StereoKit_Hackathon
{
	class UIManager
	{

		public void ShowWindow(MeshManager _meshManager)
		{
			Pose windowLeftPose = new Pose(-0.3f, 0, -0.3f, Quat.LookAt(Vec3.Forward, Vec3.Zero));
			Pose windowRightPose = new Pose(0.3f, 0, -0.3f, Quat.LookAt(Vec3.Forward, Vec3.Zero));


			UI.WindowBegin("Pinch", ref windowLeftPose);

			if (UI.Button("Spawn"))
			{
				_meshManager.OnPinch = _meshManager.AddCube;
			}
			if (UI.Button("Delete"))
			{
				_meshManager.OnPinch = _meshManager.RemoveCube;
			}
			if (UI.Button("Change"))
			{
				_meshManager.OnPinch = _meshManager.ChangeCubeColor;
			}

			UI.WindowEnd();

			UI.WindowBegin("Grip", ref windowRightPose);

			if (UI.Button("Spawn"))
			{
				_meshManager.OnGrip = _meshManager.AddCube;
			}
			if (UI.Button("Delete"))
			{
				_meshManager.OnGrip = _meshManager.RemoveCube;
			}
			if (UI.Button("Change"))
			{
				_meshManager.OnGrip = _meshManager.ChangeCubeColor;
			}

			UI.WindowEnd();
		}
	}
}
