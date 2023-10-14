using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class seatChange : MonoBehaviour
{
    public PlayerMove player;
    public GameManager gameManager;

    public SpriteRenderer spriteRenderer;
    public Sprite fullChair;
    public Sprite chair;
    private bool isSitting = false; // �̹� �ɾҴ��� ���θ� Ȯ���ϱ� ���� �÷���
    private bool nearChair = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Chair")
        {
            nearChair = true;
            if (isSitting == true)
            {
                SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();

                spriteRenderer.sprite = fullChair;
                gameObject.SetActive(false);
            }
            else if (isSitting == false)
            {
                spriteRenderer.sprite = chair;
            }
        }
    }

    public void SitOnClick()
    {
        if (nearChair == true)
        {
            isSitting = true;
        }
    }

    public void StandOnClick()
    {
        isSitting = false;
        gameObject.SetActive(true);
    }
}
