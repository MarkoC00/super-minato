using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2f;
    public float xOffset = 0.5f;
    public float yOffset = 2f;
    public Transform target;

    [HideInInspector]
    public bool shouldOffset = false;
    float yOffsetAfterCalc;

    void Update()
    {
        if (!shouldOffset)
            yOffsetAfterCalc = yOffset;
        if (shouldOffset)
            yOffsetAfterCalc = -yOffset;

        Vector3 newPos = new Vector3(target.position.x + xOffset, target.position.y + yOffsetAfterCalc, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed*Time.deltaTime);
    }
}
