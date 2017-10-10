using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCamera : MonoBehaviour {

	#region structs

	public struct BoxLimit {
		public float leftLimit;
		public float rightLimit;
		public float topLimit;
		public float bottomLimit;
	}

	#endregion

	public static BoxLimit cameraLimits = new BoxLimit();
	public static BoxLimit mouseScrollLimits = new BoxLimit();
	public static WorldCamera instance;

	private float cameraMoveSpeed = 60f;
	private float shiftBonus = 45f;
	private float mouseBoundary = 25f;

	void Awake() {
		instance = this;
	}

	void Start() {
		cameraLimits.leftLimit = 10.0f;
		cameraLimits.rightLimit = 240.0f;
		cameraLimits.topLimit = 204.0f;
		cameraLimits.bottomLimit = -20.0f;

		mouseScrollLimits.leftLimit = mouseBoundary;
		mouseScrollLimits.rightLimit = mouseBoundary;
		mouseScrollLimits.topLimit = mouseBoundary;
		mouseScrollLimits.bottomLimit = mouseBoundary;
	}

	public void Update() {
		if (CheckIfUserCameraInput ()) {
			Vector3 cameraDesiredMove = GetDesiredTranslation ();
			if (isDesiredPositionOverBoundaries (cameraDesiredMove)) {
				this.transform.Translate (cameraDesiredMove);
			}
		
		}
	}

	public bool CheckIfUserCameraInput() {
		bool keyboardMove;
		bool mouseMove;
		bool canMove;

		keyboardMove = WorldCamera.AreCameraKeyboardButtonPressed ();
		mouseMove = WorldCamera.IsMousePositionWithinBoundaries ();

		return keyboardMove || mouseMove;
	}

	public Vector3 GetDesiredTranslation() {
		float moveSpeed = 0f;
		float desiredX = 0f;
		float desiredZ = 0f;

		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
			moveSpeed = (cameraMoveSpeed + shiftBonus) * Time.deltaTime;
		else
			moveSpeed = cameraMoveSpeed * Time.deltaTime;

		if (Input.GetKey (KeyCode.Z))
			desiredZ = moveSpeed;
		if (Input.GetKey (KeyCode.S))
			desiredZ = moveSpeed * -1;
		if (Input.GetKey (KeyCode.Q))
			desiredX = moveSpeed * -1;
		if (Input.GetKey (KeyCode.D))
			desiredX = moveSpeed;

		if (Input.mousePosition.x < mouseScrollLimits.leftLimit)
			desiredX = moveSpeed * -1;
		if (Input.mousePosition.x > (Screen.width - mouseScrollLimits.rightLimit))
			desiredX = moveSpeed;
		
		if (Input.mousePosition.y < mouseScrollLimits.bottomLimit)
			desiredZ = moveSpeed * -1;
		if (Input.mousePosition.y > (Screen.height - mouseScrollLimits.topLimit))
			desiredZ = moveSpeed;

		return new Vector3 (desiredX, 0, desiredZ);
	}

	public bool isDesiredPositionOverBoundaries(Vector3 desiredPosition) {
		bool overBoudaries = false;
		if ((this.transform.position.x + desiredPosition.x) < cameraLimits.leftLimit)
			overBoudaries = true;
		if ((this.transform.position.x + desiredPosition.x) > cameraLimits.rightLimit)
			overBoudaries = true;
		if ((this.transform.position.z + desiredPosition.z) < cameraLimits.bottomLimit)
			overBoudaries = true;
		if ((this.transform.position.z + desiredPosition.z) < cameraLimits.topLimit)
			overBoudaries = true;
		return overBoudaries;
	}


	#region Helpers functions

	public static bool AreCameraKeyboardButtonPressed() {
		return Input.GetKey (KeyCode.Z) || Input.GetKey (KeyCode.Q) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D);
	}

	public static bool IsMousePositionWithinBoundaries() {
		return (Input.mousePosition.x < mouseScrollLimits.leftLimit && Input.mousePosition.x > -5) ||
			(Input.mousePosition.x > (Screen.width - mouseScrollLimits.rightLimit) && Input.mousePosition.x < (Screen.width + 5)) ||
			(Input.mousePosition.y < mouseScrollLimits.bottomLimit && Input.mousePosition.y > -5) ||
			(Input.mousePosition.y > (Screen.height - mouseScrollLimits.topLimit) && Input.mousePosition.y < (Screen.height + 5));
	}

	#endregion
}
