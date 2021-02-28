using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float delta = (player.transform.position.y - transform.position.y) * 20;
        if (delta < 0)
        {
            return;
        }   
        transform.position += Vector3.up * (delta * Time.deltaTime);
    }
}
