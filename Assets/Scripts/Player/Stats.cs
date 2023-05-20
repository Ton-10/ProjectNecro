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
    public float HitPoints { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        // Set Default stat values
        MovementSpeed = 10;
        AttackSpeed = 1;
        PhysicalAttack = 1;
        MagicAttack = 1;
        SpecialAttack = 1;
        HitPoints = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addStats(Stats somaStats)
    {
        //Add to stats based on part's stats
        MovementSpeed += somaStats.MovementSpeed;
        AttackSpeed += somaStats.AttackSpeed;
        PhysicalAttack += somaStats.PhysicalAttack;
        MagicAttack += somaStats.MagicAttack;
        SpecialAttack += somaStats.SpecialAttack;
    }
    public void lowerRandomStat(int damageValue)
    {
        //Get part's stats, negate values from player's current stats
        //Damage part a random amount (reduce stat values, etc...)
        //Add updated stats to this
        //Add to player model
    }


}
