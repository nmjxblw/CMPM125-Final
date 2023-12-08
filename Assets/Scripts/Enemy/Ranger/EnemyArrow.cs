using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    public float speed;
    public float destoryDistance;
    private Rigidbody2D rb;
    private Vector3 startPos;

    public float rangerDirection;
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(1, 1, 1);
        if (rangerDirection > 0)
        {
            rb.velocity = transform.right * speed;
        }
        else if (rangerDirection < 0)
        {
            rb.velocity = transform.right * -speed;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        startPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(startPos, transform.position);
        if (distance > destoryDistance)
        {
            gameObject.SetActive(false);
        }
    }


}
