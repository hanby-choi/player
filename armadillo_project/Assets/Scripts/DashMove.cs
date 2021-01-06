using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{
    public Vector2 speed_vec; 
    public float speed; //이동 속도

    private Rigidbody2D rb;
    public float dashSpeed; //대쉬 속도
    private float dashTime; //대쉬 지속시간
    public float startDashTime; //대쉬 지속시간(초기화 값)
    private int isDash; //대쉬 여부

    SpriteRenderer spriteRenderer; 
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        //애니메이션 - 스프라이트 방향 전환
        if (Input.GetButtonDown("Horizontal")){
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        //이동
        speed_vec.x = Input.GetAxis("Horizontal") * speed;
        speed_vec.y = Input.GetAxis("Vertical") * speed;

        //transform.Translate(speed_vec); 아래 코드 또는 이 코드 사용하여 플레이어 이동
        rb.velocity = speed_vec;

        //대쉬

        if (isDash == 0) //대쉬하고 있지 않음
        {
            if (Input.GetKeyDown(KeyCode.Space)) //스페이스바가 눌렸을 때
            {
                anim.SetBool("isDashing", true); //이동에서 대시로 애니메이션 전환
                isDash = 1; //대시 플래그 변경
            }
        }

        else //대쉬중임
        {
            if (dashTime <= 0) //dashTime이 0보다 작아지면 대쉬 끝
            { 
                isDash = 0; //플래그 초기화
                dashTime = startDashTime; //dashTime 초기화
                rb.velocity = Vector2.zero; //속도 0으로 초기화
                anim.SetBool("isDashing", false); //대시에서 이동으로 애니메이션 변환
            }
            else  //dashTime이 0보다 크면 dashTime감소 및 속도 변화
            {
                dashTime -= Time.deltaTime;

                speed_vec.x = Input.GetAxis("Horizontal") * speed * 3; //speed*3 또는 dashSpeed
                speed_vec.y = Input.GetAxis("Vertical") * speed * 3;

                rb.velocity = speed_vec;
            }
        }

    }
}
