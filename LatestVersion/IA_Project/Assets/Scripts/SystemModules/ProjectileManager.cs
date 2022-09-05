using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour {

    [SerializeField] ProjectilePool[] playerProjectilePools;

    static Dictionary<GameObject, ProjectilePool> dictionary;

    void Awake() {

        dictionary = new Dictionary<GameObject, ProjectilePool>();

        Initialize(playerProjectilePools);
    }

    void OnDestroy() {
        CheckPoolSize(playerProjectilePools);
    }

    void CheckPoolSize(ProjectilePool[] pools) {

        foreach (var pool in pools) {

            if (pool.RuntimeSize > pool.Size) {

                Debug.LogWarning(string.Format("Pool: {0} has a runtime size {1} bigger than its initial size {2}!",
                    pool.Projectile.name, pool.RuntimeSize, pool.Size));
            }
        }
    }

    void Initialize(ProjectilePool[] pools) {

        foreach (var pool in pools) {

            dictionary.Add(pool.Projectile, pool);

            Transform poolParent = new GameObject("Pool: " + pool.Projectile.name).transform;

            poolParent.parent = transform;
            pool.Initialize(poolParent);

        }
    }

    public static GameObject FireProjectile(GameObject projectile, Vector3 position, Quaternion rotation) {

        if (!dictionary.ContainsKey(projectile)) {
            return null;
        }

        return dictionary[projectile].PreparedProjectile(position, rotation);

    }
}