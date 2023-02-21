using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjects : MonoBehaviour
{
    [SerializeField] GameObject propsParent;
    bool isCollided;
    GameObject player;

    void Update()
    {
        if(isCollided)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.SetParent(player.transform);
            } else if (Input.GetKeyUp(KeyCode.E))
            {
                isCollided = false;
                transform.SetParent(propsParent.transform);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isCollided = true;
            player = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollided = false;
        }
    }
}
