using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeReference]
    private GameInput gameInput;

    private bool isWalking;

    // Start is called before the first frame update
    private void Start() {
        
    }

    // Update is called once per frame
    private void Update() {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // Get a vector of the player's movement
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        isWalking = moveDirection != Vector3.zero;
        
        // Move the player
        transform.position += moveSpeed * Time.deltaTime * moveDirection;

        // Rotate the player
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
    }

    public bool IsWalking() {
        return isWalking;
    }
}
