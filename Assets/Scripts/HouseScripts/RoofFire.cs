using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class RoofFire : MonoBehaviour {

    public float roofBurnTime = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<MatchFire>())
        {
            StartCoroutine(SetRoofOnFire());


        }
    }

    private IEnumerator SetRoofOnFire()
    {

        //Set all fires active.
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        //Wait for a few seconds, then destroy the roof.
        yield return new WaitForSeconds(roofBurnTime);
        GameObject.Destroy(this.gameObject);
    }
}
