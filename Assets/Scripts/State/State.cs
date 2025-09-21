using System;
using System.Collections.Generic;
using Factory;
using UnityEngine;

[Serializable]
public class State {
    public List<Province> provinces;
    public int stateId;
    
    public State(List<Province> provinces, int stateId) {
        this.provinces = provinces;
        this.stateId = stateId;
    }

    public GameObject GetGameObject() {
        return StateFactory.GenerateGameObject(this);
    }
}