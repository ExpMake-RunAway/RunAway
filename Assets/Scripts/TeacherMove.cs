using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TeacherMove : MonoBehaviour
{
    public PlayerMove player;
    private Animator anim;
    private float timeCount = 0.0f;
    private bool isCounting = true;
    private float targetTime = 0.0f; // ��ǥ ī��Ʈ �ٿ� �ð�

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount >= 5.0f) // 5�ʿ� �� ���� �ڵ��ƺ���
        {
            anim.SetBool("IsTurning", true);
            player.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; //�������̰� ����

            if (player.isSitting) //�÷��̾ �ɾ�������
            {
                targetTime = 1.0f; // 1�� ī��Ʈ �ٿ� ����
            }
            else //�÷��̾ ��������
            {
                targetTime = 3.0f; // 3�� ī��Ʈ �ٿ� ����
            }

            isCounting = true; // ī��Ʈ �ٿ� ����
            timeCount = 0.0f; // �������� �ڵ��ƺ��� �ð� �ʱ�ȭ
        }

        if (isCounting)
        {
            targetTime -= Time.deltaTime; // 1�� Ȥ�� 3�ʵ��� ī��Ʈ�ٿ�

            if (targetTime <= 0.0f)
            {
                anim.SetBool("IsTurning", false); // �ڵ��ƺ��� �ִϸ��̼� ��Ȱ��ȭ (������)
                isCounting = false; // ī��Ʈ �ٿ� ����
                timeCount = 0.0f; // �ð� �ʱ�ȭ
                player.rb.constraints &= ~(RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY); //������ ���� ����

            }
        }
    }
}
