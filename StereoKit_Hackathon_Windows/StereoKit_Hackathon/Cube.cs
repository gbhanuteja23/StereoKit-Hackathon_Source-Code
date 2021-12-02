using StereoKit; 

namespace StereoKit_Hackathon
{
    public class Cube
    {
        Pose cubePose;
        public Model cubeModel;

		public Vec3 GetPos() => cubePose.position;
		public Pose GetPose() => cubePose;

		public Cube()
		{
            cubePose = new Pose(0, 0, -0.5f, Quat.Identity);
            cubeModel = Model.FromMesh(
                Mesh.GenerateRoundedCube(Vec3.One * 0.1f, 0.02f),
                MeshManager.cubeMaterial);
        }
        public Cube(Vec3 pos)
        {
            cubePose = new Pose(pos, Quat.Identity);
            cubeModel = Model.FromMesh(
                Mesh.GenerateRoundedCube(Vec3.One * 0.1f, 0.02f),
                MeshManager.cubeMaterial);
        }
    }
}
