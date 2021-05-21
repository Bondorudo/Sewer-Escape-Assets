using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D col;
    private GameManager gameManager;
    public Trajectory trajcetory;
    private Camera cam;

    private bool isTouchingGround = true;
    private bool isDragging = false;

    [SerializeField] private float pushForce = 4f;
    [SerializeField] private float distance;
    [SerializeField] private float maxDistance = 30;
    [SerializeField] private float playerHealth = 10;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;

    public Vector3 playerPos
    {
        get { return transform.position; }
    }


    private void Start()
    {
        cam = Camera.main;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();   
    }

    private void Update()
    {
        if (gameManager.pauseGame == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                OnDragStart();
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                OnDragEnd();
            }
            if (isDragging)
            {
                OnDrag();
            }
        }
        if (gameManager.hasPressedContinue == true)
        {
            force = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            gameManager.hasPressedContinue = false;
        }
    }

    public void Push(Vector2 force)
    {
        if (isTouchingGround == true && gameManager.pauseGame == false)
        {
           rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    public void ActivateRb()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.isKinematic = false;
    }

    public void DeActivateRb()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
    }

    public void OnDragStart()
    {
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        trajcetory.Show();
    }

    public void OnDrag()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;

        if (distance >= maxDistance)
        {
            distance = maxDistance;
        }

        force = direction * distance * pushForce;

        Debug.DrawLine(startPoint, endPoint);

        trajcetory.UpdateDots(playerPos, force);
    }

    public void OnDragEnd()
    {
        ActivateRb();

        Push(force);
        trajcetory.Hide();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        isTouchingGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouchingGround = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "spike")
        {
            playerHealth -= playerHealth;
            gameManager.GameOver();
        }

        if (collision.tag == "Goal")
        {
            gameManager.Victory();
            gameManager.isTouchingGoal = true;
        }
    }
}
