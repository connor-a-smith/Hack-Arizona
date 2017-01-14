using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectScale : MonoBehaviour {

    private Vector3 currScale;
    private Vector3 newScale;

    [SerializeField] private float scaleUpFactor = 1.05f;
    [SerializeField] private float scaleDownFactor = 0.95f;

	// Use this for initialization
	void Start () {
        currScale = gameObject.transform.localScale;
	}

    // Use for Scaling Up
    public void ScaleUp()
    {
        newScale = currScale * (1 + (scaleUpFactor * Time.deltaTime));
        gameObject.transform.localScale = newScale;
        currScale = newScale;
    }
    
    // Use for Scaling Down
    public void ScaleDown()
    {
        newScale = currScale * (1 - (scaleDownFactor * Time.deltaTime));
        gameObject.transform.localScale = newScale;
        currScale = newScale;
    }
	
	// Update is called once per frame
	void Update () {
        /*
		if (Input.GetKey(KeyCode.UpArrow))
        {
            //scale up
            Debug.Log("Up Scale\n");
            newScale = currScale * (1 + (scaleUpFactor * Time.deltaTime));
            gameObject.transform.localScale = newScale;
            currScale = newScale;
        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            //scale down
            Debug.Log("Down Scale\n");
            newScale = currScale * (1 - (scaleDownFactor * Time.deltaTime));
            gameObject.transform.localScale = newScale;
            currScale = newScale;
        }
        */
	}
}
