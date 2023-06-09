using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomaAttacherOLD : MonoBehaviour
{
    public BodyPart AttachmentLocation;
    public Stats somaStats;
    public string SomaType;
    public Material SomaMat;
    public GameObject SomaModel;
    public GameObject SomaOverlay;

    // Torso ORG-spine.003
    // Hips ORG-spine
    // Head DEF-spine.005
    public enum BodyPart
    {
        None,
        Head,
        Torso,
        RightArm,
        LeftArm,
        RightLeg,
        LeftLeg
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        somaStats = gameObject.GetComponent<Stats>();
        SomaType = gameObject.name;
        SomaMat = gameObject.GetComponent<Material>();
        AttachmentLocation =  AttachmentLocation == BodyPart.None ? BodyPart.Head : AttachmentLocation;
        if (!player.transform.Find(SomaType))
        {
            SomaOverlay = Instantiate(SomaModel, player.transform);
        }
        else
        {
            SomaOverlay = player.transform.Find(SomaType).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AttachSoma()
    {
        if(AttachmentLocation == BodyPart.Head)
        {
            Material[] mats = SomaOverlay.transform.Find("skeleton").GetComponents<Material>();
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("SomaAnchor"))
            {
                if (item.name.Equals("DEF-spine.005"))
                {
                    gameObject.transform.parent = item.transform;
                    gameObject.transform.localPosition = new Vector3(0, 0, 0);
                };
            }
        }
        if (AttachmentLocation == BodyPart.Torso)
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("SomaAnchor"))
            {
                if (item.name.Equals("ORG-spine.003"))
                {
                    gameObject.transform.parent = item.transform;
                    gameObject.transform.localPosition = new Vector3(0, 0, 0);
                };
            }
        }
        if (AttachmentLocation == BodyPart.RightArm)
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("SomaAnchor"))
            {
                if (item.name.Equals("ORG-spine.003"))
                {
                    Transform anchor1 = item.transform.Find("ORG-shoulder.R/DEF-upper_arm.R/DEF-upper_arm.R.001");
                    Transform anchor2 = anchor1.Find("DEF-forearm.R/DEF-forearm.R.001");
                    gameObject.transform.parent = anchor1;
                    gameObject.transform.localPosition = new Vector3(0, 0, 0);
                };
            }
        }
        if (AttachmentLocation == BodyPart.LeftArm)
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("SomaAnchor"))
            {
                if (item.name.Equals("ORG-spine.003"))
                {
                    Transform anchor1 = item.transform.Find("ORG-shoulder.L/DEF-upper_arm.L/DEF-upper_arm.L.001");
                    Transform anchor2 = anchor1.Find("DEF-forearm.L/DEF-forearm.L.001");
                    gameObject.transform.parent = anchor1;
                    gameObject.transform.localPosition = new Vector3(0, 0, 0);
                };
            }
        }
        if (AttachmentLocation == BodyPart.RightLeg)
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("SomaAnchor"))
            {
                if (item.name.Equals("ORG-spine"))
                {
                    Transform anchor1 = item.transform.Find("DEF-thigh.R/DEF-thigh.R.001");
                    Transform anchor2 = anchor1.Find("DEF-shin.R/DEF-shin.R.001");
                    gameObject.transform.parent = anchor1;
                    gameObject.transform.localPosition = new Vector3(0, 0, 0);
                };
            }
        }
        if (AttachmentLocation == BodyPart.LeftLeg)
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("SomaAnchor"))
            {
                if (item.name.Equals("ORG-spine"))
                {
                    Transform anchor1 = item.transform.Find("DEF-thigh.L/DEF-thigh.L.001");
                    Transform anchor2 = anchor1.Find("DEF-shin.L/DEF-shin.L.001");
                    gameObject.transform.parent = anchor1;
                    gameObject.transform.localPosition = new Vector3(0, 0, 0);
                };
            }
        }
    }
}
