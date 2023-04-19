using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    Light target;
    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<Light>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float mod = Mathf.Cos(Time.time);
        target.color = new Color(1f*mod, 1f*mod, 1f*mod);
    }
}
