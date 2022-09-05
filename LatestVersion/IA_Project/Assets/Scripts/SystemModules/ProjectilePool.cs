using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectilePool {

    [SerializeField] GameObject prefab;

    [SerializeField] int size = 1;

    Queue<GameObject> queue;

    Dictionary<GameObject, Projectile> dictionary;

    Transform parent;
    public GameObject Projectile => prefab;
    public int Size => size;
    public int RuntimeSize => queue.Count;

    public void Initialize(Transform parent) {

        queue = new Queue<GameObject>();
        dictionary = new Dictionary<GameObject, Projectile>();

        this.parent = parent;

        for (var i = 0; i < size; i++) {

            queue.Enqueue(Copy());

        }

    }

    GameObject Copy() { //Instantiate projectile prefabs

        var copy = GameObject.Instantiate(prefab, parent);

        dictionary.Add(copy, copy.GetComponent<Projectile>());

        copy.SetActive(false);

        return copy;

    }

    GameObject AvailableProjectile() { //Return available projectile

        GameObject availableObject;

        if (queue.Count > 0 && !queue.Peek().activeSelf) {

            availableObject = queue.Dequeue();

        } else {

            availableObject = Copy();

        }

        queue.Enqueue(availableObject);

        return availableObject;
    }

    public GameObject PreparedProjectile(Vector3 position, Quaternion rotation) {

        GameObject preparedProjectile = AvailableProjectile();

        preparedProjectile.SetActive(true);
        preparedProjectile.transform.position = position;
        preparedProjectile.transform.rotation = rotation;

        dictionary[preparedProjectile].Fire();

        return preparedProjectile;

    }
}