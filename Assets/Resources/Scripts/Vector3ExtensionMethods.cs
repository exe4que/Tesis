using UnityEngine;
using System.Collections;

namespace VectorExtensionMethods
{
	public static class Vector3ExtensionMethods
	{

		public static Vector2 XY (this Vector3 v)
		{
			return new Vector2 (v.x, v.y);
		}

		public static Vector2 YZ (this Vector3 v)
		{
			return new Vector2 (v.y, v.z);
		}

		public static Vector2 XZ (this Vector3 v)
		{
			return new Vector2 (v.x, v.z);
		}

		public static Vector2 ToVector2 (this Vector3 v)
		{
			return new Vector2 (v.x, v.y);
		}
	}

	public static class Vector2ExtensionMethods
	{

		public static Vector2 Invert (this Vector2 v)
		{
			return new Vector2 (v.y, v.x);
		}

		public static Vector3 ToVector3 (this Vector2 v)
		{
			return new Vector3 (v.x, v.y, 0);
		}
	}


}