
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomController : CustomController, IPersonController
{

    private GameObject playerObj;
    private Transform playerTra;
    private Player player;

    #region 保存所有部位的所有装备
    public Dictionary<string, SkinnedMeshRenderer> heads = new Dictionary<string, SkinnedMeshRenderer>();
    public Dictionary<string, SkinnedMeshRenderer> bodys = new Dictionary<string, SkinnedMeshRenderer>();
    public Dictionary<string, SkinnedMeshRenderer> clavicles = new Dictionary<string, SkinnedMeshRenderer>();
    public Dictionary<string, SkinnedMeshRenderer> hands = new Dictionary<string, SkinnedMeshRenderer>();
    public Dictionary<string, SkinnedMeshRenderer> skirts = new Dictionary<string, SkinnedMeshRenderer>();
    public Dictionary<string, SkinnedMeshRenderer> foots = new Dictionary<string, SkinnedMeshRenderer>();
    public Dictionary<string, SkinnedMeshRenderer> capes = new Dictionary<string, SkinnedMeshRenderer>();
    #endregion

    #region 当前角色装备
    public SkinnedMeshRenderer headRenderer;
    public SkinnedMeshRenderer bodyRenderer;
    public SkinnedMeshRenderer clavicleRenderer;
    public SkinnedMeshRenderer handRenderer;
    public SkinnedMeshRenderer skirtRenderer;
    public SkinnedMeshRenderer footRenderer;
    public SkinnedMeshRenderer capeRenderer;
    #endregion

    public void updated()
    {
        Start();
    }

    void Start()
    {
        this.playerObj = this.gameObject;
        this.playerTra = this.transform;
        this.player = this.GetComponent<Player>();


        SkinnedMeshRenderer[] renderers = this.playerTra.GetComponentsInChildren<SkinnedMeshRenderer>();
        string rname = "";
        foreach (SkinnedMeshRenderer renderer in renderers)
        {
            renderer.gameObject.SetActive(false);
            //renderer.enabled = false;
            rname = renderer.name;
            if (rname.StartsWith(Head + "_"))
            {
                heads.Add(rname, renderer);
            }
            else if (rname.StartsWith(Body + "_"))
            {
                bodys.Add(rname, renderer);
            }
            else if (rname.StartsWith(Clavicle + "_"))
            {
                clavicles.Add(rname, renderer);
            }
            else if (rname.StartsWith(Hand + "_"))
            {
                hands.Add(rname, renderer);
            }
            else if (rname.StartsWith(Skirt + "_"))
            {
                skirts.Add(rname, renderer);
            }
            else if (rname.StartsWith(Foot + "_"))
            {
                foots.Add(rname, renderer);
            }
            else if (rname.StartsWith(Cape + "_"))
            {
                capes.Add(rname, renderer);
            }
        }
    }

    public override void changeHead(string head)
    {
        SkinnedMeshRenderer skinnedMeshRenderer;
        if (heads.TryGetValue(head, out skinnedMeshRenderer))
        {
            if (headRenderer != null && headRenderer != skinnedMeshRenderer)
            {
                //skinnedMeshRenderer.enabled = false;
                headRenderer.gameObject.SetActive(false);
            }
            //skinnedMeshRenderer.enabled = true;
            skinnedMeshRenderer.gameObject.SetActive(true);
            headRenderer = skinnedMeshRenderer;
        }
    }

    public override void changeBody(string body)
    {
        SkinnedMeshRenderer skinnedMeshRenderer;
        if (bodys.TryGetValue(body, out skinnedMeshRenderer))
        {
            if (bodyRenderer != null && bodyRenderer != skinnedMeshRenderer)
            {
                //bodyRenderer.enabled = false;
                bodyRenderer.gameObject.SetActive(false);
            }
            //skinnedMeshRenderer.enabled = true;
            skinnedMeshRenderer.gameObject.SetActive(true);
            bodyRenderer = skinnedMeshRenderer;
        }
    }

    public override void changeClavicle(string clavicle)
    {
        SkinnedMeshRenderer skinnedMeshRenderer;
        if (clavicles.TryGetValue(clavicle, out skinnedMeshRenderer))
        {
            if (clavicleRenderer != null && clavicleRenderer != skinnedMeshRenderer)
            {
                //clavicleRenderer.enabled = false;
                clavicleRenderer.gameObject.SetActive(false);
            }
            //skinnedMeshRenderer.enabled = true;
            skinnedMeshRenderer.gameObject.SetActive(true);
            clavicleRenderer = skinnedMeshRenderer;
        }
    }

    public override void changeHand(string hand)
    {
        SkinnedMeshRenderer skinnedMeshRenderer;
        if (hands.TryGetValue(hand, out skinnedMeshRenderer))
        {
            if (handRenderer != null && handRenderer != skinnedMeshRenderer)
            {
                //handRenderer.enabled = false;
                handRenderer.gameObject.SetActive(false);
            }
            //skinnedMeshRenderer.enabled = true;
            skinnedMeshRenderer.gameObject.SetActive(true);
            handRenderer = skinnedMeshRenderer;
        }
    }

    public override void changeSkirt(string skirt)
    {
        SkinnedMeshRenderer skinnedMeshRenderer;
        if (skirts.TryGetValue(skirt, out skinnedMeshRenderer))
        {
            if (skirtRenderer != null && skirtRenderer != skinnedMeshRenderer)
            {
                //skirtRenderer.enabled = false;
                skirtRenderer.gameObject.SetActive(false);
            }
            //skinnedMeshRenderer.enabled = true;
            skinnedMeshRenderer.gameObject.SetActive(true);
            skirtRenderer = skinnedMeshRenderer;
        }
    }

    public override void changeFoot(string foot)
    {
        SkinnedMeshRenderer skinnedMeshRenderer;
        if (foots.TryGetValue(foot, out skinnedMeshRenderer))
        {
            if (footRenderer != null && footRenderer != skinnedMeshRenderer)
            {
                //footRenderer.enabled = false;
                footRenderer.gameObject.SetActive(false);
            }
            //skinnedMeshRenderer.enabled = true;
            skinnedMeshRenderer.gameObject.SetActive(true);
            footRenderer = skinnedMeshRenderer;
        }
    }

    public override void changeCape(string cape)
    {
        SkinnedMeshRenderer skinnedMeshRenderer;
        if (capes.TryGetValue(cape, out skinnedMeshRenderer))
        {
            if (capeRenderer != null && capeRenderer != skinnedMeshRenderer)
            {
                //capeRenderer.enabled = false;
                capeRenderer.gameObject.SetActive(false);
            }
            //skinnedMeshRenderer.enabled = true;
            skinnedMeshRenderer.gameObject.SetActive(true);
            capeRenderer = skinnedMeshRenderer;
        }
    }


    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 110, 10, 100, 30), "初始装备"))
        {
            // 初始化默认装备
            {
                string defaultEqitem = "LV100";
                changeHead(CustomController.Head + "_" + defaultEqitem);
                changeBody(CustomController.Body + "_" + defaultEqitem);
                changeClavicle(CustomController.Clavicle + "_" + defaultEqitem);
                changeSkirt(CustomController.Skirt + "_" + defaultEqitem);
                changeCape(CustomController.Cape + "_" + defaultEqitem);
                changeFoot(CustomController.Foot + "_" + defaultEqitem);
                changeHand(CustomController.Hand + "_" + defaultEqitem);
            }
        }

        if (GUI.Button(new Rect(Screen.width - 110, 50, 100, 30), "自定义换装"))
        {
            this.playerObj.getOrAddComponent<CustomControllerTest>();
        }
    }
}
