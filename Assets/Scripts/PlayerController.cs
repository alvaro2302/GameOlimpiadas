using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float forceJump = 6f;
    public float forceRun = 1f;
    public float distanceDash = 10f;
    public float speedDash = 50f;
    [SerializeField] private Transform PfDashEffect;
    private float moveX = 0f;
    private Rigidbody2D rigidbodyPlayer;
    public LayerMask maskGround;
    Animator animator;
    Transform player;


    private const string IS_GROUND = "IsGround";
    private const string IS_ALIVE = "IsAlive";
    private const string IS_RUN = "IsRun";




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
        animator.SetBool(IS_RUN, false);



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {


            Jump();

        }
        animator.SetBool(IS_GROUND, IsGround());
        animator.SetBool(IS_RUN, IsRun());
      


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
           ;
            Run();


        }
       
        if (transform.localScale.x > 0)
        {
            moveX = 1f;

        }
        else
        {
            moveX = -1f;
        }

        followCamera();
        Vector3 beforePosition = transform.position;
        if (Input.GetKeyDown(KeyCode.K))
        {
            Transform dashedBefore=handleDash(beforePosition);
            StartCoroutine("deleteDashObject", dashedBefore);
        }




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
                
                if (transform.localScale.x > 0)
                {
                    rigidbodyPlayer.velocity = new Vector2(forceRun, rigidbodyPlayer.velocity.y);
                }
                else
                {
                    rigidbodyPlayer.velocity = new Vector2(forceRun * -1, rigidbodyPlayer.velocity.y);
                }


            }   

    }

        bool IsGround()
        {
           
            return (Physics2D.Raycast(transform.position, Vector2.down, 0.15f, maskGround));

        }

        void followCamera()
        {
            Vector3 positionCameraNow = Camera.main.transform.position;
            Vector3 positionCharacter = new Vector3(transform.position.x, transform.position.y, -20);
            Camera.main.transform.position = Vector3.Lerp(positionCameraNow, positionCharacter, 0.05f);
        }

        Transform handleDash(Vector3 beforePosition)
        {
            Vector3 moveDir = new Vector3(moveX, 0).normalized;
            transform.position += moveDir * speedDash * Time.deltaTime;
            Transform dashEffectTransform = Instantiate(PfDashEffect, beforePosition, Quaternion.identity);
            dashEffectTransform.gameObject.active = true;
            
            dashEffectTransform.eulerAngles = new Vector3(0, 0, 0);
            float dashEffectWidth = 3f;
            dashEffectTransform.localScale = new Vector3(distanceDash / dashEffectWidth, 0.3f, 1f);
            return dashEffectTransform;

        }
       
    bool IsRun()
    {
        return (((int) System.Math.Ceiling(rigidbodyPlayer.velocity.x)) !=0);
    }

    IEnumerator deleteDashObject(Transform Afterdash)
    {
        yield return new WaitForSeconds(1);
        Destroy(Afterdash.gameObject);

    }

}
