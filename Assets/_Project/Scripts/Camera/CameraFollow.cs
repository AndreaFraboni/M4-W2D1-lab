using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;


    private void LateUpdate()
    {
        if (target == null) return;

        transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
    }
}
