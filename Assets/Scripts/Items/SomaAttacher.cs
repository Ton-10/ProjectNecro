using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomaAttacher : MonoBehaviour
{
    public BodyPart AttachmentLocation;
    public Stats somaStats;
    public string SomaType;
    public Material SomaMat;
    public GameObject SomaModel;
    public GameObject SomaOverlay;
    GameObject player;

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
        player = GameObject.FindWithTag("Player");
        somaStats = gameObject.GetComponent<Stats>();
        SomaType = gameObject.name;
        SomaMat = gameObject.GetComponent<Renderer>().material;
        AttachmentLocation =  AttachmentLocation == BodyPart.None ? BodyPart.Head : AttachmentLocation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AttachSoma()
    {
        SkinnedMeshRenderer renderer;
        Material[] mats;
        if (!player.transform.Find("Body/"+SomaType))
        {
            SomaOverlay = Instantiate(SomaModel, player.transform.Find("Body"));
            SomaOverlay.name = SomaType;
            renderer = SomaOverlay.transform.Find("skeleton").GetComponent<SkinnedMeshRenderer>();
            mats = renderer.materials;
            foreach (Material mat in mats)
            {
                mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0f);
            }
            player.GetComponent<MapMovement>().SomaAnims.Add(SomaOverlay.GetComponent<Animator>());
        }
        else
        {
            SomaOverlay = player.transform.Find("Body/" + SomaType).gameObject;
        }
        renderer = SomaOverlay.transform.Find("skeleton").GetComponent<SkinnedMeshRenderer>();
        mats = renderer.materials;
        if (AttachmentLocation == BodyPart.Head)
        {
            foreach (Material mat in mats)
            {
                if (mat.name.Contains("Head"))
                {
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, SomaMat.color.a);
                }
            }
        }
        if (AttachmentLocation == BodyPart.Torso)
        {
            foreach (Material mat in mats)
            {
                if (mat.name.Contains("Torso"))
                {
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, SomaMat.color.a);
                }
            }
        }
        if (AttachmentLocation == BodyPart.RightArm)
        {
            foreach (Material mat in mats)
            {
                if (mat.name.Contains("RArm"))
                {
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, SomaMat.color.a);
                }
                if (mat.name.Contains("RForearm"))
                {
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, SomaMat.color.a);
                }
                if (mat.name.Contains("RHand"))
                {
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, SomaMat.color.a);
                }
            }
        }
        if (AttachmentLocation == BodyPart.LeftArm)
        {
            foreach (Material mat in mats)
            {
                if (mat.name.Contains("LArm"))
                {
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, SomaMat.color.a);
                }
                if (mat.name.Contains("LForearm"))
                {
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, SomaMat.color.a);
                }
                if (mat.name.Contains("LHand"))
                {
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, SomaMat.color.a);
                }
            }
        }
        if (AttachmentLocation == BodyPart.RightLeg)
        {
            foreach (Material mat in mats)
            {
                if (mat.name.Contains("RLeg"))
                {
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, SomaMat.color.a);
                }
                if (mat.name.Contains("RShin"))
                {
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, SomaMat.color.a);
                }
                if (mat.name.Contains("RFoot"))
                {
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, SomaMat.color.a);
                }
            }
        }
        if (AttachmentLocation == BodyPart.LeftLeg)
        {
            foreach (Material mat in mats)
            {
                if (mat.name.Contains("LLeg"))
                {
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, SomaMat.color.a);
                }
                if (mat.name.Contains("LShin"))
                {
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, SomaMat.color.a);
                }
                if (mat.name.Contains("LFoot"))
                {
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, SomaMat.color.a);
                }
            }
        }
    }
}
