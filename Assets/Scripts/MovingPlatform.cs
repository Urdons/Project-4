using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 direction;
    public float distance;
    public float speed;

    private Vector3 position;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = new Vector3(direction.x, 0, direction.y) * MathF.Sin(Time.realtimeSinceStartup * speed) * distance;

        transform.position = position + offset;
    }
}
