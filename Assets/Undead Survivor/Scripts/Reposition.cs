using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    private void Awake()
    {
        coll= GetComponent<Collider2D>();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        //float dirX = playerPos.x - myPos.x;
        //float dirY = playerPos.y - myPos.y;

        float diffx = Mathf.Abs(playerPos.x - myPos.x);
        float diffy = Mathf.Abs(playerPos.y - myPos.y);
        Vector3 playerDir = GameManager.instance.player.inputVec;
        //dirX = dirX > 0 ? 1 : -1;
        //dirY = dirY > 0 ? 1 : -1;

        float dirX = playerDir.x > 0 ? 1 : -1;
        float dirY=  playerDir.y > 0 ? 1 : -1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffx > diffy)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffx < diffy)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    transform.Translate(playerDir);
                }
                break;
        }

    }

}