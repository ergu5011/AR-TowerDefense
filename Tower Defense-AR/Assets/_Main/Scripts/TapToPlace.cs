using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using newInputTouch = UnityEngine.InputSystem.EnhancedTouch;

public class TapToPlace : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToPlace;

    private bool objectState = false;

    [SerializeField]
    private ARRaycastManager raycastManager;

    private static List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    private void Awake()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnEnable()
    {
        newInputTouch.Touch.onFingerDown += Touch_onFingerDown;
    }


    private void OnDisable()
    {
        newInputTouch.Touch.onFingerDown -= Touch_onFingerDown;
    }

    private void Touch_onFingerDown(Finger obj)
    {

        if (raycastManager.Raycast(obj.screenPosition, hitResults, TrackableType.PlaneWithinBounds) && objectState == false)
        {
            Pose pose = hitResults[0].pose;

            objectState = !objectState;

            objectToPlace.SetActive(objectState);

            objectToPlace.transform.position = pose.position;
            objectToPlace.transform.rotation = pose.rotation;
        }
    }

    public void stateToggle()
    {
        if (objectState == true)
        {
            objectState = !objectState;
            objectToPlace.SetActive(objectState);
        }
    }
}
