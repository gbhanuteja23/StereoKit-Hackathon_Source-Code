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


            // Create assets used by the app
            //cube
            Material cubeMaterial = Default.MaterialUI;
            cubeMaterial.SetColor("color", Color.Black);

            Pose cubePose = new Pose(0, 0, -0.5f, Quat.Identity);
            Model cube = Model.FromMesh(
                Mesh.GenerateRoundedCube(Vec3.One * 0.1f, 0.02f),
                cubeMaterial);


            Model cylinder = Model.FromMesh(Mesh.GenerateCylinder(0.1f, 0.1f, Vec3.Up), cubeMaterial);

            Matrix floorTransform = Matrix.TS(0, -1.5f, 0, new Vec3(30, 0.1f, 30));



            //floor
            Material floorMaterial = new Material(Shader.FromFile("floor.hlsl"));
            floorMaterial.Transparency = Transparency.Blend;


            // Core application loop
            while (SK.Step(() =>
            {
                if (SK.System.displayType == Display.Opaque)
                    Default.MeshCube.Draw(floorMaterial, floorTransform);

                UI.Handle("Cube", ref cubePose, cube.Bounds);
                cube.Draw(cubePose.ToMatrix());
                
                cylinder.Draw(cubePose.ToMatrix());
            })) ;
            SK.Shutdown();
        }
    }
}
