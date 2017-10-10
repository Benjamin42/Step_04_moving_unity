using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public string monsterName = "Bob the Enemy";
	public int maxHP = 100;
	public int HP;

	void Start () {
		HP = maxHP;	
	}

	void Update () {
		
	}
}
