using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float forceJump = 6f;
    public float forceRun = 1f;
    private Rigidbody2D rigidbodyPlayer;
    public LayerMask maskGround;
    Animator animator;
    Transform player;
  

    private const string IS_GROUND = "IsGround";
    private const string IS_ALIVE = "IsAlive";
  



    void Awake()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<Transform>();
    }
    void Start()
    {
        animator.SetBool(IS_ALIVE, true);
        animator.SetBool(IS_GROUND, false);
         


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {


            Jump();

        }
        animator.SetBool(IS_GROUND, IsGround());



        Debug.DrawRay(transform.position, Vector3.down * 0.15f, Color.black);

    }

    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(1, 1, 1);
            Run();

        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Run();


        }
        followCamera();
    }

    void Jump()
    {
        if (IsGround())
        {
            rigidbodyPlayer.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
        }


    }
    void Run()
    {
        if (IsGround())
        {
            if(transform.localScale.x >0)
            {
                rigidbodyPlayer.velocity = new Vector2(forceRun , rigidbodyPlayer.velocity.y);
            }
            else
            {
                rigidbodyPlayer.velocity = new Vector2(forceRun*-1, rigidbodyPlayer.velocity.y);
            }


        }

    }

    bool IsGround()
    {
        return (Physics2D.Raycast(transform.position, Vector2.down, 0.15f, maskGround));

    }

    void followCamera()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -20);
    }
}
