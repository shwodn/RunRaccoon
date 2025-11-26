using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    private new Rigidbody2D rigidbody;
    private Vector2 movePosition;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        MoveObject();
    }

    private void MoveObject() 
    {
        movePosition = rigidbody.position + Vector2.left * moveSpeed * Time.deltaTime;
        rigidbody.MovePosition(movePosition); 
    }
}
