using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public Vector3 normalPose, aimPose;
    public float aimSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, aimPose, aimSpeed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, normalPose, aimSpeed * Time.deltaTime);
        }
    }
}
