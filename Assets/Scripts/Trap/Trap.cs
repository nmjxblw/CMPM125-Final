using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Attack
{
    [Header("Trap Basic Parameters")]
    public bool isMove;
    public bool isDamage;
    public bool needCheckPlayerRange;

    [Header("Trap Movement")]
    public bool isVertical;
    public bool isHorizontal;
    public bool isLooping;
    public bool isDestroy;
    public float verDistance;
    public float verSpeed;
    public float horDistance;
    public float horSpeed;
    public float activationDistance = 5f;

    private Vector3 startPosition;
    private Vector3 verDirection;
    private Vector3 horDirection;
    private Vector3 endPosition;
    private bool movingToEnd;

    void Start()
    {
        startPosition = transform.position;
        verDirection = Vector3.up * Mathf.Sign(verDistance);
        horDirection = Vector3.right * Mathf.Sign(horDistance);
        endPosition = startPosition + (isVertical ? verDirection * Mathf.Abs(verDistance) : horDirection * Mathf.Abs(horDistance));
        movingToEnd = true;
        isMove = !needCheckPlayerRange;
    }

    void Update()
    {
        if (needCheckPlayerRange) { CheckPlayerDistance(); }

        if (isMove) { MoveTrap(); }
    }

    private void CheckPlayerDistance()
    {
        if (TransformManager.Instance.currentPlayer != null)
        {
            float distanceX = Mathf.Abs(transform.position.x - TransformManager.Instance.currentPlayer.transform.position.x);

            isMove = distanceX <= activationDistance;
            needCheckPlayerRange = !isMove;
        }
    }

    private void MoveTrap()
    {
        Vector3 targetPosition = movingToEnd ? endPosition : startPosition;
        float currentSpeed = isVertical ? verSpeed : horSpeed;
        Vector3 currentDirection = isVertical ? verDirection : horDirection;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            if (isLooping)
            {
                movingToEnd = !movingToEnd;
            }
            else if (isDestroy)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position = startPosition;
            }
        }
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {
        if (isDamage)
        {
            other.GetComponent<Character>()?.TakeDamage(this);
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }

}
