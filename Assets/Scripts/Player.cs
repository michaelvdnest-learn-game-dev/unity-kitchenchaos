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

    [SerializeField]
    private float interactDistance = 2f;

    [SerializeReference]
    private GameInput gameInput;

    [SerializeReference]
    private LayerMask countersLayersMask;

    private bool isWalking;

    private Vector3 lastInteractDirection;

    // Start is called before the first frame update
    private void Start() {
        
    }

    // Update is called once per frame
    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleMovement() {

        // Get a vector of the player's movement 
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        // Cast a ray in the player movement direction to see if there are any objects in that direction
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

    private void HandleInteractions() {
         // Get a vector of the player's movement 
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDirection != Vector3.zero){
            lastInteractDirection = moveDirection;
        }

        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit hitInfo, interactDistance, countersLayersMask)) {
            if (hitInfo.transform.TryGetComponent(out ClearCounter clearCounter)){
                // Has ClearCounter
                clearCounter.Interact();
            }
        }
    }
}
