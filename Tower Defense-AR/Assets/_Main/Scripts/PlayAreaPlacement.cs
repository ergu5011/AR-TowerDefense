using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using newInputTouch = UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Events;

public class PlayAreaPlacement : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToPlace;

    private bool objectState = false;

    [SerializeField]
    private ARRaycastManager raycastManager;

    private static List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    [SerializeField]
    private UnityEvent confirmPlacement;

    private void Awake()
    {
        EnhancedTouchSupport.Enable();
    }

    /*
    private void OnEnable()
    {
        newInputTouch.Touch.onFingerDown += Touch_onFingerDown;
    }

    private void OnDisable()
    {
        newInputTouch.Touch.onFingerDown -= Touch_onFingerDown;
    }
    */

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (raycastManager.Raycast(ray, hitResults, TrackableType.PlaneWithinBounds) && objectState == false)
        {
            Pose pose = hitResults[0].pose;

            //objectState = !objectState;

            objectToPlace.SetActive(true);

            objectToPlace.transform.position = pose.position;
            objectToPlace.transform.rotation = pose.rotation;

            confirmPlacement.Invoke();
        }
    }

    /*
    private void Touch_onFingerDown(Finger obj)
    {
        // Place play area on plane
        if (raycastManager.Raycast(obj.screenPosition, hitResults, TrackableType.PlaneWithinBounds) && objectState == false)
        {
            Pose pose = hitResults[0].pose;

            //objectState = !objectState;

            objectToPlace.SetActive(true);

            objectToPlace.transform.position = pose.position;
            objectToPlace.transform.rotation = pose.rotation;

            confirmPlacement.Invoke();
        }
    }   
    */

    public void stateToggle()
    {
        objectState = !objectState;
    }
}
