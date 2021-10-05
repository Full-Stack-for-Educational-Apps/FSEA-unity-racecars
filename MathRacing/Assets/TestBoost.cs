using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoost : MonoBehaviour
{
	// This basically finds the RaceController.cs class and assigns it to "rc"
	RaceController rc;
	void Awake() {
		GameObject racer = GameObject.Find("RaceController");
    	rc = racer.GetComponent<RaceController>();
	}

	// Boost the car on mouse click (RaceController.cs)
	void OnMouseDown() {
        rc.playerCar.Boost(rc.carBaseBoost);
 	}
}
