
using UnityEngine;

public class FightController : MonoBehaviour
{

    protected LocalPlayer localPlayer;
    protected SkillController skillController;

    protected void Start()
    {
        this.localPlayer = GetComponent<LocalPlayer>();
        this.skillController = GetComponent<SkillController>();

    }

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Debug.Log("KeyCode::1");
            this.skillController.castSkill(null, 0, 1);
        } else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            Debug.Log("KeyCode::2");
            this.skillController.castSkill(null, 0, 2);
        }else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            Debug.Log("KeyCode::3");
            this.skillController.castSkill(null, 0, 3);
        }
    }
#endif

}
