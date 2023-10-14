using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TeacherMove : MonoBehaviour
{
    public PlayerMove player;
    private Animator anim;
    private float timeCount = 0.0f;
    private bool isCounting = true;
    private float targetTime = 0.0f; // 목표 카운트 다운 시간

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount >= 5.0f) // 5초에 한 번씩 뒤돌아보기
        {
            anim.SetBool("IsTurning", true);
            player.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; //못움직이게 고정

            if (player.isSitting) //플레이어가 앉아있으면
            {
                targetTime = 1.0f; // 1초 카운트 다운 설정
            }
            else //플레이어가 서있으면
            {
                targetTime = 3.0f; // 3초 카운트 다운 설정
            }

            isCounting = true; // 카운트 다운 시작
            timeCount = 0.0f; // 교수님이 뒤돌아보는 시간 초기화
        }

        if (isCounting)
        {
            targetTime -= Time.deltaTime; // 1초 혹은 3초동안 카운트다운

            if (targetTime <= 0.0f)
            {
                anim.SetBool("IsTurning", false); // 뒤돌아보기 애니메이션 비활성화 (교수님)
                isCounting = false; // 카운트 다운 종료
                timeCount = 0.0f; // 시간 초기화
                player.rb.constraints &= ~(RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY); //움직임 고정 해제

            }
        }
    }
}
