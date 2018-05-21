using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStars : MonoBehaviour
{
    [SerializeField]
    private float minSpawnTime;
    [SerializeField]
    private float maxSpawnTime;
    [Space]
    [SerializeField]
    private Vector2 UpperLeftSpawnPoint;
    [SerializeField]
    private Vector2 LowerRightSpawnPoint;
    [Space]
    [SerializeField]
    private float minAngle;
    [SerializeField]
    private float maxAngle;
    [Space]
    [SerializeField]
    private float speed;

    private void Start()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private IEnumerator RespawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            Vector2 pos = new Vector2(
                Random.Range(UpperLeftSpawnPoint.x, LowerRightSpawnPoint.x), 
                Random.Range(LowerRightSpawnPoint.y, UpperLeftSpawnPoint.y));
            transform.position = pos;
            transform.rotation = Quaternion.AngleAxis(Random.Range(minAngle, maxAngle), Vector3.forward);
        }
    }
}
