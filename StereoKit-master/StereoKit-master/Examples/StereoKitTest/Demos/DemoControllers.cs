﻿using StereoKit;

namespace StereoKitTest
{
	class DemoControllers : ITest
	{
		Matrix descPose    = Matrix.TR (-0.5f, 0, -0.5f, Quat.LookDir(1,0,1));
		string description = "While StereoKit prioritizes hand input, sometimes a controller has more precision! StereoKit provides access to any controllers via the Input.Controller function. This is a debug visualization of the controller data provided there.\n\nStereoKit will simulate hands if only controllers are present, but it will not simulate controllers if only hands are present.";
		Matrix titlePose   = Matrix.TRS(V.XYZ(-0.5f, 0.05f, -0.5f), Quat.LookDir(1, 0, 1), 2);
		string title       = "Controllers";

		public void Initialize()
		{
			Default.MaterialHand[MatParamName.ColorTint] = new Color(1,1,1,0.4f);
		}

		public void Shutdown()
		{
			Default.MaterialHand[MatParamName.ColorTint] = new Color(1,1,1,1);
		}

		public void Update()
		{
			ShowController(Handed.Right);
			ShowController(Handed.Left);

			Text.Add(title,       titlePose);
			Text.Add(description, descPose, V.XY(0.4f, 0), TextFit.Wrap, TextAlign.TopCenter, TextAlign.TopLeft);
		}

		/// :CodeSample: Controller Input.Controller TrackState Input.ControllerMenuButton Controller.IsTracked Controller.trackedPos Controller.trackedRot Controller.IsX1Pressed Controller.IsX2Pressed Controller.IsStickClicked Controller.stick Controller.aim Controller.grip Controller.trigger Controller.pose
		/// ### Controller Debug Visualizer
		/// This function shows a debug visualization of the current state of
		/// the controller! It's not something you'd show to users, but it's
		/// nice for just seeing how the API works, or as a temporary 
		/// visualization.
		void ShowController(Handed hand)
		{
			Controller c = Input.Controller(hand);
			if (!c.IsTracked) return;

			Hierarchy.Push(c.pose.ToMatrix());
				// Pick the controller color based on trackin info state
				Color color = Color.Black;
				if (c.trackedPos == TrackState.Inferred) color.g = 0.5f;
				if (c.trackedPos == TrackState.Known)    color.g = 1;
				if (c.trackedRot == TrackState.Inferred) color.b = 0.5f;
				if (c.trackedRot == TrackState.Known)    color.b = 1;
				Default.MeshCube.Draw(Default.Material, Matrix.S(new Vec3(3, 3, 8) * U.cm), color);

				// Show button info on the back of the controller
				Hierarchy.Push(Matrix.TR(0,1.6f*U.cm,0, Quat.LookAt(Vec3.Zero, new Vec3(0,1,0), new Vec3(0,0,-1))));

					// Show the tracking states as text
					Text.Add(c.trackedPos==TrackState.Known?"(pos)":(c.trackedPos==TrackState.Inferred?"~pos~":"pos"), Matrix.TS(0,-0.03f,0, 0.25f));
					Text.Add(c.trackedRot==TrackState.Known?"(rot)":(c.trackedRot==TrackState.Inferred?"~rot~":"rot"), Matrix.TS(0,-0.02f,0, 0.25f));

					// Show the controller's buttons
					Text.Add(Input.ControllerMenuButton.IsActive()?"(menu)":"menu", Matrix.TS(0,-0.01f,0, 0.25f));
					Text.Add(c.IsX1Pressed?"(X1)":"X1", Matrix.TS(0,0.00f,0, 0.25f));
					Text.Add(c.IsX2Pressed?"(X2)":"X2", Matrix.TS(0,0.01f,0, 0.25f));

					// Show the analog stick's information
					Vec3 stickAt = new Vec3(0, 0.03f, 0);
					Lines.Add(stickAt, stickAt + c.stick.XY0*0.01f, Color.White, 0.001f);
					if (c.IsStickClicked) Text.Add("O", Matrix.TS(stickAt, 0.25f));

					// And show the trigger and grip buttons
					Default.MeshCube.Draw(Default.Material, Matrix.TS(0, -0.015f, -0.005f, new Vec3(0.01f, 0.04f, 0.01f)) * Matrix.TR(new Vec3(0,0.02f,0.03f), Quat.FromAngles(-45+c.trigger*40, 0,0) ));
					Default.MeshCube.Draw(Default.Material, Matrix.TS(0.0149f*(hand == Handed.Right?1:-1), 0, 0.015f, new Vec3(0.01f*(1-c.grip), 0.04f, 0.01f)));

				Hierarchy.Pop();
			Hierarchy.Pop();

			// And show the pointer
			Default.MeshCube.Draw(Default.Material, c.aim.ToMatrix(new Vec3(1,1,4) * U.cm), Color.HSV(0,0.5f,0.8f).ToLinear());
		}
		/// :End:
	}
}
