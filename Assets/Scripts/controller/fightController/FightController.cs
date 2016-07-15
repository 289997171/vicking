
using DG.Tweening;
using UnityEngine;

public class FightController : MonoBehaviour
{

    protected LocalPlayer localPlayer;
    protected SkillController skillController;
    protected Moveable moveable;

    protected void Start()
    {
        this.localPlayer = GetComponent<LocalPlayer>();
        this.skillController = GetComponent<SkillController>();
        this.moveable = GetComponent<Moveable>();

    }

    //#if UNITY_EDITOR
    //    void Update()
    //    {
    //        if (Input.GetKeyUp(KeyCode.Alpha1))
    //        {
    //            Debug.Log("KeyCode::1");
    //            this.skillController.castSkill(null, 1, 1);
    //        }
    //        else if (Input.GetKeyUp(KeyCode.Alpha2))
    //        {
    //            Debug.Log("KeyCode::2");
    //            this.skillController.castSkill(null, 2, 1);
    //        }
    //        else if (Input.GetKeyUp(KeyCode.Alpha3))
    //        {
    //            Debug.Log("KeyCode::3");
    //            this.skillController.castSkill(null, 3, 1);
    //        }
    //        else if (Input.GetKeyUp(KeyCode.Alpha4))
    //        {
    //            Debug.Log("KeyCode::4");
    //            this.skillController.castSkill(null, 4, 1);
    //        }
    //        else if (Input.GetKeyUp(KeyCode.Alpha5))
    //        {
    //            Debug.Log("KeyCode::4");
    //            this.skillController.castSkill(null, 5, 1);
    //        }
    //        else if (Input.GetKeyUp(KeyCode.Alpha6))
    //        {
    //            Debug.Log("KeyCode::4");
    //            this.skillController.castSkill(null, 6, 1);
    //        }
    //    }
    //#endif


    public void faceToTarget()
    {
        if (this.localPlayer.Targetable == null)
        {
            Debug.LogError("没有目标");
            return;
        }

        Vector3 direction = this.localPlayer.Targetable.transform.position - this.transform.position;
        direction.y = 0f;
        direction.Normalize();

        this.moveable.turn(direction);
    }

    // 移动到： EffectController.playEffect
    //    public void playEffect(string s)
    //    {
    //        Debug.Log("播放特效： " + s);
    //    }

    public void playSound(string s)
    {
        Debug.Log("播放音效: " + s);
    }

    public void moveForward(string s)
    {
        Debug.Log("向前移动： " + s);
        string[] splits = s.Split(',');

        float move = float.Parse(splits[0]);
        float speed = float.Parse(splits[1]);
        bool canNav = bool.Parse(splits[2]);
        
        // TODO 会导致穿墙，需要修改
        // this.transform.DOBlendableMoveBy(transform.forward*move, duration);
        this.moveable.moveBy(transform.forward*move, speed, canNav);
    }

    public void moveBackward(string s)
    {
        Debug.Log("向后移动： " + s);
        string[] splits = s.Split(',');

        float move = float.Parse(splits[0]);
        float speed = float.Parse(splits[1]);
        this.moveable.moveBy(transform.forward * move, speed, false);
    }


    public void jump(string s)
    {
        Debug.Log("跳跃： " + s);
        string[] splits = s.Split(',');
        float move = float.Parse(splits[0]);
        float duration = float.Parse(splits[1]);
        Tweener up = this.transform.DOBlendableMoveBy(transform.up * move, duration)
            .OnStart(() =>
            {
                this.moveable.setFly(true);
            })
            .OnComplete(() =>
            {
                this.moveable.setFly(false);
                this.moveable.setOnGround(false);
            });
    }

    public void canMove(int can)
    {
        Debug.LogError("canTurn :: " + can);
        if (can == 0)
        {
            this.moveable.setCanTurn(false);
        }
        else
        {
            this.moveable.setCanTurn(true);
        }
    }
}
