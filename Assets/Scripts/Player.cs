using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private float playerSize = .7f;

    [SerializeField]
    private float playerHeight = 2f;

    [SerializeReference]
    private GameInput gameInput;

    private bool isWalking;

    // Start is called before the first frame update
    private void Start() {
        
    }

    // Update is called once per frame
    private void Update() {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        float moveDistance = moveSpeed * Time.deltaTime;

        // Get a vector of the player's movement and cast a ray in the player movement direction to see if there are any objects in that direction
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, moveDirection, moveDistance);

        if (!canMove) {
            // Attempt moving in X direction.
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, moveDirectionX, moveDistance);
            
            if (canMove) {
                moveDirection = moveDirectionX;
            } else {
                // Attempt moving in X direction.
                Vector3 moveDirectionZ = new Vector3(0f, 0f, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, moveDirectionZ, moveDistance);

                if (canMove) {
                    moveDirection = moveDirectionZ;
                }
            }
        }

        // Move the player
        if (canMove) {
            transform.position += moveDistance * moveDirection;
        }

        isWalking = moveDirection != Vector3.zero;
        
        // Rotate the player
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
    }

    public bool IsWalking() {
        return isWalking;
    }
}
