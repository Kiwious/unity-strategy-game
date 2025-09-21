using System.Collections.Generic;
using UnityEngine;

namespace Factory {
    public static class StateFactory {
        
        public static State Create(List<Province> provinces, int id) {
            State state = new State(provinces, id);

            // stateGameObject = GenerateGameObject(state);
            return state;
        }

        public static GameObject GenerateGameObject(State state) {
            GameObject go = new GameObject($"State-{state.stateId}");

            StateComponent sc = go.AddComponent<StateComponent>();
            sc.state = state;
            
            return go;
        }
    }
}