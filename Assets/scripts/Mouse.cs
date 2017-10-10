using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
	PlayerController selected;

	Vector3 targetPosition;
	void Start() {
		selected = null;
	}

	void Update () {
		if (selected != null) {
			if (Input.GetMouseButton (GameConstants.RIGHT_MOUSE_BUTTON)) {
				//UpdatePosition ();
				UpdatePositionNew();
			}
			selected.Move (targetPosition, 2f);
		}

		if (Input.GetMouseButtonDown (GameConstants.LEFT_MOUSE_BUTTON)) {
			RaycastHit hitInfo = new RaycastHit();

			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit) {
				GameObject obj = hitInfo.transform.gameObject;
				if (obj.tag == "Player") {
					PlayerController myComponent = obj.GetComponent<PlayerController> ();
					myComponent.Select ();
					if (selected != null) {
						selected.Select ();
					}
					selected = myComponent;
					targetPosition = selected.transform.position;
				}
			}

		}

		Debug.DrawLine (targetPosition, new Vector3(targetPosition.x, 100, targetPosition.y), Color.red);

	}

	void UpdatePositionNew() {
		RaycastHit hitInfo = new RaycastHit();

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		Collider c = Terrain.activeTerrain.GetComponent<Collider> ();
		if (c.Raycast(ray, out hitInfo, Mathf.Infinity)) {
			targetPosition = hitInfo.point;
		}
	}

	void UpdatePosition() {
		Plane plane = new Plane(Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hitInfo = new RaycastHit();
		bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

		float point = 0f;
		if (plane.Raycast (ray, out point)) {
			targetPosition = ray.GetPoint (point);
			targetPosition.y = 0;
		}

	}
}

