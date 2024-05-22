using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject BlueCar;
    public GameObject Bulldozer;
    public GameObject DumpTruck;
    public GameObject motorbikeblack;
    public GameObject MotorBikeRed;
    public GameObject PurpleCar;
    public GameObject YellowCar;

    public GameObject endPanel;
    public GameObject gameOverScene;

    private float[] bestTime = new float[5];
    private float[] rankScore = new float[5];
    public string[] bestName = new string[5];
    public float recordTime;

    public Text[] RankScoreText;
    public Text playerScore;
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
        gameOverScene.SetActive(false);

        gameTime += Time.deltaTime;

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
        float p = Random.Range(0, 10);  //�ֱ������� ���� ������
        if (p < 3) Instantiate(BlueCar);
        else if (p >= 3 && p < 7) Instantiate(PurpleCar);
        else if (p >= 7) Instantiate(YellowCar);

        if(time >= 20) // ������ ������̰� ������
        {
            float r = Random.Range(0, 30);
            if (r < 10) Instantiate(motorbikeblack);
            if (r > 20) Instantiate(MotorBikeRed);
        }
        if(time >= 40) // ������ ����Ʈ���̳� �ҵ����� ������
        {
            float t = Random.Range(0, 30);
            if (t < 5) Instantiate(Bulldozer);
            if (t > 20) Instantiate(DumpTruck);
        }

    }

    //���� ���� ���� ��
    public void GameOver()
    {
        float overTime = 0;
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
        endPanel.SetActive(true);
    }

    void ScoreSet(float currentTime, string currentName)
    {
        //�ϴ� ���翡 �����ϰ� ����
        PlayerPrefs.SetString("currentName", currentName = "chan");
        PlayerPrefs.SetFloat("CurrentPlayerTime", currentTime = recordTime);

        float tmpTime = 0f;
        string tmpName = "";

        for (int i = 0; i < 5; i++)
        {
            //����� �ְ� ������ �̸��� ��������
            bestTime[i] = PlayerPrefs.GetFloat(i + "BestTime");
            bestName[i] = PlayerPrefs.GetString(i + "BestName");

            //���� ������ ��ŷ�� ���� �� ���� ��
            while (bestTime[i] < currentTime)
            {
                //�ڸ� �ٲٱ�
                tmpTime = bestTime[i];
                tmpName = bestName[i];
                bestTime[i] = currentTime;
                bestName[i] = currentName;

                //��ŷ�� ����
                PlayerPrefs.SetFloat(i + "BestScore", currentTime);
                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

                //���� �ݺ��� ���� �غ�
                currentTime = tmpTime;
                currentName = tmpName;
            }
        }

        //��ŷ�� ���� ������ �̸� ����
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetFloat(i + "BestScore", bestTime[i]);
            PlayerPrefs.SetString(i.ToString() + "BestName", bestName[i]);
        }
    }

    //��ŷ ����
    public void Board()
    {
        //�÷��̾��� ���� �ؽ�Ʈ�� ���� '��'�� ������ ǥ��
        playerScore.text = PlayerPrefs.GetString("CurrentPlayerScore");
        

        //��ŷ�� ���� �ҷ��� ���� ǥ��
        for (int i = 0; i < 5; i++)
        {
            rankScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
            RankScoreText[i].text = string.Format("{0:N3}cm", rankScore[i]);

            //��ŷ ���� ǥ��
            if (playerScore.text == RankScoreText[i].text)
            {
                Color Rank = new Color(255, 255, 0);
                playerScore.color = Rank;
                RankScoreText[i].color = Rank;
            }
        }
    }
}
