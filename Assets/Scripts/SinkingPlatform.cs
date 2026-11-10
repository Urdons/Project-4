using UnityEngine;

public class SinkingPlatform : MonoBehaviour
{
    public GameObject platform;
    private Vector3 position;
    private Vector3 offset = Vector3.zero;
    private bool touchingPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = platform.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(touchingPlayer)
        {
            offset.y -= Time.deltaTime * 0.5f;
        }
        else
        {
            offset.y += -offset.y * 0.01f;
        }

        platform.transform.position = position + offset;
    }

    void OnTriggerEnter(Collider other)
    {
        touchingPlayer = true;
    }

    void OnTriggerExit(Collider other)
    {
        touchingPlayer = false;
    }
}
