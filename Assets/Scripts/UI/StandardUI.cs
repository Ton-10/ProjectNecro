using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardUI : MonoBehaviour
{
    protected string outerColor = "403D4B";
    protected string innerColor = "5387FF";
    protected int outerAlphaValue = 150;
    protected bool facingCamera = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void LateUpdate()
    {
        if (facingCamera)
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }
}
