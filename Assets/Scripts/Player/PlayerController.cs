using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float movementSpeed;

    private Vector2 movement;
    private Rigidbody2D rb;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }


    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.deltaTime);
    }

    public void setMovementSpeed(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }
}