using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    //버튼 및 스크립트
    public GameObject HowtoBtn; //게임방법 버튼 -> 기획에는 없지만 만들어놓음 나중에 지워도됨
    public GameObject StartBtn; //게임시작 버튼
    public GameObject RetryBtn; //재시작 버튼 (게임오버 시 나타남)
    public PlayerMove player; //PlayerMove 스크립트 가져옴

    //시간 UI
    public Text UItime; //1분 시간 카운트다운 텍스트
    private float totalTime = 60.0f; // 총 시간 (60초==1분)
    private float currentTime; //카운트다운되는 시간

    // 의자 및 학생 랜덤 생성
    public GameObject Student;
    public GameObject Desk;
    GameObject deskObject;

    public bool[,] map1 = new bool[16, 7];
    public int rowOfMap = 16;
    public int colOfMap = 7;
    public int numberOfTrueTiles = 30;

    public void Start()
    {
        currentTime = totalTime; //시작할 때는 60초로 시작

        GenerateRandomTiles(); //랜덤 타일 생성
        SetChair(); //16 * 7 책상 배열 함수
    }
    public void Retry()//RetryBtn과 연결되어있음
    {
        SceneManager.LoadScene("Play"); //재시작 버튼 누르면 Play씬 로드
    }
    public void OnStart() //StartBtn과 연결되어있음
    {
        SceneManager.LoadScene("Play"); //게임시작 버튼 눌렀을 때 Play씬 로드
    }

    public void HowTo() //HowtoBtn과 연결되어있음
    {
        SceneManager.LoadScene("Howto"); //게임방법 버튼 눌렀을 때 HowTo씬 로드 //게임방법씬은 아직 없음
    }

    private void Update() //1초에 60프레임씩 업데이트됨
    {
        if (currentTime <= 0) //00:00:00되면 게임오버 == 1분 안에 교실밖으로 못 나가면 게임오버
        {
            SceneManager.LoadScene("GameOver"); // 게임오버씬 로드
        }
        else
        {
            currentTime -= Time.deltaTime; // 시간 감소
            UpdateTimeText(); //UI시간 업데이트 함수
        }
    }

    private void UpdateTimeText() //UI시간 업데이트 함수
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        int milliseconds = Mathf.FloorToInt((currentTime * 100) % 100);

        UItime.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds); //int를 string으로 변환해서 UI에 나타내기
    }

    /* 학생(이 앉아있는 책상) 랜덤 생성 */
    void GenerateRandomTiles()
    {
        int totalTiles = map1.GetLength(0) * map1.GetLength(1);
        if (numberOfTrueTiles > totalTiles)
        {
            numberOfTrueTiles = totalTiles;
        }

        int remainingTrueTiles = numberOfTrueTiles;

        for (int x = 0; x < map1.GetLength(0); x++)
        {
            for (int y = 0; y < map1.GetLength(1); y++)
            {
                if (UnityEngine.Random.Range(0, totalTiles) < remainingTrueTiles)
                {
                    map1[x, y] = true;
                    remainingTrueTiles--;
                }
                totalTiles--;
            }
        }
    }

    void SetChair()
    { // 16 * 7 책상 배치
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (map1[i, j] == true)
                { // 1이라면 Prefab1
                    deskObject = Instantiate(Student);
                    deskObject.transform.position = new Vector2(i * 3, j * (-3f) - 1f);
                }
                else if (map1[i, j] == false)
                { // 0이라면 Prefab2
                    deskObject = Instantiate(Desk);
                    deskObject.transform.position = new Vector2(i * 3, j * (-3f) - 1f);
                }
            }
        }
    }

    /* // 학생 랜덤 생성 끝 */
}
