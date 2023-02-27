using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] ScrollUI scrollUI;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;
    private Vector3 lastInteractDir;
    [SerializeField] private Transform scrollHoldPoint;
    [SerializeField] private LayerMask scrollLayerMask;
    [SerializeField] private LayerMask scrollDropAreaMask;

    public ActivityScroll currentActivityScroll;



    private bool isHoldingScroll;
    private float pickupRadius = 3f;



    

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, pickupRadius, scrollLayerMask);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out ActivityScroll activityScroll))
            {
                activityScroll.Interact(this);
                break;
            }
            
        }

    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();


        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .5f;
        float playerHeight = .75f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance) || !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance, scrollLayerMask, QueryTriggerInteraction.Ignore);


        if (!canMove)
        {
            //Cannot move towards moveDir

            //Attepmt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                //Cannot move only on the X

                //Attepmt only Z movement
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;

                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    //Can move only on the Z

                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot move in any direction
                }
            }

        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);

    }

    public Transform GetScrollHoldPoint()
    {
        return scrollHoldPoint;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void Update()
    {
        HandleMovement();     
    }

    public bool IsHoldingScroll()
    {
        return isHoldingScroll;
    }

    public void SetIsHoldingScroll(bool isHoldingScroll)
    {
        this.isHoldingScroll = isHoldingScroll;
    }

    public void SetCurrentActivityScroll(ActivityScroll activityScroll)
    {
        currentActivityScroll = activityScroll;
    }

    public ActivityScroll GetCurrentActivityScroll()
    {
        return currentActivityScroll;
    }

}
