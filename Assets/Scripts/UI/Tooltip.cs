using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : StandardUI, ISelectHandler, IDeselectHandler
{
    public SomaAttacher somaAttacher;
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("selected button" + gameObject.name);
        Debug.Log(somaAttacher.SomaType);
        Debug.Log(somaAttacher.somaStats.name);
        Debug.Log(somaAttacher.somaStats.PhysicalAttack);
        Debug.Log(somaAttacher.somaStats.MagicAttack);
        Debug.Log(somaAttacher.somaStats.SpecialAttack);
        Debug.Log(somaAttacher.somaStats.AttackSpeed);
        Debug.Log(somaAttacher.somaStats.MovementSpeed);
        Debug.Log(somaAttacher.somaStats.HitPoints);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("deselected button" + gameObject.name);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
