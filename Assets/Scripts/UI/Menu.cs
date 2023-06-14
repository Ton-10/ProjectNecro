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
    public List<GameObject> buttonElements;
    public InputActionAsset actions;
    public bool Enabled;

    private InputAction menuAction;
    private GameObject Currentpage;
    private GameObject player;


    private Color currentColor;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        menuAction = actions.FindActionMap("PlayerActions").FindAction("Menu");
        Currentpage = 
            gameObject.transform.Find
            ("MainContainer/MenuPageBackground/MenuPageContainer/StatusDetailsContainer").gameObject;
        ApplyStyles();
    }

    // Update is called once per frame
    void Update()
    {
        if (menuAction.WasPressedThisFrame())
        {
            Enabled = !Enabled;
            UpdateStatScreen();
            gameObject.GetComponent<Canvas>().enabled = Enabled;
        }


    }
    public void ApplyStyles()
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>(true))
        {
            if (child.name.ToLower().Contains("button"))
            {
                buttonElements.Add(child.gameObject);
                ColorUtility.TryParseHtmlString(accentColor, out currentColor);
                ColorBlock newColors = child.GetComponent<Button>().colors;
                newColors.selectedColor = currentColor;
                child.GetComponent<Button>().colors = newColors;
            }
            if (child.name.ToLower().Contains("container"))
            {
                outerElements.Add(child.gameObject);
                ColorUtility.TryParseHtmlString(outerColor + transparency, out currentColor);
            }
            else if (child.name.ToLower().Contains("text"))
            {
                innerElements.Add(child.gameObject);
                ColorUtility.TryParseHtmlString(innerColor, out currentColor);
            }
            else if (child.name.ToLower().Contains("background"))
            {
                backgroundElements.Add(child.gameObject);
                ColorUtility.TryParseHtmlString(backgroundColor, out currentColor);
            }
            else
            {
                accentElements.Add(child.gameObject);
                ColorUtility.TryParseHtmlString(accentColor, out currentColor);
            }
            if (child.GetComponent<Image>() != null)
            {
                child.GetComponent<Image>().color = currentColor;
            }
            else if (child.GetComponent<TextMeshProUGUI>() != null)
            {
                child.GetComponent<TextMeshProUGUI>().color = currentColor;
            }
            Currentpage.SetActive(true);
        }
    }
    public void OpenStatus()
    {
        Currentpage.SetActive(false);
        Currentpage = 
            gameObject.transform.Find
            ("MainContainer/MenuPageBackground/MenuPageContainer/StatusDetailsContainer").gameObject;
        UpdateStatScreen();
        Currentpage.SetActive(true);
    }

    public void OpenOptions()
    {
        Currentpage.SetActive(false);
        Currentpage = 
            gameObject.transform.Find
            ("MainContainer/MenuPageBackground/MenuPageContainer/OptionDetailsContainer").gameObject;
        Currentpage.SetActive(true);
    }
    public void UpdateStatScreen()
    {
        //Assuming current page is the StatusDetailsContainer
        Transform container = Currentpage.transform.Find("StatContainer");
        List<Transform> statTexts = new List<Transform>();  
        statTexts.AddRange(container.GetComponentsInChildren<Transform>(true));
        statTexts.Remove(container);
        Stats playerStats = player.GetComponent<Stats>();
        foreach (Transform text in statTexts)
        {
            string statName = text.name.Split("Text")[0];
            text.GetComponent<TextMeshProUGUI>().text = $"{statName} \n {playerStats.GetStatByName(statName)}";
        }
        foreach (GameObject soma in player.GetComponent<Abilities>().EquippedSoma)
        {
            
            SomaAttacher somaAttacher = soma.GetComponent<SomaAttacher>();
            string buttonName = somaAttacher.SomaType + somaAttacher.AttachmentLocation + "ButtonContainer";
            Transform somaList = Currentpage.transform.Find("SomaInfoBackground/SomaListContainer/Viewport/Content");
            if (somaList.Find(buttonName) == null){
                GameObject newButton = new GameObject
                {
                    name = buttonName
                };
                Button buttonComponent = newButton.AddComponent<Button>();
                Image imageComponent = newButton.AddComponent<Image>();
                buttonComponent.targetGraphic = newButton.GetComponent<Graphic>();
                GameObject newText = new GameObject();
                newText.transform.SetParent(newButton.transform);
                TextMeshProUGUI textComponent = newText.AddComponent<TextMeshProUGUI>();
                textComponent.horizontalAlignment = HorizontalAlignmentOptions.Center;
                textComponent.verticalAlignment = VerticalAlignmentOptions.Middle;
                textComponent.autoSizeTextContainer = true;
                textComponent.text = somaAttacher.SomaType + somaAttacher.AttachmentLocation;
                textComponent.name = somaAttacher.SomaType + somaAttacher.AttachmentLocation + "Text";
                newButton.AddComponent<Tooltip>().somaAttacher = somaAttacher;
                newButton.transform.SetParent(somaList);
                newButton.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        ApplyStyles();
    }
}
