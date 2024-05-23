using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulldozer : MonoBehaviour
{
    float downSpeed = 0.02f;
    float upSpeed = 0.01f;

    SpriteRenderer spriteRenderer;

    // 자동차가 나오는 위치
    void Start()
    {
        float x = Random.Range(-2.0f, 2.0f);
        float y = 5.5f;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (x < 0.0f && x >= -2.0f)
        {
            x = -1.4f;
            transform.position = new Vector2(x, y);
        }
        else if (x < 2.0f && x >= 0.0f)
        {
            x = 1.3f;
            transform.position = new Vector2(x, -y);
            //오토바이가 역방향으로 올때 자동차의 방향 전환+뒤집어서 그림자 방향 맞춰 줌
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
            spriteRenderer.flipX = true;

        }
    }

    // 차가 고라니 쪽으로 주행 중
    void Update()
    {
        //차가 나오는 위치에 따라 정주행
        if (transform.position.x < 0.0f && transform.position.x >= -2.0f)
        {
            transform.position += Vector3.down * downSpeed;
        }
        else if (transform.position.x >= 0.0f && transform.position.x <= 2.0f)
        {
            transform.position += Vector3.up * upSpeed;
        }

        //차가 화면 밖으로 나가면 삭제
        if (transform.position.y < -8.0f)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y > 8.0f)
        {
            Destroy(gameObject);
        }
    }

    //차가 고라니(플레이어)를 쳤을 경우
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //게임 종료 메서드 실행
            GameManager.Instance.GameOver();
        }
    }
}
