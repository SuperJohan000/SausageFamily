using UnityEngine;


public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = FindObjectOfType<T>();

            if (_Instance == null)
                Debug.LogError("Singleton instance is not found: " + nameof(T));

            return _Instance;
        }
    }

    private static T _Instance;
}
