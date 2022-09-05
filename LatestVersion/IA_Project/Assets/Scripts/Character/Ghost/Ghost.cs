using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

    [MinMaxRange(0, 2)]
    [SerializeField] RangedFloat size;

    [SerializeField] RangedFloat rotationSpeed;
    [SerializeField] Vector3 zoomScale;

    [SerializeField] RangedFloat delayTime;

    Vector3 initialScale;
    Vector3 rotateDirection = Vector3.zero;

    [SerializeField] int score;

    public int Score => score;

    float t;
    bool zoomSwitcher;

    [Header("Effects")]

    [SerializeField] GameObject deathVFX;
    [SerializeField] SimpleSFX deathSFX;

    Vector3 Size => Vector3.one * Random.Range(size.minValue, size.maxValue);

    WaitForSeconds waitForDelay;

    void OnEnable() {

        //Random zoom scale
        initialScale = Size;
        transform.localScale = initialScale;

        //Random rotation
        transform.rotation = Random.rotation;

        //Intialize the rotate direction
        rotateDirection.x = Random.Range(0f, 1f);
        rotateDirection.y = Random.Range(0f, 1f);
        rotateDirection.z = Random.Range(0f, 1f);

        rotateDirection = rotateDirection.normalized * Random.Range(rotationSpeed.minValue, rotationSpeed.maxValue);

        waitForDelay = new WaitForSeconds(Random.Range(delayTime.minValue, delayTime.maxValue));

        StartCoroutine(nameof(RandomAnimation));

    }

    void Zoom(bool value) {
        if (value)
            t += Time.deltaTime;
        else
            t -= Time.deltaTime;
    }

    IEnumerator RandomAnimation() {

        yield return waitForDelay;

        t = 0;

        while (gameObject.activeSelf) {

            transform.Rotate(rotateDirection);

            if (t <= 0) {
                zoomSwitcher = true;
            } else if (t >= 1) {
                zoomSwitcher = false;
            }

            Zoom(zoomSwitcher);

            transform.localScale = Vector3.Lerp(initialScale * 0.9f, initialScale * 1.1f, t);

            yield return null;
        }
    }

    public void GetHit() {

        AudioManager.Instance.PlaySFX(deathSFX);
        PoolManager.Release(deathVFX, transform.position);

        gameObject.SetActive(false);

    }
}