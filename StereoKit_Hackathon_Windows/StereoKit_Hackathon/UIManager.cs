using StereoKit;
using System;

namespace StereoKit_Hackathon
{
	class UIManager
	{
		string pinchMode = "";
		string gripMode = "";

		Vec3 m_leftPosition;
		Vec3 m_rightPosition;

		Vec3 leftPosition { 
			get { return m_leftPosition + Input.Head.position; } 
			set { m_leftPosition = value; } 
		}
		Vec3 rightPosition {
			get { return m_rightPosition + Input.Head.position; }
			set { m_rightPosition = value; }
		}

		Pose windowLeftPose;
		Pose windowRightPose;

		public UIManager()
		{
			leftPosition = new Vec3(-0.3f, 0f, -0.3f);
			rightPosition = new Vec3(0.3f, 0f, -0.3f);
		}

		public void ShowWindow(MeshManager _meshManager)
		{
			windowLeftPose = new Pose(leftPosition, Quat.LookAt(leftPosition, Input.Head.position));
			windowRightPose = new Pose(rightPosition, Quat.LookAt(rightPosition, Input.Head.position));

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
