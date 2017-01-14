using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMonitor : MonoBehaviour {

  // Monitor instance.
  public static PlayerMonitor instance;

  // Height that the monitor should float at, relative to player scale.
  [SerializeField] private float monitorHeight = 7.0f;

  [SerializeField] private float fadeTimes = 0.5f;

  // Player object, that will have scale associated with it.
  [SerializeField] private GameObject playerObject;

  [SerializeField] private float radiusFromPlayer = 10.0f;
  [SerializeField] private float viewAngle = 0.0f;

  private List<Vector3> floatPoints;

  private Text monitorText;

  private bool monitorVisible = false;

  void Awake() {

    if (PlayerMonitor.instance != null) {

      GameObject.Destroy(this.gameObject);

    }
    else {
  
      PlayerMonitor.instance = this;

    }
  }

  // Use this for initialization
  void Start() {

    if (playerObject == null) {
      // Store the main camera as the player POV.
      playerObject = Camera.main.gameObject;
    }

    monitorText = GetComponentInChildren<Text>();

    // Make sure that the monitor is at the desired height, relative to Camera scale.
    this.transform.position = new Vector3(this.transform.position.x, 
      monitorHeight * playerObject.transform.localScale.y, 
      this.transform.position.z);

    // Set the constraints of the monitor - it should stay at that height;z
    this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;

    // Create a new monitor Joinect object to connect the monitor to.
    GameObject monitorJointObject = new GameObject("MonitorJoint");
   
    // Set up the monitor joint under the player, so that the monitor follows the player.
    monitorJointObject.transform.parent = playerObject.transform;
    monitorJointObject.transform.localPosition = new Vector3(0.0f, 0.0f, radiusFromPlayer);
    monitorJointObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
    monitorJointObject.transform.localScale = Vector3.one;

    // Add the actual SpringJoint and configure it.
    SpringJoint monitorJoint = monitorJointObject.AddComponent<SpringJoint>();
    monitorJoint.connectedBody = this.GetComponent<Rigidbody>();
    monitorJoint.anchor = Vector3.zero;
    monitorJoint.autoConfigureConnectedAnchor = false;
    monitorJoint.connectedAnchor = Vector3.zero;
    monitorJoint.spring = 50.0f;
    monitorJoint.damper = 10.0f;

    // Ensure that Rigidbody values won't make the monitor act in strange ways.
    monitorJointObject.GetComponent<Rigidbody>().useGravity = false;
    monitorJointObject.GetComponent<Rigidbody>().isKinematic = true;

    SetVisible(false, false); 

  }
	
  // Update is called once per frame
  private void Update() {

    // Always have the monitor face the player.
    this.transform.LookAt(playerObject.transform);

    // Ensure that the monitor is always moving with the joint - not springing around.
    this.GetComponent<Rigidbody>().velocity = Vector3.zero;

    if (Input.GetKeyDown("g")) {

      SetText("Hello");

    }

    if (Input.GetKeyDown("h")) {

      SetText("Goodbye");

    }

    if (Input.GetKeyDown("j")) {


      SetVisible(false);

    }

    if (Input.GetKeyDown("k")) {

      SetVisible(true);
    
    }

  }

  public void SetText(string text) {

    StartCoroutine(ChangeText(text));

  }

  public void SetVisible(bool visible, bool shouldFade = true) {

    if (shouldFade) {
      StartCoroutine(Fade(visible, true, fadeTimes));
    }
    else {
      StartCoroutine(Fade(visible, true, 0.0f));
    }

    monitorVisible = visible;

  }

  public IEnumerator ChangeText(string text) {

    if (monitorVisible) {
      yield return StartCoroutine(Fade(false, false, fadeTimes));
    }

    monitorText.text = text;

    if (monitorVisible) {
      yield return StartCoroutine(Fade(true, false, fadeTimes));
    }
  }

  public IEnumerator Fade(bool fadeIn, bool fadeMonitor, float fadeTime) {

    MeshRenderer monitorRenderer = GetComponentInChildren<MeshRenderer>();
    Material monitorMaterial = monitorRenderer.material;

    Color startColor = monitorRenderer.material.color;
    Color endColor = startColor;

    Color startTextColor = monitorText.color;
    Color endTextColor = monitorText.color;

    if (fadeIn) {
      endColor.a = 1.0f;
      endTextColor.a = 1.0f;
    }
    else {
      endColor.a = 0.0f;
      endTextColor.a = 0.0f;
    }
     
    for (float i = 0; i < fadeTime; i += Time.deltaTime) {

      if (fadeMonitor) {
        Color newColor = Color.Lerp(startColor, endColor, i / fadeTime);
        monitorMaterial.SetColor("_Color", newColor);
      }

      Color textColor = Color.Lerp(startTextColor, endTextColor, i / fadeTime);
      monitorText.color = textColor;

      yield return null;

    }

    if (fadeMonitor) {

      monitorMaterial.SetColor("_Color", endColor);

    }

    monitorText.color = endTextColor;


  }
}
