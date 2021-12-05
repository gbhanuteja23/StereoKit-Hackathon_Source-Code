using System;
using StereoKit;

namespace StereoKit_Hackathon
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize StereoKit
            SKSettings settings = new SKSettings
            {
                appName = "StereoKit_Hackathon",
                assetsFolder = "Assets",
            };
            if (!SK.Initialize(settings))
                Environment.Exit(1);

            //floor
            Matrix floorTransform = Matrix.TS(0, -1.5f, 0, new Vec3(30, 0.1f, 30));
            Material floorMaterial = new Material(Shader.FromFile("floor.hlsl"));
            floorMaterial.Transparency = Transparency.Blend;

            //initialize and create objects
            MeshManager meshManager = new MeshManager();
            UIManager uiManager = new UIManager();

            // Core application loop
            while (SK.Step(() =>
            {
                if (SK.System.displayType == Display.Opaque)
                    Default.MeshCube.Draw(floorMaterial, floorTransform);

                for (int h = 0; h < (int)Handed.Max; h++)
                {
                    // Get the pose for the index fingertip
                    Hand hand = Input.Hand((Handed)h);
                    if (Input.Hand((Handed)h).IsTracked && hand.IsJustPinched && !UI.IsInteracting((Handed)h))
                    {
                        meshManager?.OnPinch(Input.Hand((Handed)h).pinchPt);
                    }
                    if (Input.Hand((Handed)h).IsTracked && hand.IsJustUnpinched && !UI.IsInteracting((Handed)h))
                    {
                        meshManager?.OnUnPinch(Input.Hand((Handed)h).pinchPt);
                    }
                    if (Input.Hand((Handed)h).IsTracked && hand.IsJustGripped && !UI.IsInteracting((Handed)h))
					{
                        meshManager?.OnGrip(Input.Hand((Handed)h).pinchPt);
                    }
                }
                meshManager.Draw();
                uiManager.ShowWindow(meshManager);

                //UI.Handle("Cube", ref cubePose, cube.Bounds);
            }));
            SK.Shutdown();
        }        
    }
}
