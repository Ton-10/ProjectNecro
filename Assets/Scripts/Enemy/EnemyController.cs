using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public int detectionRange;
    public float speed;
    private Animator anim;
    private Rigidbody body;
    private float moveDelay;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        body = gameObject.GetComponent<Rigidbody>();
        Player = GameObject.FindGameObjectWithTag("Player");
        // Set move delay to move animation duration
        moveDelay = anim.runtimeAnimatorController.animationClips[0].length;
    }

    // Update is called once per frame
    void Update()
    {
        updatePlayerProximity();
    }
    private void updatePlayerProximity()
    {
        
        float newDistance = Vector3.Magnitude(Player.transform.position - gameObject.transform.position);
    
        if (newDistance <= detectionRange && !anim.GetBool("Moving") && !anim.GetBool("Attacking"))
        {
            anim.SetBool("Moving", true);
            StartCoroutine(moveToPlayer());
            
        }
    }
    IEnumerator moveToPlayer()
    {
        yield return new WaitForSeconds(moveDelay);
        Vector3 direction = (Player.transform.position - transform.position).normalized;
        body.velocity = direction * speed;
        transform.rotation = Quaternion.LookRotation(-direction);
        anim.SetBool("Moving", false);
    }
}
