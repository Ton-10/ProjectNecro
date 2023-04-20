using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    GameObject Target;
    public int Distance;
    public CameraMode mode;

    public enum CameraMode
    {
        Cave,
        City,
    }

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform.Find("Body").gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check camera mode before setting camera distance
        int cameraDistance;
        if (mode.Equals(CameraMode.Cave))
        {
            cameraDistance = Distance / 2;
        }
        else if (mode.Equals(CameraMode.City))
        {
            cameraDistance = Distance * 2;
        }
        else
        {
            cameraDistance = Distance / 1;
        }

        // Set camera distance and look at target (making a 45 deg angle about the axis perpendicular to the direction it is facing
        if (Camera.main.orthographic)
        {
            Camera.main.orthographicSize = cameraDistance;
        }
        transform.position = Target.transform.position + new Vector3(cameraDistance, cameraDistance, 0);
        transform.LookAt(Target.transform.position);
    }
}
