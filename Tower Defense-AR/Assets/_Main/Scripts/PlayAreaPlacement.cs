using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
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

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (raycastManager.Raycast(ray, hitResults, TrackableType.PlaneWithinBounds) && objectState == false)
        {
            Pose pose = hitResults[0].pose;

            objectToPlace.SetActive(true);

            objectToPlace.transform.position = pose.position;
            objectToPlace.transform.rotation = pose.rotation;

            confirmPlacement.Invoke();
        }
    }

    public void stateToggle()
    {
        objectState = !objectState;
    }
}
