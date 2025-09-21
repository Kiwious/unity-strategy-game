using Interface;
using UnityEngine;

public class StateComponent : MonoBehaviour, IFocusable {
    public State state;
    public void Focus(GameObject focus) {
        throw new System.NotImplementedException();
    }
}
