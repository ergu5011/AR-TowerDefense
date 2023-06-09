using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using newInputTouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private LayerMask enemyLayer;

    [SerializeField]
    private ParticleSystem spellParticle;

    [SerializeField]
    private GameObject attackPos;

    [SerializeField]
    private AudioSource audio;

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
        // Get player tap position
        Vector3 screenPos = new Vector3(obj.screenPosition.x, obj.screenPosition.y, Camera.main.nearClipPlane);

        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        // Deal damage if target is enemy
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, enemyLayer))
        {
            attackPos.transform.position = hitInfo.collider.transform.position;
            spellParticle.Play();

            audio.PlayOneShot(audio.clip);

            IDamageable damageable = hitInfo.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(1);
            }
        }
    }
}
