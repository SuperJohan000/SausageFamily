using UnityEngine;


public abstract class ScriptableSingleton<T> : ScriptableObject where T : ScriptableSingleton<T>
{
    public static T Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = Resources.Load<T>("");

            if (_Instance == null)
                Debug.LogError("ScriptableSingleton instance is not found: " + nameof(T));

            return _Instance;
        }
    }

    private static T _Instance;
}
