using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Util
{
	public class AddElements : MonoBehaviour
	{
		#region Trails
		public TrailRenderer[] TrailRends;
		#endregion

		#region Materials
		public Material[] Mats;
		#endregion

		/// <summary>
		/// Adds an Elements visual effects to a Gameobject by enum
		/// </summary>
		/// <param name="obj">Object which needs element effects added.</param>
		/// <param name="element">Element added to the game object.</param>
		public void AddElementByEnum(GameObject obj, Enums.Element element, bool replaceMat) {
			//GameObject trail = Instantiate("Resources/prefabs/trail", obj.transform.position, obj.transform.rotation);
			//trail.GetComponent<TrailRenderer>() = TrailRends[element];

			//if (replaceMat) obj.GetComponent<Material>() = Mats[element];
		}
	}
}
