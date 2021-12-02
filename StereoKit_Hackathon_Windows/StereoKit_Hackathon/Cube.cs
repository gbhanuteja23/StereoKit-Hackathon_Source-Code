using StereoKit;
using System;

namespace StereoKit_Hackathon
{
    public class Cube
    {
        Pose cubePose;
        public Model cubeModel;
        public Material cubeMat; 

		public Vec3 GetPos() => cubePose.position;
		public Pose GetPose() => cubePose;

		public Cube()
		{
            cubePose = new Pose(0, 0, -0.5f, Quat.Identity);

            cubeMat = Default.Material;
            cubeModel = Model.FromMesh(
                Mesh.GenerateCube(Vec3.One * MeshManager.cubeSize),
                cubeMat);
           
        }

        public Cube(Vec3 pos)
        {
            cubePose = new Pose(pos, Quat.Identity);

            cubeMat = Default.Material;
            cubeModel = Model.FromMesh(
                Mesh.GenerateCube(Vec3.One * MeshManager.cubeSize),
                cubeMat);            
        }

        public void ChangeMaterialColor()
        {
            Random random = new Random();
            cubeMat.SetColor("color", Color.HSV(new Vec3((float)random.Next() / int.MaxValue, 0.8f, 1f)));
        }

    }
}
