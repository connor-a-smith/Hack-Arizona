using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BeginScaleControl : MonoBehaviour {

    [SerializeField]
    private GameObject leftRadial;
    [SerializeField]
    private GameObject rightRadial;
    [SerializeField]
    private GameObject leftMenuPanel;
    [SerializeField]
    private GameObject rightMenuPanel;

    private bool hasScaled;

    void Start()
    {
        hasScaled = false;
    }

    public void SetCheckForHasScaled(bool status)
    {
        hasScaled = status;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            SetRadialPanelsActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            SetRadialPanelsActive(false);
        } 
    }

    void SetRadialPanelsActive(bool active)
    {
        if (!active && hasScaled)
        {
            leftMenuPanel.GetComponent<RadialMenu>().UnClickButton(351.6f);
            rightMenuPanel.GetComponent<RadialMenu>().UnClickButton(174.8f);
        }
        leftRadial.SetActive(active);
        rightRadial.SetActive(active);

        
    }
}
