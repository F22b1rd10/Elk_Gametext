using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject BlueCar;
    public GameObject DumpTruck;
    public GameObject motorbikeblack;
    public GameObject MotorBikeRed;
    public GameObject PurpleCar;
    public GameObject YellowCar;

    public GameObject endPanel;
    public GameObject UiBar;

    public float recordTime;

    public Text timeTxt;
    public Text NowScore;
    public Text BestScore;

    public bool isItem1Active = false;
    public bool isItem2Active = false;

    public int item1Hav = 1;
    public int item2Hav = 1;
    public int item3Hav = 1;

    bool isPlay = true;

    float gameTime = 0f;
    float time = 0f;
    string key = "BestScore";

    private void Awake()
    {
        if (Instance == null)
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        Application.targetFrameRate = 60;
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        endPanel.SetActive(false);
        UiBar.SetActive(true);

        InvokeRepeating("MakeCar", 0.0f, 2.0f); //�ڵ����� �������� ���´�.
    }

    void Update()
    {
        if (isPlay)
        {
            time += Time.deltaTime;
            timeTxt.text = time.ToString("N2");
        }
    }

    //�ڵ��� ȣ���ϴ� �޼���
    void MakeCar()
    {
        float p = Random.Range(0, 35);  //�ֱ������� ���� ������
        if (p <= 10) Instantiate(BlueCar);
        else if (p > 10 && p <= 20) Instantiate(PurpleCar);
        else if (p > 20 && p <= 30) Instantiate(YellowCar);
        else if (p > 30) Instantiate(DumpTruck);

        if (time >= 20) // ������ ������̰� ������
        {
            float r = Random.Range(0, 30);
            if (r < 10) Instantiate(motorbikeblack);
            if (r > 20) Instantiate(MotorBikeRed);
        }
    }

    //���� ���� ���� ��
    public void GameOver()
    {
        isPlay = false;
        Time.timeScale = 0.0f; //���� ���� ó��
        NowScore.text = time.ToString("N2"); //��ƾ �ð� ��ŭ ���� ��Ͽ� ǥ��

        //�ְ� ������ �ִٸ�
        if (PlayerPrefs.HasKey(key))
        {
            float best = PlayerPrefs.GetFloat(key);
            if (best < time) //���� ������ �ְ� �������� ���� ���
            {
                PlayerPrefs.SetFloat(key, time); //���� ������ �ְ� ������ ����
                BestScore.text = time.ToString("N2"); //���� ������ �ְ� ������ �����ش�.
            }
            else //���� ������ �ְ� �������� ���� ��� (���� ����)
            {
                BestScore.text = best.ToString("N2"); //����� �ְ� ������ �����ش�.
            }
        }
        //�ְ� ������ ���ٸ�
        else
        {
            PlayerPrefs.SetFloat(key, time); //���� ������ �ְ� ������ ����
            BestScore.text = time.ToString("N2"); //���� ������ �ְ� ������ �����ش�.
        }

        //�÷��̾��� �ð��� ����
        PlayerPrefs.SetFloat("recordTime", time);

        //������ ����Ǹ� ���� �ǳ� Ȱ��ȭ   
        UiBar.SetActive(false);
        endPanel.SetActive(true);
    }
}
