using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    private void Update()
    {
        MoveObject();
    }

    private void MoveObject() { transform.position += Vector3.left * moveSpeed * Time.deltaTime; }
}
