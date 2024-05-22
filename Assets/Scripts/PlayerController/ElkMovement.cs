using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElkMovement : MonoBehaviour
{
    private ElkController movementController;
    private Rigidbody2D movementRigidbody;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer angleElkSprite;

    public GameObject angelElk;

    private Vector2 movementDirection = Vector2.zero;

    private void Awake()
    {
        movementController = GetComponent<ElkController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        angleElkSprite = angelElk.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        movementController.OnMoveEvent += Move;
    }

    // ����ϰ� ȭ�� ������ �ȳ������� ���� ����
    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (x > 2.0f)
        {
            x = 2.0f;
        }
        else if (x < -2)
        {
            x = -2.0f;
        }

        if (y > 4.0f)
        {
            y = 4.0f;
        }
        else if (y < -4.0f)
        {
            y = -4.0f;
        }
        transform.position = new Vector2(x, y);

        if(GameManager.Instance.isItem1Active)
        {
            spriteRenderer.enabled = false;
            angelElk.SetActive(true);
        }
        else if(!GameManager.Instance.isItem1Active)
        {
            spriteRenderer.enabled = true;
            angelElk.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement(movementDirection);
    }

    private void Move(Vector2 direction)
    {
        movementDirection = direction;

        if(direction.x < 0)
        {
            spriteRenderer.flipX = true;
            angleElkSprite.flipX = true;
        }
        else if(direction.x > 0)
        {
            spriteRenderer.flipX = false;
            angleElkSprite.flipX = false;
        }
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * 3;
        movementRigidbody.velocity = direction;
    }
}
