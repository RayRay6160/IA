using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] PlayerData playerData;

    new Rigidbody rigidbody;

    [SerializeField] float fireForce = 10;

    [SerializeField] float angle = 10;

    Quaternion rotation;
    Vector3 shootAngle;

    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnDisable() {
        rigidbody.velocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision other) {
        if (other.collider.gameObject.TryGetComponent<Ghost>(out Ghost ghost)) {

            ghost.GetHit();
            playerData.score += ghost.Score;

            gameObject.SetActive(false);

        }
    }

    public void Fire() {

        rotation = Quaternion.AngleAxis(angle * -1, transform.right);
        shootAngle = rotation * transform.forward;

        rigidbody.AddForce(shootAngle * fireForce, ForceMode.Impulse);

    }

}