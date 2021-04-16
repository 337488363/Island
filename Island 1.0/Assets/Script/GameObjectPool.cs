using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    /// <summary>
    /// GameObjectPool
    /// </summary>
    public class GameObjectPool : MonoSingleton<GameObjectPool>
	{
        private Dictionary<string, List<GameObject>> cache;

        protected override void Init()
        {
            base.Init();
            cache = new Dictionary<string, List<GameObject>>();
        }

        public GameObject CreateObject(string type, GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var obj = FindUsableObject(type);

            if (obj == null)
                obj = AddObject(type, prefab, position, rotation);

            UseObject(position, rotation, obj);
            return obj;
        }

        private void UseObject(Vector3 position, Quaternion rotation, GameObject obj)
        {
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);

            foreach(var item in obj.GetComponentsInChildren<IResetable>())
            {
                item.OnReset();
            }
        }

        private GameObject AddObject(string type, GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var obj = Instantiate(prefab, position, rotation);
            if (!cache.ContainsKey(type))
                cache.Add(type, new List<GameObject>());
            cache[type].Add(obj);
            return obj;
        }

        private GameObject FindUsableObject(string type)
        {
            GameObject obj = null;
            if (cache.ContainsKey(type))
                obj = cache[type].Find(go => !go.activeInHierarchy);
            return obj;
        }

        public void CollectObject(GameObject go ,float delayTime = 0)
        {
            StartCoroutine(CollectObjectDelay(go,delayTime));
        }

        private IEnumerator CollectObjectDelay(GameObject go,float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            if(go != null)
                go.SetActive(false);
        }

        public void Clear(string type)
        {
            for(var i = 0; i < cache[type].Count; i++)
            {
                Destroy(cache[type][i]);
            }
            cache.Remove(type);
        }

        public void ClearAll()
        {
            var keys = new List<string>(cache.Keys) ;
            foreach (var item in keys)
            {
                Clear(item);
            }
        }
	}

    public interface IResetable
    {
        void OnReset();
    }
}
