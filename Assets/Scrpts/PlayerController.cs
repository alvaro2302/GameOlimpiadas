using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float forceJump = 6f;
    private Rigidbody2D rigidbodyPlayer;
    void Awake()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {

            Jump();

        }
    }

    void Jump()
    {

        rigidbodyPlayer.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
    }
}
