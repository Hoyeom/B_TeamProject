
    using UnityEngine;

    public class Util
    {
        public static T FindOrNewComponent<T>(out T component, string name) where T : Component
        {
            GameObject obj = GameObject.Find(name);

            if (obj == null)
                component = new GameObject(name).AddComponent<T>();
            else
                component = obj.GetComponent<T>();
            return component;
        }
    }
