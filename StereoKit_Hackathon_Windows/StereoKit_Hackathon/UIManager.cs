using StereoKit;

namespace StereoKit_Hackathon
{
	class UIManager
	{
		string pinchMode = "";
		string gripMode = "";
		
		static Vec3 leftPosition = new Vec3(-0.3f, 0f, -0.3f);
		static Vec3 rightPosition = new Vec3(0.3f, 0f, -0.3f);

		Pose windowLeftPose = new Pose(leftPosition, Quat.LookAt(leftPosition, Vec3.Zero));
		Pose windowRightPose = new Pose(rightPosition, Quat.LookAt(rightPosition, Vec3.Zero));

		public void ShowWindow(MeshManager _meshManager)
		{
			//left pinch window
			UI.WindowBegin("Pinch", ref windowLeftPose, moveType: UIMove.FaceUser);

			UI.HSeparator();
			UI.Text(pinchMode, TextAlign.TopCenter);
			UI.HSeparator();

			if (UI.Button("Spawn"))
			{
				_meshManager.OnPinch = _meshManager.AddCube;
				pinchMode = "Spawn";
			}
			if (UI.Button("Delete"))
			{
				_meshManager.OnPinch = _meshManager.RemoveCube;
				pinchMode = "Delete";
			}
			if (UI.Button("Change"))
			{
				_meshManager.OnPinch = _meshManager.ChangeCubeColor;
				pinchMode = "Change";
			}

			UI.WindowEnd();


			//right grip window
			UI.WindowBegin("Grip", ref windowRightPose, moveType: UIMove.FaceUser);

			UI.HSeparator();
			UI.Text(gripMode, TextAlign.TopCenter);
			UI.HSeparator();

			if (UI.Button("Spawn"))
			{
				_meshManager.OnGrip = _meshManager.AddCube;
				gripMode = "Spawn";
			}
			if (UI.Button("Delete"))
			{
				_meshManager.OnGrip = _meshManager.RemoveCube;
				gripMode = "Delete";
			}
			if (UI.Button("Change"))
			{
				_meshManager.OnGrip = _meshManager.ChangeCubeColor;
				gripMode = "Change";
			}

			UI.WindowEnd();
		}
	}
}
