using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Util
{
	public static class AddElements
	{
		#region Trails
		public TrailRenderer FireTrail;
		public TrailRenderer EarthTrail;
		public TrailRenderer ThunderTrail;
		public TrailRenderer[] TrailRends = {FireTrail, EarthTrail, ThunderTrail};
		#endregion

		#region Materials
		public Material FireMat;
		public Material EarthMat;
		public Material ThunderMat;
		public Material[] Mats = {FireMat, EarthMat, ThunderMat};
		#endregion

		/// <summary>
		/// Adds an Elements visual effects to a Gameobject by enum
		/// </summary>
		/// <param name="obj">Object which needs element effects added.</param>
		/// <param name="element">Element added to the game object.</param>
		public static void AddElementByEnum(GameObject obj, Enums.Element element, bool replaceMat) {
			GameObject trail = Instantiate("Resources/prefabs/trail", obj.transform.position, obj.transform.rotation);
			trail.GetComponent<TrailRenderer>() = TrailRends[element];

			if (replaceMat) obj.GetComponent<Material>() = Mats[element];
		}
	}
}
