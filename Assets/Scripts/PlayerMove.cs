using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;
using UnityEngine.XR;

public class PlayerMove : MonoBehaviour
{
    //�ɱ�/�Ͼ�� ��ư
    public Sprite fullChairSprite;
    public Sprite ChairSprite;
    public Button SitBtn; //��ư
    public Text UISitTxt; //��ư �ؽ�Ʈ (�ɴ´�/�Ͼ��)
    public Color standColor; //�Ͼ���� ��ư ����  == �Ķ�
    public Color sitColor; //�ɴ��� ��ư ���� == ����

    private bool isButtonActive = false; //��ư Ȱ��ȭ ����

    //�¼� sprite ����
    private bool nearChair = false; //���� collider�� �����ϰ� �ִ���
    public bool isSitting = false; //�ɾ��ִ��� ����
    SpriteRenderer spriteRenderer;
    public Sprite sittedSprite;
    public Sprite notSittedSprite;

    // Player Speed
    [SerializeField]
    private float Speed = 3;

    public VariableJoystick joy;
    public Rigidbody2D rb;
    SpriteRenderer spriter;
    Vector2 moveVec;
    Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // 1. Input Value
        float x = joy.Horizontal;
        float y = joy.Vertical;

        // Debug.Log($"hJoy: {x}, vJoy: {y}"); 

        // 2. Move Position
        moveVec = new Vector2(x, y) * Speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVec);

    }
    void LateUpdate()
    {
        if (joy.Horizontal != 0)
        {
            spriter.flipX = joy.Horizontal < 0;  // Right.anim �¿����
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //�÷��̾� rigidbody
        Image buttonImage = SitBtn.GetComponent<Image>(); //�ɱ�/�Ͼ�� ��ư �̹���
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item") // Coin�� Item �±׸� ���� ����
        {
            //���� �Ծ ������� �����
            collision.gameObject.SetActive(false);
        }

        else if(collision.gameObject.tag == "Finish") //���ǹ����� �����ϸ� �ð� ������� (���� ���� ������ �ö󰡰Բ� ����)
        {
            Time.timeScale = 0;
        }

    }

    /* �¼� sprite ���� �ڵ� */

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Chair")//==collision.CompareTag("Chair")
        {
            nearChair = true;
        }
        if (collision.gameObject.tag == "Chair"&&isSitting == true)
        {
            SpriteRenderer colSpriteRenderer = collision.GetComponent<SpriteRenderer>();
            colSpriteRenderer.sprite = sittedSprite;
        }
        if (collision.gameObject.tag == "Chair" && isSitting == false)
        {
            SpriteRenderer colSpriteRenderer = collision.GetComponent<SpriteRenderer>();
            colSpriteRenderer.sprite = notSittedSprite;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Chair")) //CompareTag�� �ش� �±׿� ���������� Ȯ���� �� ����
        {
            nearChair = false;
        }
        

    }
    /* //�¼� sprite ���� �ڵ� �� */

    /* �ɱ�/�Ͼ�� ��ư �ڵ� */
    public void SitBtnFunc()
    {
        if (nearChair == true)
        {
            // ���� ��ư ���� ���� (�ѱ� <-> ����)
            isButtonActive = !isButtonActive;
            
            // ��ư ���¿� ���� �ؽ�Ʈ ����
            if (isButtonActive)
            {
                SitBtn.GetComponentInChildren<Text>().text = "�Ͼ��";
                SitBtn.image.color = standColor;//standColor(�Ķ���) ��ư���� �����ϱ�

                isSitting = true;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; //�ɾ��ִµ��� �������̰� �ϱ�
                Color currentColor = spriteRenderer.color;
                currentColor.a = 0f; //���İ� ���� -> 0���� �����ؼ� �� ���̰� (���ִ� sprite�� �� ���̰� �ϰ� �ɾ��ִ� sprite�� ���̰� �ϱ�)
                spriteRenderer.color = currentColor;
            }
            else
            {
                SitBtn.GetComponentInChildren<Text>().text = "�ɴ´�";
                SitBtn.image.color = sitColor; //sitColor(������) ��ư���� �����ϱ�

                isSitting = false;
                rb.constraints &= ~(RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY);
                Color currentColor = spriteRenderer.color;
                currentColor.a = 1f; //���İ� ���� -> 1�� �����ؼ� �ٽ� ���̰�
                spriteRenderer.color = currentColor;
            }
        }
        
    }
    /* // �ɱ�/�Ͼ�� ��ư �ڵ� ��*/

}
