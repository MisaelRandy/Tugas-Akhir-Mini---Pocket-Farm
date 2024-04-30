using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask greenTreeLayersMask;

    private Vector2 inputVector;
    private Vector3 movementDir;
    private Vector3 interactDir;
    private Vector3 lastInteractDir;
    private NormalGreenTree GreenTree;
    private float playerSize = 0.7f;
    private bool canMove;
    
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        GreenTree.Interact();
        
    }

    private void HandleInteractions()
    {
        inputVector = gameInput.GetMovementVectorNormalized();
        inputVector = inputVector.normalized;

        interactDir = new Vector3 (inputVector.x, inputVector.y, 0f);

        if (interactDir != Vector3.zero)
        {
            lastInteractDir = interactDir;
        }

        float interactDistance = 1f;

        RaycastHit2D raycastHit = Physics2D.Raycast(
            transform.position,
            lastInteractDir,
            interactDistance,
            greenTreeLayersMask);

        if (raycastHit.collider != null)
            {
                Transform objectHit = raycastHit.transform;

                // Debug.Log(objectHit.transform.name + " is in front of me!");
                Debug.DrawRay(transform.position, lastInteractDir * interactDistance, Color.red);

                objectHit.transform.TryGetComponent(out NormalGreenTree normalGreenTree);
                // * Source code above used so we can have "NormalGreenTree" component
                // normalGreenTree.Interact();
            }

        else
            {
                // Debug.Log("No object detected in front of me!");
                Debug.DrawRay(transform.position, lastInteractDir * interactDistance, Color.green);
            }
        
    }

    private void HandleMovement()
    {
        inputVector = gameInput.GetMovementVectorNormalized();
        inputVector = inputVector.normalized;

        movementDir = new Vector3 (inputVector.x, inputVector.y, 0f);

        float moveDistance = movementSpeed * Time.deltaTime;
        float capsuleCastAngle = 0f;

        canMove = !Physics2D.CapsuleCast(
            transform.position,
            new Vector2(playerSize, playerSize),
            CapsuleDirection2D.Vertical,
            capsuleCastAngle,
            movementDir,
            moveDistance
            );

        if (canMove)
        {
            transform.position += movementDir * moveDistance;
        }
    }

    public Vector3 PlayerMovementDirection()
    {
        return movementDir;
    }
}
