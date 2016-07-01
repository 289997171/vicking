

using System.Collections;
using UnityEngine;

public class MoveTest : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(createPlayer(1));
    }


    private IEnumerator createPlayer(int job)
    {
        string modelPath = "";
        if (job == 1)
        {
            modelPath = "person/player/female/Female";
        }
        Object model = Resources.Load(modelPath);
        GameObject player = Instantiate(model) as GameObject;
        yield return 1;

        Player person = player.getOrAddComponent<Player>();
        person.height = 2f;
        person.radius = 0.5f;
        person.center = Vector3.up;
        person.finalAbility.speed = 6f;

        player.getOrAddComponent<PCWASDController>();
        yield return 1;

        player.getOrAddComponent<PlayerSyncPosRotController>();
        yield return 1;

        player.getOrAddComponent<PlayerAreaController>();
        yield return 1;

        if (job == 1)
        {
            player.getOrAddComponent<PlayerFemaleMoveAnimationController>();
            yield return 1;
        }

        player.getOrAddComponent<PlayerMoveController>();
        yield return 1;


        // 设置相机
        {
            WowMainCamera mainCamera = Camera.main.gameObject.getOrAddComponent<WowMainCamera>();
            mainCamera.target = player.transform;
        }
        
    }
}
