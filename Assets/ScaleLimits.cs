using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLimits : MonoBehaviour {

  public static ScaleLimits instance;

  public static KeyValuePair<float, string> ROOM_MIN_SCALE;
  public static KeyValuePair<float, string> ROOM_MAX_SCALE;
  public static KeyValuePair<float, string> CITY_MAX_SCALE;
  public static KeyValuePair<float, string> GLOBAL_MAX_SCALE;

  public static KeyValuePair<float, string> ATOMIC_MIN_SCALE;

  public static KeyValuePair<float, string> currentActiveMax;
  public static KeyValuePair<float, string> currentActiveMin;


  public void Awake() {

    if (instance != null) {

      GameObject.Destroy(this.gameObject);

    }
    else {

      instance = this;

    }

    // Key value pairs with the scale limits and their corresponding monitor text hints.
    ROOM_MIN_SCALE = new KeyValuePair<float, string>(0.1f, "The spider is blocking my way.");
    ROOM_MAX_SCALE = new KeyValuePair<float, string>(20.0f, "The roof is blocking my way.");
    CITY_MAX_SCALE = new KeyValuePair<float, string>(0.0f, "");
    GLOBAL_MAX_SCALE = new KeyValuePair<float, string>(0.0f, "");
    ATOMIC_MIN_SCALE = new KeyValuePair<float, string>(0.0f, "");

  }

  public void Start() {

    currentActiveMax = ROOM_MAX_SCALE;
    currentActiveMin = ROOM_MIN_SCALE;

  }

  public static bool IsWithinScaleLimits(float uniformScaleVal, bool shouldTriggerMonitor = true) {

    if (uniformScaleVal > currentActiveMin.Key && uniformScaleVal < currentActiveMax.Key) {

      return true;

    }

    if (shouldTriggerMonitor) {

      if (uniformScaleVal < currentActiveMin.Key) {

        instance.StartCoroutine(ActivateMonitor(currentActiveMin.Value));

      }
      else if (uniformScaleVal > currentActiveMax.Key) {

        instance.StartCoroutine(ActivateMonitor(currentActiveMax.Value));

      }
    }

    return false;

  }

  public static bool IsWithinScaleLimits(Vector3 localScale, bool shouldTriggerMonitor = true) {

    return IsWithinScaleLimits(localScale.x, shouldTriggerMonitor);

  }

  public static void IncreaseMaxScaleLimit() {

    if (currentActiveMax.Key == ROOM_MAX_SCALE.Key) {

      currentActiveMax = CITY_MAX_SCALE;
     
    }
    else if (currentActiveMax.Key == CITY_MAX_SCALE.Key) {

      currentActiveMax = GLOBAL_MAX_SCALE;

    }
  }

  public static void DecreaseMinScaleLimit() {

    if (currentActiveMin.Key == ROOM_MIN_SCALE.Key) {

      currentActiveMin = ATOMIC_MIN_SCALE;

    }
  }

  public static IEnumerator ActivateMonitor(string monitorText) {

    PlayerMonitor.instance.SetText(monitorText);
    PlayerMonitor.instance.SetVisible(true);

    yield return new WaitForSeconds(5.0f);

    PlayerMonitor.instance.SetVisible(false);

  }
}
