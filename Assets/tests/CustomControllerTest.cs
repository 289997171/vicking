using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CustomControllerTest : MonoBehaviour {

    public string Headname;
    public string Bodyname;
    public string Footname;
    public string Handname;
    public string Claviclename;
    public string Skirtname;
    public string Capename;
    private string[] TypeZB = new string [] { "Body", "Clavicle", "Foot", "Hand", "Head", "Skirt", "Cape" };
    private List<string> Zblist = new List<string>();

    private GameObject ui;

    // Use this for initialization
    void Start ()
    {
        ui = Instantiate(Resources.Load("tests/UI/CustomControllerTestUI")) as GameObject;

        PlayerCustomController playerCustomController = this.GetComponent<PlayerCustomController>();

        List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();

        skinnedMeshRenderers.AddRange(playerCustomController.heads.Values.ToArray());
        skinnedMeshRenderers.AddRange(playerCustomController.bodys.Values.ToArray());
        skinnedMeshRenderers.AddRange(playerCustomController.capes.Values.ToArray());
        skinnedMeshRenderers.AddRange(playerCustomController.clavicles.Values.ToArray());
        skinnedMeshRenderers.AddRange(playerCustomController.foots.Values.ToArray());
        skinnedMeshRenderers.AddRange(playerCustomController.hands.Values.ToArray());
        skinnedMeshRenderers.AddRange(playerCustomController.skirts.Values.ToArray());

        foreach (string name in TypeZB)
        {
            Dropdown dropdown = ui.transform.Find("Panel/Image/" + name).GetComponent<Dropdown>();

            dropdown.captionText.text = name;
            dropdown.options.Clear();

            string defaultZB = "";
            foreach (SkinnedMeshRenderer o in skinnedMeshRenderers)
            {
                if (o.name.StartsWith(name + "_"))
                {
                    Dropdown.OptionData optionData = new Dropdown.OptionData();
                    optionData.text = o.name;
                    dropdown.options.Add(optionData);
                    
                    defaultZB = o.name;
                }
            }

            if (!string.IsNullOrEmpty(defaultZB))
            {
                ChangeZB(defaultZB, name);
            }
            

            if (name.Equals("Head"))
            {
                dropdown.onValueChanged.AddListener(changeHead);
            }
            else if (name.Equals("Body"))
            {
                dropdown.onValueChanged.AddListener(changeBody);
            }
            else if (name.Equals("Foot"))
            {
                dropdown.onValueChanged.AddListener(changeFoot);
            }
            else if (name.Equals("Hand"))
            {
                dropdown.onValueChanged.AddListener(changeHand);
            }
            else if (name.Equals("Clavicle"))
            {
                dropdown.onValueChanged.AddListener(changeClavicle);
            }
            else if (name.Equals("Skirt"))
            {
                dropdown.onValueChanged.AddListener(changeSkirt);
            }
            else if (name.Equals("Cape"))
            {
                dropdown.onValueChanged.AddListener(changeCape);
            }
        }

    }

    private void changeHead(int index)
    {
        string type = "Head";
        Dropdown dropdown = ui.transform.Find("Panel/Image/" + type).GetComponent<Dropdown>();
        Dropdown.OptionData optionData = dropdown.options[index];
        ChangeZB(optionData.text, type);
    }

    private void changeBody(int index)
    {
        string type = "Body";
        Dropdown dropdown = ui.transform.Find("Panel/Image/" + type).GetComponent<Dropdown>();
        Dropdown.OptionData optionData = dropdown.options[index];
        ChangeZB(optionData.text, type);
    }

    private void changeFoot(int index)
    {
        string type = "Foot";
        Dropdown dropdown = ui.transform.Find("Panel/Image/" + type).GetComponent<Dropdown>();
        Dropdown.OptionData optionData = dropdown.options[index];
        ChangeZB(optionData.text, type);
    }

    private void changeHand(int index)
    {
        string type = "Hand";
        Dropdown dropdown = ui.transform.Find("Panel/Image/" + type).GetComponent<Dropdown>();
        Dropdown.OptionData optionData = dropdown.options[index];
        ChangeZB(optionData.text, type);
    }

    private void changeClavicle(int index)
    {
        string type = "Clavicle";
        Dropdown dropdown = ui.transform.Find("Panel/Image/" + type).GetComponent<Dropdown>();
        Dropdown.OptionData optionData = dropdown.options[index];
        ChangeZB(optionData.text, type);
    }

    private void changeSkirt(int index)
    {
        string type = "Skirt";
        Dropdown dropdown = ui.transform.Find("Panel/Image/" + type).GetComponent<Dropdown>();
        Dropdown.OptionData optionData = dropdown.options[index];
        ChangeZB(optionData.text, type);
    }

    private void changeCape(int index)
    {
        string type = "Cape";
        Dropdown dropdown = ui.transform.Find("Panel/Image/" + type).GetComponent<Dropdown>();
        Dropdown.OptionData optionData = dropdown.options[index];
        ChangeZB(optionData.text, type);
    }


    void ChangeZB(string ZBname,string type)
    {
        if (ZBname == string.Empty)
        {
            Debug.LogError("装备名称为空");
            return;
        }
        string[] sp_name = ZBname.Split('_');
        if (sp_name.Length != 2)
        {
            Debug.LogError("与命名规则不符");
            return;
        }
        if (sp_name[0] != type)
        {
            Debug.LogError("所换的装备，跟装备类型不符，请检查");
            return;
        }
        bool CanChange = false;
        for (int i = 0; i < TypeZB.Length; ++i)
        {
            if (sp_name[0] == TypeZB[i])
            {
                CanChange = true;
                break;
            }
        }
        if (!CanChange)
        {
            Debug.LogError("命名规则不符，必须包含对应装备位置，请参考命名规则");
            return;
        }
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).name.Contains(sp_name[0]))
            {
                if (!transform.GetChild(i).name.Equals(ZBname))
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }
}
