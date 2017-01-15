using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MatchFire : VRTK_InteractableObject {

    public GameObject fireParticles;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartFire()
    {
        fireParticles.SetActive(true);
    }

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
        StartFire();
    }
}
