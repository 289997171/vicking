
using UnityEngine;

public class FightController : MonoBehaviour
{

    protected LocalPlayer localPlayer;
    protected SkillController skillController;
    protected Moveable mooveable;

    protected void Start()
    {
        this.localPlayer = GetComponent<LocalPlayer>();
        this.skillController = GetComponent<SkillController>();
        this.mooveable = GetComponent<Moveable>();

    }

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Debug.Log("KeyCode::1");
            this.skillController.castSkill(null, 1, 1);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            Debug.Log("KeyCode::2");
            this.skillController.castSkill(null, 2, 1);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            Debug.Log("KeyCode::3");
            this.skillController.castSkill(null, 3, 1);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            Debug.Log("KeyCode::4");
            this.skillController.castSkill(null, 4, 1);
        }
    }
#endif

    public void PlayEffect(string s)
    {
        Debug.Log("PlayEffect !!!! " + " " + s);
    }

    public void PlayEffect(int i)
    {
        Debug.Log("PlayEffect !!!! " + " " + i);
    }

    public void PlayEffect(float f)
    {
        Debug.Log("PlayEffect !!!! " + " " + f);
    }

    public void PlayEffect(GameObject obj)
    {
        Debug.Log("PlayEffect !!!! " + " " + obj);
    }

    public void canMove(int can)
    {
        Debug.LogError("canTurn :: " + can);
        if (can == 0)
        {
            this.mooveable.setCanTurn(false);
        }
        else
        {
            this.mooveable.setCanTurn(true);
        }
    }
}
