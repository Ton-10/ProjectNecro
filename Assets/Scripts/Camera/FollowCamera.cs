using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    GameObject Target;
    public int Distance;
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform.Find("Body").gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Target.transform.position + new Vector3(Distance, Distance, 0);
        transform.LookAt(Target.transform.position);
    }
}
