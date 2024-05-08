using UnityEngine;

public class ShieldSpawnScript : MonoBehaviour
{
    public GameObject shield;
    public float spawnRate = 2;
    private float timer = 0;
    public float heightOffset = 10;

    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            SpawnShield();
            timer = 0;
        }
    }

    void SpawnShield()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        Instantiate(shield, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}
