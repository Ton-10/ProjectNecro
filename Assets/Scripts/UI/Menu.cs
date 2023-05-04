using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Menu : StandardUI
{
    public List<GameObject> outerElements;
    public List<GameObject> innerElements;
    public List<GameObject> backgroundElements;
    public List<GameObject> accentElements;
    public InputActionAsset actions;
    public bool Enabled;

    private InputAction menuAction;


    private Color currentColor;
    // Start is called before the first frame update
    void Start()
    {
        menuAction = actions.FindActionMap("PlayerActions").FindAction("Menu");
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (child.name.Contains("Container"))
            {
                outerElements.Add(child.gameObject);
                ColorUtility.TryParseHtmlString(outerColor + transparency, out currentColor);
            }
            else if (child.name.Contains("Text"))
            {
                innerElements.Add(child.gameObject);
                ColorUtility.TryParseHtmlString(innerColor, out currentColor);
            }
            else if (child.name.Contains("Background"))
            {
                backgroundElements.Add(child.gameObject);
                ColorUtility.TryParseHtmlString(backgroundColor, out currentColor);
            }
            else
            {
                accentElements.Add(child.gameObject);
                ColorUtility.TryParseHtmlString(accentColor, out currentColor);
            }
            if(child.GetComponent<Image>() != null)
            {
                child.GetComponent<Image>().color = currentColor;
            }
            else if (child.GetComponent<TextMeshProUGUI>() != null)
            {
                child.GetComponent<TextMeshProUGUI>().color = currentColor;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (menuAction.WasPressedThisFrame())
        {
            Enabled = !Enabled;
            gameObject.GetComponent<Canvas>().enabled = Enabled;
        }


    }
}
