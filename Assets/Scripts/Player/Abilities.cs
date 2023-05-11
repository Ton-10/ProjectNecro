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
    private Animator anim;
    private MapMovement movement;
    private bool startAttack, attacking, endAttack;
    private int reach;
    GameObject[] soma = null;
    GameObject closestObject = null;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.Find("Body").gameObject.GetComponent<Animator>();
        movement = gameObject.GetComponent<MapMovement>();
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
            anim.SetBool("Attack", true);
            movement.CanMove = false;
            // Play swing noise here
        }
        if (attackAction.WasPerformedThisFrame() && startAttack && !endAttack)
        {
            attacking = true;
            startAttack = false;
        }
        if (attacking
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 
            && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            movement.CanMove = true;
            // Play parry stance activate noise here
        }
        if (attackAction.WasReleasedThisFrame() && attacking)
        {
            attacking = false;
            endAttack = true;
            anim.SetBool("Attack", false);
            movement.CanMove = true;
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
                // Play pickup noise here
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
    void OnEnable()
    {
        actions.FindActionMap("PlayerActions").Enable();
    }
    void OnDisable()
    {
        actions.FindActionMap("PlayerActions").Disable();
    }
}
