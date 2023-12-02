using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Attack
{
    [Header("Trap Basic Parameters")]
    public bool isMove;
    public bool isDamage;

    [Header("Trap Movement")]
    public bool isVertical;
    public bool isHorizontal;
    public bool isLooping;
    public float verDistance;
    public float verSpeed;
    public float horDistance;
    public float horSpeed;

    private Vector3 startPosition;
    private Vector3 verDirection;
    private Vector3 horDirection;
    private Vector3 endPosition;
    private bool movingToEnd;

    void Start()
    {
        startPosition = transform.position;
        verDirection = Vector3.up * Mathf.Sign(verDistance); // 垂直移动方向
        horDirection = Vector3.right * Mathf.Sign(horDistance); // 水平移动方向
        endPosition = startPosition + (isVertical ? verDirection * Mathf.Abs(verDistance) : horDirection * Mathf.Abs(horDistance));
        movingToEnd = true; // 初始设置为移动到终点
    }

    void Update()
    {
        MoveTrap();
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
                movingToEnd = !movingToEnd; // 往复移动时改变方向
            }
            else
            {
                transform.position = startPosition; // 非往复移动时立即返回起点
            }
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (isDamage) { 
            other.GetComponent<Character>()?.TakeDamage(this);
        }
       
    }

}
