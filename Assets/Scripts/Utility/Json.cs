using Newtonsoft.Json;
using UnityEngine;

public static class Json {
    public static T Load<T>(string path) {
        TextAsset asset = Resources.Load<TextAsset>(path);
        return JsonConvert.DeserializeObject<T>(asset.text);
    }
}
