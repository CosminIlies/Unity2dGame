using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    [SerializeField] private string targetTag;
    [SerializeField] private float smoothRotation;

    private Transform target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    void Update()
    {
        Vector3 dir = target.transform.position - transform.position;

        dir.Normalize();

        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, rotZ), smoothRotation * Time.deltaTime);
    }
}
