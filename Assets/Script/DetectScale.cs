using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectScale : MonoBehaviour {

    private Vector3 currScale;
    private Vector3 newScale;
    
    [SerializeField] private float scaleUpFactor = 1.05f;
    [SerializeField] private float scaleDownFactor = 0.95f;

    private float antSceneLimit = 0.1545715f;
    private float citySceneLimit = 42.40319f;

	// Use this for initialization
	void Start () {
        currScale = gameObject.transform.localScale;
	}

    // Use for Scaling Up
    public void ScaleUp()
    {
        newScale = currScale * (1 + (scaleUpFactor * Time.deltaTime));
        if (ScaleLimits.IsWithinScaleLimits(newScale))
        {
            gameObject.transform.localScale = newScale;
            currScale = newScale;
        }
    }
    
    // Use for Scaling Down
    public void ScaleDown()
    {
        newScale = currScale * (1 - (scaleDownFactor * Time.deltaTime));
        if (ScaleLimits.IsWithinScaleLimits(newScale))
        {
            gameObject.transform.localScale = newScale;
            currScale = newScale;
        }
        
    }
	
}
