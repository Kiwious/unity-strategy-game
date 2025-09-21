using Interface;
using UnityEngine;

public class ProvinceComponent : MonoBehaviour, IFocusable {
    public Province province;

    private LineRenderer lr;

    private void Start() {
        lr = GetComponent<LineRenderer>();
    }

    public void Focus(GameObject focus) {
        if (ProvinceManager.focusedProvince != gameObject && ProvinceManager.focusedProvince != null) {
            ProvinceManager.focusedProvince.gameObject.GetComponent<LineRenderer>().enabled = false;
        }
        
        ProvinceManager.focusedProvince = gameObject;
        lr.enabled = true;

        // focus the parent state
        StateComponent stateComponent = gameObject.GetComponentInParent<StateComponent>();

        if (stateComponent != null) {
            stateComponent.Focus(stateComponent.gameObject);
        }
    }
}
