using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float MovementSpeed { get; set; }
    public float AttackSpeed { get; set; }
    public float PhysicalAttack { get; set; }
    public float MagicAttack { get; set; }
    public float SpecialAttack { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        // Set Default stat values
        MovementSpeed = 10;
        AttackSpeed = 1;
        PhysicalAttack = 1;
        MagicAttack = 1;
        SpecialAttack = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickupPart(GameObject part)
    {
        //Add to stats based on part's stats
        //Add to player model
    }
    public void DamagePart(GameObject part)
    {
        //Get part's stats, negate values from player's current stats
        //Damage part a random amount (reduce stat values, etc...)
        //Add updated stats to this
        //Add to player model
    }


}
