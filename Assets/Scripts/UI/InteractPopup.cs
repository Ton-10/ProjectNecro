using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractPopup : StandardUI
{
    public InputActionAsset actions;
    // Start is called before the first frame update
    void Start()
    {
        facingCamera = true;
        gameObject.transform.Find("Button/Text").GetComponent<TextMeshProUGUI>().text = 
            actions.FindAction("Interact").GetBindingDisplayString();
    }
    private void Update()
    {
    }
    // Update is called once per frame
    protected override void LateUpdate()
    {
        base.LateUpdate();
    }
}
