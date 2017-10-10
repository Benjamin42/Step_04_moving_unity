using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePoint : MonoBehaviour
{
	RaycastHit hit;

	public GameObject target;

	void Update() {
		
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {

			if (hit.collider.name == "Terrain") {

				if (Input.GetMouseButtonDown (GameConstants.RIGHT_MOUSE_BUTTON)) {
					GameObject TargetObj = Instantiate (target, hit.point, Quaternion.identity) as GameObject;
					TargetObj.name = "Target Instantiated"; 
				
				}
			}
		}

	}
}


