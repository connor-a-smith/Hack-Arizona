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

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ontriggerenter\n");
        if (other.gameObject.CompareTag("MainCamera"))
        {
            Debug.Log("detected maincamera entering\n");
            SetRadialPanelsActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("oncollisionexit\n");
        if (other.gameObject.CompareTag("MainCamera"))
        {
            Debug.Log("detected maincamera exiting\n");
            SetRadialPanelsActive(false);
        } else
        {
            Debug.Log("the thing leaving is: " + other.gameObject.name);
        }
    }

    void SetRadialPanelsActive(bool active)
    {
        Debug.Log("settign radial panels " + active + "\n");
        if (!active)
        {
            leftMenuPanel.GetComponent<RadialMenu>().UnClickButton(351.6f);
            rightMenuPanel.GetComponent<RadialMenu>().UnClickButton(174.8f);
        }
        leftRadial.SetActive(active);
        rightRadial.SetActive(active);

        
    }
}
