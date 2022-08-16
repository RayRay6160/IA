using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField] float mouseSenesitivity = 50f;

    [SerializeField] new Transform camera;

    float mouseX;
    float mouseY;

    float xRotation;

    #region Shoot

    [SerializeField] Image crosshair;

    [SerializeField] float range = 10f;
    [SerializeField] float reloadSpeed;

    bool allowShoot = true;

    RaycastHit hit;

    float t;

    #endregion

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        //StartCoroutine(nameof(CameraFollowMouseCoroutine));
    }

    void Update() {

        CameraFollowMouse();

        if (Input.GetButtonDown("Fire1") && allowShoot) {
            Shoot();
        }
    }

    void CameraFollowMouse() {
        //for (;;){
            mouseX = Input.GetAxis("Mouse X") * mouseSenesitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSenesitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90f);

            camera.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.Rotate(Vector3.up * mouseX);

            //yield return null;
        //}
    }

    void Shoot() {
        allowShoot = false;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range)) {
            Debug.Log(hit.transform.name);
        }
        StartCoroutine(nameof(Reload));
    }

    IEnumerator Reload() {
        t = 0;
        while (t < 1) {
            t += reloadSpeed * Time.deltaTime;
            crosshair.fillAmount = t;

            yield return null;
        }
        allowShoot = true;
    }
}