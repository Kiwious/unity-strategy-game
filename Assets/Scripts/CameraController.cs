using System;
using Interface;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    [SerializeField]
    private int maxRaycastDistance = 3000;

    private Camera cam;
    
    private void Awake() {
        cam = GetComponent<Camera>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxRaycastDistance)) {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 2f);
            }
            else {
                Debug.DrawRay(ray.origin, ray.direction * maxRaycastDistance, Color.green, 2f);
            }

            GameObject hitCollider = hit.transform.gameObject;
            IFocusable hitFocus = hitCollider.GetComponent<IFocusable>();

            if (hitFocus != null) {
                hitFocus.Focus(hitCollider);
            }
            
            /*if (hitProvince != null) {
                hitProvince.Focus();
                Debug.Log($"Hit Province {hitProvince.GetProvinceData().Name}");
            }*/
        }
    }
}
