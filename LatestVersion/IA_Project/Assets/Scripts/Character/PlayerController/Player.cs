using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    #region Component

    [SerializeField] new Transform camera;

    #endregion

    #region ComputerControl

    [SerializeField] float mouseSenesitivity = 50f;

    float mouseX;
    float mouseY;

    float xRotation;

    #endregion

    #region  Gyro

    Gyroscope gyroscope;

    Quaternion cameraRotation = new Quaternion(0, 0, 1, 0);

    bool gyroEnable;

    bool EnableGyro() { //whether user equipment support gyroscope

        if (SystemInfo.supportsGyroscope) {

            gyroscope = Input.gyro;
            gyroscope.enabled = true;

            transform.rotation = Quaternion.Euler(90, 90, 0);

            return true;
        }
        return false;
    }

    #endregion

    #region ShootParameter

    [SerializeField] Image crosshair;

    [SerializeField] GameObject projectile;

    [SerializeField] float reloadSpeed;

    bool allowShoot = true;

    RaycastHit hit;

    float t;

    #endregion

    void Awake() {
        gyroEnable = EnableGyro();
    }

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

    void CameraFollowMouse() { //For computer testing

        if (gyroEnable) {

            camera.localRotation = gyroscope.attitude * cameraRotation;

        } else {

            mouseX = Input.GetAxis("Mouse X") * mouseSenesitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSenesitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90f);

            camera.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.Rotate(Vector3.up * mouseX);

        }
    }

    #region Shoot

    void Shoot() {

        allowShoot = false;

        ProjectileManager.FireProjectile(projectile, transform.position, camera.rotation);

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

    #endregion

}