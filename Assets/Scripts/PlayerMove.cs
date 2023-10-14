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
    //앉기/일어나기 버튼
    public Sprite fullChairSprite;
    public Sprite ChairSprite;
    public Button SitBtn; //버튼
    public Text UISitTxt; //버튼 텍스트 (앉는다/일어난다)
    public Color standColor; //일어나다의 버튼 색상  == 파랑
    public Color sitColor; //앉다의 버튼 색상 == 빨강

    private bool isButtonActive = false; //버튼 활성화 여부

    //좌석 sprite 변경
    private bool nearChair = false; //의자 collider와 접촉하고 있는지
    public bool isSitting = false; //앉아있는지 여부
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
            spriter.flipX = joy.Horizontal < 0;  // Right.anim 좌우반전
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //플레이어 rigidbody
        Image buttonImage = SitBtn.GetComponent<Image>(); //앉기/일어나기 버튼 이미지
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item") // Coin이 Item 태그를 갖고 있음
        {
            //코인 먹어서 사라지게 만들기
            collision.gameObject.SetActive(false);
        }

        else if(collision.gameObject.tag == "Finish") //교실문까지 도착하면 시간 멈춰놓기 (추후 다음 레벨로 올라가게끔 구현)
        {
            Time.timeScale = 0;
        }

    }

    /* 좌석 sprite 변경 코드 */

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
        if (collision.CompareTag("Chair")) //CompareTag로 해당 태그와 접촉중인지 확인할 수 있음
        {
            nearChair = false;
        }
        

    }
    /* //좌석 sprite 변경 코드 끝 */

    /* 앉기/일어나기 버튼 코드 */
    public void SitBtnFunc()
    {
        if (nearChair == true)
        {
            // 현재 버튼 상태 반전 (켜기 <-> 끄기)
            isButtonActive = !isButtonActive;
            
            // 버튼 상태에 따라 텍스트 변경
            if (isButtonActive)
            {
                SitBtn.GetComponentInChildren<Text>().text = "일어난다";
                SitBtn.image.color = standColor;//standColor(파란색) 버튼으로 변경하기

                isSitting = true;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; //앉아있는동안 못움직이게 하기
                Color currentColor = spriteRenderer.color;
                currentColor.a = 0f; //알파값 설정 -> 0으로 설정해서 안 보이게 (서있던 sprite를 안 보이게 하고 앉아있는 sprite를 보이게 하기)
                spriteRenderer.color = currentColor;
            }
            else
            {
                SitBtn.GetComponentInChildren<Text>().text = "앉는다";
                SitBtn.image.color = sitColor; //sitColor(빨간색) 버튼으로 변경하기

                isSitting = false;
                rb.constraints &= ~(RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY);
                Color currentColor = spriteRenderer.color;
                currentColor.a = 1f; //알파값 설정 -> 1로 설정해서 다시 보이게
                spriteRenderer.color = currentColor;
            }
        }
        
    }
    /* // 앉기/일어나기 버튼 코드 끝*/

}
