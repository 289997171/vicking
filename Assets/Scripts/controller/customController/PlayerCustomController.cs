
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomController : CustomController, IPersonController
{

    private GameObject playerObj;
    private Transform playerTra;
    private Player player;

#region 保存所有部位的所有装备
    private Dictionary<string, SkinnedMeshRenderer> heads = new Dictionary<string, SkinnedMeshRenderer>();
    private Dictionary<string, SkinnedMeshRenderer> bodys = new Dictionary<string, SkinnedMeshRenderer>();
    private Dictionary<string, SkinnedMeshRenderer> clavicles = new Dictionary<string, SkinnedMeshRenderer>();
    private Dictionary<string, SkinnedMeshRenderer> hands = new Dictionary<string, SkinnedMeshRenderer>();
    private Dictionary<string, SkinnedMeshRenderer> skirts = new Dictionary<string, SkinnedMeshRenderer>();
    private Dictionary<string, SkinnedMeshRenderer> foots = new Dictionary<string, SkinnedMeshRenderer>();
    private Dictionary<string, SkinnedMeshRenderer> capes = new Dictionary<string, SkinnedMeshRenderer>();
    #endregion

#region 当前角色装备
    private SkinnedMeshRenderer headRenderer;
    private SkinnedMeshRenderer bodyRenderer;
    private SkinnedMeshRenderer clavicleRenderer;
    private SkinnedMeshRenderer handRenderer;
    private SkinnedMeshRenderer skirtRenderer;
    private SkinnedMeshRenderer footRenderer;
    private SkinnedMeshRenderer capeRenderer;
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
            rname = renderer.name;
            if (rname.StartsWith(Head + "_"))
            {

                renderer.enabled = false;
                heads.Add(rname, renderer);
            }
            else if (rname.StartsWith(Body + "_"))
            {
                renderer.enabled = false;
                bodys.Add(rname, renderer);
            }
            else if (rname.StartsWith(Clavicle + "_"))
            {
                renderer.enabled = false;
                clavicles.Add(rname, renderer);
            }
            else if (rname.StartsWith(Hand + "_"))
            {
                renderer.enabled = false;
                hands.Add(rname, renderer);
            }
            else if (rname.StartsWith(Skirt + "_"))
            {
                renderer.enabled = false;
                skirts.Add(rname, renderer);
            }
            else if (rname.StartsWith(Foot + "_"))
            {
                renderer.enabled = false;
                foots.Add(rname, renderer);
            }
            else if (rname.StartsWith(Cape + "_"))
            {
                renderer.enabled = false;
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
               headRenderer.enabled = false;
            }
            skinnedMeshRenderer.enabled = true;
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
                bodyRenderer.enabled = false;
            }
            skinnedMeshRenderer.enabled = true;
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
                clavicleRenderer.enabled = false;
            }
            skinnedMeshRenderer.enabled = true;
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
                handRenderer.enabled = false;
            }
            skinnedMeshRenderer.enabled = true;
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
                skirtRenderer.enabled = false;
            }
            skinnedMeshRenderer.enabled = true;
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
                footRenderer.enabled = false;
            }
            skinnedMeshRenderer.enabled = true;
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
                capeRenderer.enabled = false;
            }
            skinnedMeshRenderer.enabled = true;
            capeRenderer = skinnedMeshRenderer;
        }
    }

    
}
