using Interface;
using UnityEngine;

public class ProvinceComponent : MonoBehaviour, IFocusable {
    public Province province;
    public void Focus(GameObject focus) {
        Debug.Log(province.GetDebugSummary());
    }
}
