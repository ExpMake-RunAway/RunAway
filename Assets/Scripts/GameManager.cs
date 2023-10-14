using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    //��ư �� ��ũ��Ʈ
    public GameObject HowtoBtn; //���ӹ�� ��ư -> ��ȹ���� ������ �������� ���߿� ��������
    public GameObject StartBtn; //���ӽ��� ��ư
    public GameObject RetryBtn; //����� ��ư (���ӿ��� �� ��Ÿ��)
    public PlayerMove player; //PlayerMove ��ũ��Ʈ ������

    //�ð� UI
    public Text UItime; //1�� �ð� ī��Ʈ�ٿ� �ؽ�Ʈ
    private float totalTime = 60.0f; // �� �ð� (60��==1��)
    private float currentTime; //ī��Ʈ�ٿ�Ǵ� �ð�

    // ���� �� �л� ���� ����
    public GameObject Student;
    public GameObject Desk;
    GameObject deskObject;

    public bool[,] map1 = new bool[16, 7];
    public int rowOfMap = 16;
    public int colOfMap = 7;
    public int numberOfTrueTiles = 30;

    public void Start()
    {
        currentTime = totalTime; //������ ���� 60�ʷ� ����

        GenerateRandomTiles(); //���� Ÿ�� ����
        SetChair(); //16 * 7 å�� �迭 �Լ�
    }
    public void Retry()//RetryBtn�� ����Ǿ�����
    {
        SceneManager.LoadScene("Play"); //����� ��ư ������ Play�� �ε�
    }
    public void OnStart() //StartBtn�� ����Ǿ�����
    {
        SceneManager.LoadScene("Play"); //���ӽ��� ��ư ������ �� Play�� �ε�
    }

    public void HowTo() //HowtoBtn�� ����Ǿ�����
    {
        SceneManager.LoadScene("Howto"); //���ӹ�� ��ư ������ �� HowTo�� �ε� //���ӹ������ ���� ����
    }

    private void Update() //1�ʿ� 60�����Ӿ� ������Ʈ��
    {
        if (currentTime <= 0) //00:00:00�Ǹ� ���ӿ��� == 1�� �ȿ� ���ǹ����� �� ������ ���ӿ���
        {
            SceneManager.LoadScene("GameOver"); // ���ӿ����� �ε�
        }
        else
        {
            currentTime -= Time.deltaTime; // �ð� ����
            UpdateTimeText(); //UI�ð� ������Ʈ �Լ�
        }
    }

    private void UpdateTimeText() //UI�ð� ������Ʈ �Լ�
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        int milliseconds = Mathf.FloorToInt((currentTime * 100) % 100);

        UItime.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds); //int�� string���� ��ȯ�ؼ� UI�� ��Ÿ����
    }

    /* �л�(�� �ɾ��ִ� å��) ���� ���� */
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
    { // 16 * 7 å�� ��ġ
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (map1[i, j] == true)
                { // 1�̶�� Prefab1
                    deskObject = Instantiate(Student);
                    deskObject.transform.position = new Vector2(i * 3, j * (-3f) - 1f);
                }
                else if (map1[i, j] == false)
                { // 0�̶�� Prefab2
                    deskObject = Instantiate(Desk);
                    deskObject.transform.position = new Vector2(i * 3, j * (-3f) - 1f);
                }
            }
        }
    }

    /* // �л� ���� ���� �� */
}
