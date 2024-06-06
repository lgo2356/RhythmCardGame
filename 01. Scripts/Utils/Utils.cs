using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static T FindChild<T>(GameObject parent, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (parent == null)
        {
            Debug.LogError($"Utils FindChild() - parent is null");
            return null;
        }

        if (recursive == false)  // 직속 자식만 찾기
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                Transform child = parent.transform.GetChild(i);

                if (string.IsNullOrEmpty(name) || child.name == name)
                {
                    T component = child.GetComponent<T>();

                    if (component != null) return component;
                }
            }
        }
        else  // 모든 자식(자식의 자식) 찾기
        {
            foreach (T component in parent.GetComponentsInChildren<T>(true))
            {
                if (string.IsNullOrEmpty(name) || component.name == name) return component;
            }
        }

        return null;
    }
}
