using System.Collections.Generic;
using UnityEngine;

namespace Factory {
    public static class StateFactory {
        private static int _nextStateId;
        
        public static GameObject Create(List<Province> provinces) {
            State state = new State(provinces, _nextStateId);
            _nextStateId++;

            GameObject stateGameObject = GenerateGameObject(state);
            return stateGameObject;
        }

        public static GameObject GenerateGameObject(State state) {
            GameObject go = new GameObject();

            StateComponent sc = go.AddComponent<StateComponent>();
            sc.state = state;
            
            return go;
        }
    }
}