using StereoKit;
using System;

namespace StereoKit_Hackathon
{
	static class UtilityPlus
	{
		public static Vec3 RoundToClosest(this Vec3 _vec3, float precision)
		{
			Vec3 vec3 = _vec3;
			vec3.x = MathF.Round(vec3.x / precision) * precision;
			vec3.y = MathF.Round(vec3.y / precision) * precision;
			vec3.z = MathF.Round(vec3.z / precision) * precision;
			return vec3;
		}
	}
}
