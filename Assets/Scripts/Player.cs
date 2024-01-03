using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float rotationSpeed = 10f;

    // Start is called before the first frame update
    private void Start() {
        
    }

    // Update is called once per frame
    private void Update() {

        Vector2 inputVector = new Vector2(0,0);
        
        if (Input.GetKey(KeyCode.W)) {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x = +1;
        }

        // Normalize so that player moves at the same speed in a diagonal
        inputVector = inputVector.normalized;

        // Get a vector of the player's movement
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        
        // Move the player
        transform.position += moveSpeed * Time.deltaTime * moveDirection;

        // Rotate the player
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);

    }
}
