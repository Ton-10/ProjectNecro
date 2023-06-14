using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public GameObject SomaDrop;
    public GameObject SomaModel;
    public int detectionRange;
    public float speed;
    public bool gettingHit;
    public Stats enemyStats;
    private Animator anim;
    private Rigidbody body;
    private float moveDelay, hitDelay;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        body = gameObject.GetComponent<Rigidbody>();
        Player = GameObject.FindGameObjectWithTag("Player");
        enemyStats = gameObject.GetComponent<Stats>();
        // Set move delay to move animation duration
        moveDelay = anim.runtimeAnimatorController.animationClips[0].length;
        hitDelay = anim.runtimeAnimatorController.animationClips[2].length;
    }

    // Update is called once per frame
    void Update()
    {
        updatePlayerProximity();

    }
    public IEnumerator Hit(float damageIn)
    {
        anim.SetBool("Hit", true);
        gettingHit = true;
        enemyStats.HitPoints -= damageIn;
        if (enemyStats.HitPoints <= 0)
        {
            
            Material mat = gameObject.transform.Find("SlimeMesh").GetComponent<SkinnedMeshRenderer>().material;
            float startingAlpha = mat.color.a;
            gameObject.transform.Find("DeathParticles").gameObject.SetActive(true);
            for (int i = 6; i > 0; i--)
            {
                mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a - startingAlpha/6);
                yield return new WaitForSeconds(0.1f);
            }
            // Drop soma
            GameObject soma = Instantiate(SomaDrop, transform.position, Quaternion.identity);
            soma.name = gameObject.name + "Soma";
            soma.GetComponent<SomaAttacher>().SomaModel = SomaModel;
            soma.GetComponent<SomaAttacher>().AttachmentLocation = (SomaAttacher.BodyPart) Random.Range(1, System.Enum.GetNames(typeof(SomaAttacher.BodyPart)).Length);
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(hitDelay);
        gettingHit = false;
        anim.SetBool("Hit", false);
    }
    private void updatePlayerProximity()
    {
        
        float newDistance = Vector3.Magnitude(Player.transform.position - gameObject.transform.position);
    
        if (newDistance <= detectionRange && !anim.GetBool("Moving") && !anim.GetBool("Attacking") && !gettingHit)
        {
            anim.SetBool("Moving", true);
            StartCoroutine(moveToPlayer());
            
        }
    }
    IEnumerator moveToPlayer()
    {
        yield return new WaitForSeconds(moveDelay);
        if (!anim.GetBool("Attacking") && !gettingHit)
        {
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            body.velocity = direction * speed;
            transform.rotation = Quaternion.LookRotation(-direction);
        }
        anim.SetBool("Moving", false);
    }
}
