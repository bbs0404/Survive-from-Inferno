using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour{

    private static T inst = null;

    public static T Inst()
    {
        if (inst == null)
        {
            inst = (T)FindObjectOfType(typeof(T));

            if (FindObjectsOfType(typeof(T)).Length > 1)
            {
                Debug.LogError("Multiple Singletons Exist");
            }
        }
        if (inst == null)
        {
            GameObject singleton = new GameObject();
            inst = singleton.AddComponent<T>();
            singleton.name = typeof(T).ToString();
            Debug.Log("Create new singleton : " + inst.gameObject.name);
        }
        else
        {
//            Debug.Log("Referencing existing singleton : " + inst.gameObject.name);
        }
        return inst;
    }

    private void Awake()
    {
        if (inst != null && inst != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void setStatic()
    {
        DontDestroyOnLoad(this);
    }
}
