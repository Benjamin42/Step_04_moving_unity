using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Material selectMat;

	private Animator anim;
	private Material oldMat;
	private bool selected;

	void Start () {
		anim = GetComponent<Animator>();
		oldMat = transform.Find ("Body_mesh").GetComponent<Renderer> ().material;
		selected = false;
	}

	public void Move(Vector3 targetPosition, float speed) {
		anim.SetBool("IsWalking", true);

		transform.LookAt(targetPosition);
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
		Debug.Log (transform.position.x + "/" + targetPosition.x + "  " + transform.position.z + "/" + targetPosition.z);
		if (transform.position.x == targetPosition.x && transform.position.z == targetPosition.z) {
			anim.SetBool("IsWalking", false);
		}
	}

	public void Select() {
		if (selected) {
			transform.Find ("Body_mesh").GetComponent<Renderer> ().material = oldMat;
		} else {
			transform.Find ("Body_mesh").GetComponent<Renderer> ().material = selectMat;
		}
		selected = !selected;
	}

}
