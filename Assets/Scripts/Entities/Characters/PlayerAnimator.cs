using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string MOVE_X = "moveX";
    private const string MOVE_Y = "moveY";
    private const string IS_WALKING = "isWalking";

    private Animator playerAnimator;

    [SerializeField] private Vector2 inputVector;
    [SerializeField] private Player player;
    [SerializeField] private bool isWalking;


    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVector.x = player.PlayerMovementDirection().x;
        inputVector.y = player.PlayerMovementDirection().y;

        if(player.PlayerMovementDirection() != Vector3.zero)
        {
            playerAnimator.SetFloat(MOVE_X, inputVector.x);
            playerAnimator.SetFloat(MOVE_Y, inputVector.y);
            isWalking = true;
        }
        else if(player.PlayerMovementDirection() == Vector3.zero)
        {
            isWalking = false;
        }

        playerAnimator.SetBool(IS_WALKING, isWalking);
    }
}
