using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    ///  MonoSingleton
    /// </summary>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance != null) return instance;
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    new GameObject("MonoSingleton of " + typeof(T).Name).AddComponent<T>();
                }
                else
                {
                    instance.Init();
                }
                return instance;
            }
        }

        protected void Awake()
        {
            if (instance != null) return;
            instance = this as T;
            Init();
        }

        protected  virtual void Init()
        { 
        } 
    }
}
