using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

    //Pool arrays
    [SerializeField] Pool[] ghost;

    static Dictionary<GameObject, Pool> dictionary;

    void Awake() {

        dictionary = new Dictionary<GameObject, Pool>();

        //Initialize pools
        Initialize(ghost);

    }

    void OnDestroy() {
        //Check pool size when leave game mode
        CheckPoolSize(ghost);
    }

    void CheckPoolSize(Pool[] pools) { //Debug when runtime size larger than initial size

        foreach (var pool in pools) {

            if (pool.RuntimeSize > pool.Size) {

                Debug.LogWarning(string.Format("Pool: {0} has a runtime size {1} bigger than its initial size {2}!",
                    pool.Prefab.name, pool.RuntimeSize, pool.Size));
            }
        }
    }

    void Initialize(Pool[] pools) {

        foreach (var pool in pools) {

            dictionary.Add(pool.Prefab, pool);

            Transform poolParent = new GameObject("Pool: " + pool.Prefab.name).transform;

            poolParent.parent = transform;
            pool.Initialize(poolParent);

        }
    }

    public static GameObject Release(GameObject prefab) {

        if (!dictionary.ContainsKey(prefab)) {
            return null;
        }

        return dictionary[prefab].PreparedObject();

    }
    public static GameObject Release(GameObject prefab, Vector3 position) {

        if (!dictionary.ContainsKey(prefab)) {
            return null;
        }

        return dictionary[prefab].PreparedObject(position);

    }
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation) {

        if (!dictionary.ContainsKey(prefab)) {
            return null;
        }

        return dictionary[prefab].PreparedObject(position, rotation);

    }
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 localScale) {

        if (!dictionary.ContainsKey(prefab)) {
            return null;
        }

        return dictionary[prefab].PreparedObject(position, rotation, localScale);

    }

}