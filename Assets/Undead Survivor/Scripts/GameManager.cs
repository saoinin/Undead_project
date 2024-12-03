using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTime;
    public float maxGacmeTime = 2 * 10f;
    public PoolManager pool;
    public Player player;

    private void Awake()
    {
        instance = this;
        
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGacmeTime)
        {
            gameTime = maxGacmeTime;
        }
    }

}
