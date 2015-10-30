using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Util
{
	public static class AddElements
	{
		#region Materials
		public static Material FireMat;
		public static Material EarthMat;
		public static Material ThunderMat;
		public static Material[] Mats = {FireMat, EarthMat, ThunderMat};
		#endregion
		
		/// <summary>
		/// Adds an Elements visual effects to a Gameobject by enum
		/// </summary>
		/// <param name="obj">Object which needs element effects added.</param>
		/// <param name="element">Element added to the game object.</param>
		public static void AddElementByEnum(GameObject obj, Enums.Element element, bool replaceMat) {
			//GameObject trail = GameObject.Instantiate(Resources.Load(element + "trail"), obj.transform.position, obj.transform.rotation) as GameObject;
			
			//if (replaceMat) obj.GetComponent<Renderer>().material = Mats[(int)element];
		}
	}
}
