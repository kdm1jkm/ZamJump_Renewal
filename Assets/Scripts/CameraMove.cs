using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    
    void Update()
    {
        float delta = (player.transform.position.y - transform.position.y) * 20;
        if (delta < 0)
        {
            // return;
        }   
        transform.position += Vector3.up * (delta * Time.deltaTime);
    }
}
