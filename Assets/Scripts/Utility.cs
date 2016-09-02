using UnityEngine;

public class Utility : MonoBehaviour
{
    private static T Instantiate<T>(Object original) where T : class
    {
        return Instantiate(original) as T;
    }

    private static T Instantiate<T>(string prefabPath) where T : class
    {
        //リソースからプレハブを取得
        var obj = Resources.Load(prefabPath);
        return obj == null ? null : Instantiate<T>(obj);
    }

    public static GameObject Instantiate(GameObject parent, GameObject prefab)
    {
        var obj = Instantiate<GameObject>(prefab);
        if (obj != null && parent != null)
        {
            obj.transform.parent = parent.transform;
        }

        return obj;
    }

    public static GameObject Instantiate(GameObject parent, string prefabPath)
    {
        var obj = Instantiate<GameObject>(prefabPath);
        if (obj != null && parent != null)
        {
            obj.transform.parent = parent.transform;
        }

        return obj;
    }

    public static T InstantiateGetComponent<T>(GameObject parent, GameObject prefab)
    {
        var obj = Instantiate(parent, prefab);

        return obj.GetComponent<T>();
    }

    public static T InstantiateGetComponent<T>(GameObject parent, string prefabPath)
    {
        var obj = Instantiate(parent, prefabPath);

        return obj.GetComponent<T>();
    }
}
