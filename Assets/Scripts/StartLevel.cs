using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour {

	// Use this for initialization
	void Awake () {

        Instantiate(Resources.Load(PlayerPrefs.GetString("nowLevel")));
	}
	
	
}
