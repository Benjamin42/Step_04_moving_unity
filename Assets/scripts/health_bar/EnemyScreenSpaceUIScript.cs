using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScreenSpaceUIScript : MonoBehaviour {

	private EnemyScript enemyScript;

	public Canvas canvas;
	public GameObject healthPrefab;

	public float healthPanelOffset = 2f;
	private GameObject healthPanel;
	private Text enemyName;
	private Slider healthSlider;


	void Start () 
	{
		enemyScript = GetComponent<EnemyScript>();
		healthPanel = Instantiate(healthPrefab) as GameObject;
		healthPanel.transform.SetParent(canvas.transform, false);

		enemyName = healthPanel.GetComponentInChildren<Text>();
		enemyName.text = enemyScript.HP + "";

		healthSlider = healthPanel.GetComponentInChildren<Slider>();
	}

	void Update () 
	{
		if (Input.GetKey (KeyCode.L)) {
			enemyScript.HP -= 10;
			if (enemyScript.HP < 0)
				enemyScript.HP = 0;
		}
		if (Input.GetKey (KeyCode.M)) {
			enemyScript.HP += 10;
			if (enemyScript.HP > enemyScript.maxHP)
				enemyScript.HP = enemyScript.maxHP;
		}

		healthSlider.value = enemyScript.HP/(float)enemyScript.maxHP;
		enemyName.text = enemyScript.HP + "";

		Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
		Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
		healthPanel.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
	}
}
