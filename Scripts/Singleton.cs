using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T m_Instance;
    public static bool m_isQuitting;

    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                // Checking to make sure there's no other instance of this in memory
                m_Instance = FindObjectOfType<T>();

                if (m_Instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    m_Instance = obj.AddComponent<T>();
                }
            }

            return m_Instance;
        }
    }

    // Virtual Awake that can be overriden in a derived class
    public virtual void Awake()
    {
        if (m_Instance == null)
        {
            // if null, this instance is now the singleton
            m_Instance = this as T;

            // Make sure it will persist across every scene in memory
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // Destroy current instance because it must be a duplicate
            Destroy(gameObject);
        }
    }

}