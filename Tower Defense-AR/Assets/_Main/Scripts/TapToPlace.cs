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

    [SerializeField]
    private LayerMask enemyLayer; // attack

    private float damageDealt = 1; // attack

    [SerializeField]
    private ParticleSystem spellParticle;

    [SerializeField]
    private GameObject attackPos;

    private AudioSource audio;

    private void Awake()
    {
        EnhancedTouchSupport.Enable();

        audio = GetComponent<AudioSource>();
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
        // Place play area on plane
        if (raycastManager.Raycast(obj.screenPosition, hitResults, TrackableType.PlaneWithinBounds) && objectState == false)
        {
            Pose pose = hitResults[0].pose;

            objectState = !objectState;

            objectToPlace.SetActive(objectState);

            objectToPlace.transform.position = pose.position;
            objectToPlace.transform.rotation = pose.rotation;
        }

        // Get player tap position
        Vector3 screenPos = new Vector3(obj.screenPosition.x, obj.screenPosition.y, Camera.main.nearClipPlane);

        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        // Deal damage if target is enemy
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, enemyLayer))
        {
            //Destroy(hitInfo.transform.gameObject);

            attackPos.transform.position = hitInfo.collider.transform.position;
            spellParticle.Play();

            audio.PlayOneShot(audio.clip);

            IDamageable damageable = hitInfo.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(damageDealt);
            }
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
