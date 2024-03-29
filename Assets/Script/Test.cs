using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (player.transform.position - transform.position);
        transform.rotation = Quaternion.FromToRotation(Vector2.left, dir);
    }
}
