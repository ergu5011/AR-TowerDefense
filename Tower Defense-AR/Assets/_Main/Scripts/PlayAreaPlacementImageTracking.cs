using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Events;

public class PlayAreaPlacementImageTracking : MonoBehaviour
{
    private GameObject playArea;

    void Start()
    {
        playArea = GameObject.FindGameObjectWithTag("PlayArea");

        playArea.SetActive(true);
    }

    void Update()
    {
        playArea.transform.position = gameObject.transform.position;
    }
}
