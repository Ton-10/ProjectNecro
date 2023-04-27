using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// Manages player actions outside of basic movement
public class Abilities : MonoBehaviour
{
    public Stats playerStats;
    public List<GameObject> EquippedSoma;
    // assign the actions asset to this field in the inspector:
    public InputActionAsset actions;

    // private field to store move action reference
    private InputAction attackAction, castAction, interactAction, moveAction;

    private bool startAttack, attacking, endAttack;
    private int reach;
    GameObject[] soma = null;
    GameObject closestObject = null;
    // Start is called before the first frame update
    void Start()
    {
        attackAction = actions.FindActionMap("PlayerActions").FindAction("Attack");
        castAction = actions.FindActionMap("PlayerActions").FindAction("Cast");
        interactAction = actions.FindActionMap("PlayerActions").FindAction("Interact");
        moveAction = actions.FindActionMap("PlayerActions").FindAction("Move");
        playerStats = gameObject.GetComponent<Stats>();
        reach = 20;
    }
    // Update is called once per frame
    void Update()
    {
        // Use user input as trigger to update closest soma
        if (moveAction.ReadValue<Vector2>().magnitude > 0)
        {
            updateSomaProximity();
        }
        // Can fire three separate events swing, hold, let go
        if (attackAction.WasPressedThisFrame() && !endAttack)
        {
            startAttack = true;
            print("Start attacking");
        }
        if (attackAction.WasPerformedThisFrame() && startAttack && !endAttack)
        {
            attacking = true;
            startAttack = false;
            print("Is holding");
        }
        if (attackAction.WasReleasedThisFrame() && attacking)
        {
            attacking = false;
            endAttack = true;
            print("stop attacking");
            StartCoroutine(attackCoolDown(0.5f / playerStats.AttackSpeed));
        }
        if (castAction.WasPressedThisFrame())
        {
            print("Cast");
        }
        // Interact with soma
        if (interactAction.WasPressedThisFrame())
        {
            if(closestObject.GetComponent<Stats>() != null)
            {
                print("Interacted with" + closestObject.name);
                Stats somaStats = closestObject.GetComponent<Stats>();
                playerStats.addStats(somaStats);
                EquippedSoma.Add(closestObject);
                closestObject.tag = "Untagged";
                closestObject.transform.Find("InteractionPopup").GetComponent<Canvas>().enabled = false;
                closestObject.SetActive(false);
                closestObject = null;
                // add soma model to player
            }
        }
    }
    IEnumerator attackCoolDown(float wait)
    {
        yield return new WaitForSeconds(wait / playerStats.AttackSpeed );
        endAttack = false;
    }
    private void updateSomaProximity()
    {
        soma = GameObject.FindGameObjectsWithTag("Soma");

        for (int i = 0; i < soma.Length; i++)
        {
            float newDistance = Vector3.Magnitude(soma[i].transform.position - gameObject.transform.position);
            if (closestObject != null)
            {
                float oldDistance = Vector3.Magnitude(closestObject.transform.position - gameObject.transform.position);
                if (newDistance < oldDistance && newDistance <= reach)
                {
                    closestObject.transform.Find("InteractionPopup").GetComponent<Canvas>().enabled = false;
                    closestObject = soma[i];
                    closestObject.transform.Find("InteractionPopup").GetComponent<Canvas>().enabled = true;
                }
            }
            else if (closestObject == null && newDistance <= reach)
            {
                closestObject = soma[i];
                closestObject.transform.Find("InteractionPopup").GetComponent<Canvas>().enabled = true;
            }
        }
    }
}
