using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

    [MinMaxRange(0, 2)]
    [SerializeField] RangedFloat size;

    [SerializeField] float score;

    Vector3 Size => Vector3.one * Random.Range(size.minValue, size.maxValue);

    void OnEnable() {
        transform.localScale = Size;
        transform.rotation = Random.rotation;
    }

    public void Hit() {
    }
}