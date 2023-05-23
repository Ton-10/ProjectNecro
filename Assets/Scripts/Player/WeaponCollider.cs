using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public List<GameObject> hits;
    
    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            hits.Add(hit.gameObject);
        }
    }
    void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.tag == "Enemy" && hits.Contains(hit.gameObject))
        {
            hits.Remove(hit.gameObject);
        }
    }
}
