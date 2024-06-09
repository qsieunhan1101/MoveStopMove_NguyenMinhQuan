using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    [SerializeField] private Vector3 offset;
    [SerializeField] public GameObject target;
    [SerializeField] private float smoothTime;
    private Vector3 current = Vector3.zero;


    private void Start()
    {
        SetUpCamera(10,5);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref current, smoothTime);

            transform.LookAt(target.transform);

        }
    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {

    }

    public void SetUpCamera(float distanceFormTager, float heightAboveTager)
    {
        Vector3 newPosition = target.transform.position - target.transform.forward * distanceFormTager;
        newPosition.y = target.transform.position.y + heightAboveTager;
        transform.position = newPosition;
        offset = transform.position - target.transform.position;
    }
}
