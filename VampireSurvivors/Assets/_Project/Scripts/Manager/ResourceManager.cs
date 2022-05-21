
    using UnityEngine;

    public class ResourceManager
    {
        public T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>($"Prefabs/{path}");
        }

        public T[] LoadAll<T>(string path) where T : Object
        {
            return Resources.LoadAll<T>($"Prefabs/{path}");
        }

        public GameObject Instantiate(string path)
        {
            GameObject obj = Resources.Load<GameObject>($"Prefabs/{path}");
            return Object.Instantiate(obj);
        }
    }
