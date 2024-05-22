using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpTruck : MonoBehaviour
{
    float downSpeed = 0.1f;
    float upSpeed = 0.05f;

    private bool isRotation = false;
    private float RotateSpeed = 360f;

    SpriteRenderer spriteRenderer;

    // �ڵ����� ������ ��ġ
    void Start()
    {
        float x = Random.Range(-2.0f, 2.0f);
        float y = 5.5f;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (x < -1.35f && x >= -2.0f)
        {
            x = -1.95f;
            transform.position = new Vector2(x, y);
        }
        else if (x < 0.0f && x >= -1.35f)
        {
            x = -0.75f;
            transform.position = new Vector2(x, y);
        }
        else if (x < 1.35f && x >= 0.0f)
        {
            x = 0.75f;
            transform.position = new Vector2(x, -y);
            //�ڵ����� ���������� �ö� �ڵ����� ���� ��ȯ+����� �׸��� ���� ���� ��
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
            spriteRenderer.flipX = true;

        }
        else if (x <= 2.0f && x >= 1.35f)
        {
            x = 1.95f;
            transform.position = new Vector2(x, -y);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
            spriteRenderer.flipX = true;
        }
    }

    // ���� ����� ������ ���� ��
    void Update()
    {
        //���� ������ ��ġ�� ���� ������
        if (transform.position.x < 0.0f && transform.position.x >= -2.0f)
        {
            transform.position += Vector3.down * downSpeed;
        }
        else if (transform.position.x >= 0.0f && transform.position.x <= 2.0f)
        {
            transform.position += Vector3.up * upSpeed;
        }

        //���� ȭ�� ������ ������ ����
        if (transform.position.y < -8.0f)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y > 8.0f)
        {
            Destroy(gameObject);
        }

        if (isRotation == true)
        {
            transform.Rotate(Vector3.forward, RotateSpeed * Time.deltaTime);
        }
    }

    //���� �����(�÷��̾�)�� ���� ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.Instance.isItem1Active)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (GameManager.Instance.item3Hav >= 1)
                {
                    GameManager.Instance.item3Hav -= 1;
                    DestroyCar();
                }

                else
                {
                    GameManager.Instance.GameOver();
                }
            }
        }

        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                DestroyCar();
            }
        }
    }

    private void DestroyCar()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Collider2D collider = GetComponent<Collider2D>();

        Destroy(collider);

        rb.gravityScale = 2.0f;

        Vector2 initialVelocity = new Vector2(Random.Range(-2.0f, 2.0f), Random.Range(6.0f, 10.0f));
        rb.AddForce(initialVelocity, ForceMode2D.Impulse);

        isRotation = true;
    }
}
