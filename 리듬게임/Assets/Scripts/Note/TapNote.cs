using UnityEngine;

public class TapNote : BaseNote
{
    [SerializeField]
    private float spawnY = 600f;

    [SerializeField]
    private float judgeY = -350f;

    [SerializeField]
    private float travelTime = 2f;

    private void Update()
    {
        Move();
    }

    public override void Move()
    {
        float spawnTime = Data.time - travelTime;

        float t =
            (musicManager.CurrentTime - spawnTime)
            / travelTime;

        transform.position = Vector3.Lerp(
            new Vector3(transform.position.x, spawnY),
            new Vector3(transform.position.x, judgeY),
            t);

        if (t > 1.2f)
        {
            ReturnPool();
        }
    }

    public override void Judge()
    {

    }
}