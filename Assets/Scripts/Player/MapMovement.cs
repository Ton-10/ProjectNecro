using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapMovement : MonoBehaviour
{
    public Stats playerStats;
    public bool CanMove;
    public Animator anim;
    // assign the actions asset to this field in the inspector:
    public InputActionAsset actions;

    // private field to store move action reference
    private InputAction moveAction;

    private Rigidbody body;
    private float deadZone = 0f;
    private float WaitTime;

    // Start is called before the first frame update
    void Start()
    {
        //anim = transform.Find("Body").gameObject.GetComponent<Animator>();
        moveAction = actions.FindActionMap("PlayerActions").FindAction("Move");
        playerStats = gameObject.GetComponent<Stats>();
        body = gameObject.GetComponent<Rigidbody>();
        UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        if (WaitTime < Time.time)
        {
            if (Math.Abs(moveVector.y) > deadZone || Math.Abs(moveVector.x) > deadZone)
            {
                if (CanMove)
                {
                    Vector3 move = new Vector3(-moveVector.y, 0 , moveVector.x);
                    move = move.normalized * playerStats.MovementSpeed;
                    body.velocity = move;
                    //UpdatePosition();
                }
            }
            else if( body.velocity != new Vector3(0,0,0))
            {
               body.velocity = new Vector3(0, 0, 0);
            }
        }
    }
    public void UpdatePosition()
    {
        PlayMoveAnimation();
    }
    public void WaitForTime(float t)
    {
        WaitTime = Time.time + t;
    }
    public void PlayMoveAnimation()
    {
        //anim.SetTrigger("Move");
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
