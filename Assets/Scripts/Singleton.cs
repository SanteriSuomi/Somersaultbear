using UnityEngine;

namespace Somersaultbear
{
    public abstract class SingletonBase : MonoBehaviour
    {
        protected static bool ApplicationIsQuitting { get; set; }
    }

    public abstract class Singleton<T> : SingletonBase where T : Component
    {
        private static T BaseInstance { get; set; }
        public static T Instance
        {
            get
            {
                if (ApplicationIsQuitting) { return null; }
                else if (ValidateInstance()) { return BaseInstance; }
                else
                {
                    LogInitializationError();
                    return null;
                }
            }
            protected set
            {
                BaseInstance = value;

                #if UNITY_EDITOR
                Debug.LogWarning("Something is setting the instance of this singleton. Is this a wanted behaviour?");
                #endif
            }
        }

        protected virtual void Awake()
        {
            if (!ValidateInstance()) { LogInitializationError(); }
        }

        private static void LogInitializationError()
            => Debug.LogError($"{typeof(T).Name} error, instance initialization has most likely failed.");

        private static bool ValidateInstance()
        {
            if (BaseInstance != null) { return true; }

            T[] instances = FindObjectsOfType<T>();
            if (instances.Length <= 0)
            {
                GameObject typeGameObject = new GameObject($"{typeof(T).Name}");
                ActivateInstance(typeGameObject.AddComponent<T>());
            }
            else if (instances.Length == 1)
            {
                ActivateInstance(instances[0]);
            }
            else if (instances.Length >= 2)
            {
                for (int i = 1; i < instances.Length; i++)
                {
                    Destroy(instances[i].gameObject);
                }

                ActivateInstance(instances[0]);
            }
            else if (BaseInstance is null)
            {
                return false;
            }

            return true;
        }

        private static void ActivateInstance(T newInstance)
        {
            BaseInstance = newInstance;
            newInstance.gameObject.SetActive(true);
            DontDestroyOnLoad(newInstance.gameObject);
        }

        private static void OnDestroy() => ApplicationIsQuitting = true;

        private static void OnApplicationQuit() => ApplicationIsQuitting = true;

        #if UNITY_ANDROID || UNITY_IOS
        protected virtual void OnApplicationPause(bool hasPaused)
        {
            if (hasPaused) { ApplicationIsQuitting = true; }
            else { ApplicationIsQuitting = false; }
        }
        #endif
    }
}