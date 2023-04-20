using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Manages player actions outside of basic movement
public class Abilities : MonoBehaviour
{
    public Stats playerStats;
    private bool startAttack, attacking, endAttack;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = gameObject.GetComponent<Stats>();
    }
    // Update is called once per frame
    void Update()
    {
        // Can fire three separate events swing, hold, let go
        if (Input.GetButtonDown("Attack") && !endAttack)
        {
            startAttack = true;
            print("Start attacking");
        }
        if (Input.GetButton("Attack") && startAttack && !endAttack)
        {
            attacking = true;
            startAttack = false;
            print("Is holding");
        }
        if (Input.GetButtonUp("Attack") && attacking)
        {
            attacking = false;
            endAttack = true;
            print("stop attacking");
            StartCoroutine(attackCoolDown(0.5f));
        }
        if (Input.GetButtonDown("Cast"))
        {
            print("Cast");
        }
        if (Input.GetButtonDown("Interact"))
        {
            print("interacted");
            // run interact logic
        }
    }
    IEnumerator attackCoolDown(float wait)
    {
        yield return new WaitForSeconds(wait / playerStats.AttackSpeed );
        endAttack = false;
    }
}
