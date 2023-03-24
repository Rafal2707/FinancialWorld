using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : NetworkBehaviour, IScrollParent
{

    public static event EventHandler OnAnyPlayerSpawned;

    public static void ResetStaticData()
    {
        OnAnyPlayerSpawned = null;
    }

    public static Player LocalInstance { get; private set; }


    [SerializeField] private float moveSpeed = 7f;
    private bool isWalking;
    private Vector3 lastInteractDir;
    [SerializeField] private Transform scrollHoldPoint;
    [SerializeField] private LayerMask scrollLayerMask;
    [SerializeField] private LayerMask scrollDropAreaMask;
    [SerializeField] private LayerMask collisionsLayerMask;

    [SerializeField] private List<Vector3> spawnPositionList;


    public ActivityScroll currentActivityScroll;



    private bool isHoldingScroll;
    private float pickupRadius = 3f;
   [SerializeField] private bool isInDroppingArea;

    

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            LocalInstance = this;
        }

        transform.position = spawnPositionList[GameManagerMultiplayer.Instance.GetPlayerDataIndexFromClientId(OwnerClientId)];

        OnAnyPlayerSpawned?.Invoke(this, EventArgs.Empty);

        if(IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
        }
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        if(clientId == OwnerClientId && HasActivityScroll())
        {
            ActivityScroll activityScroll = GetActivityScroll();
            activityScroll.GetScrollUI().HideDescriptionUI();
            activityScroll.GetScrollUI().HideEKeyUI();
            activityScroll.ClearActivityScrollOnParent();
            activityScroll.transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
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

    public void SetIsInDroppingArea(bool isInDroppingArea)
    {
        this.isInDroppingArea= isInDroppingArea;
    }

    public bool IsInDroppingArea()
    {
        return isInDroppingArea;
    }
    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();


        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .5f;
        float playerHeight = .75f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance) || !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance, collisionsLayerMask, QueryTriggerInteraction.Ignore);


        if (!canMove)
        {
            //Cannot move towards moveDir

            //Attepmt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = (moveDir.x < -0.5 || moveDir.x > 0.5) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance, collisionsLayerMask);
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

                canMove = (moveDir.z < -0.5 || moveDir.z > 0.5) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance, collisionsLayerMask);

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
        
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;

        if (IsOwner)
        {
            CameraController.Instance.SetCameraTarget(transform);
        }
    }

    private void Update()
    {
        if(!IsOwner)
        {
            return;
        }

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




    public Transform GetScrollFollowTransform()
    {
        return scrollHoldPoint.transform;
    }

    public void SetActivityScroll(ActivityScroll activityScroll)
    {
        currentActivityScroll = activityScroll;
    }

    public ActivityScroll GetActivityScroll()
    {
        return currentActivityScroll;
    }

    public void ClearActivityScroll()
    {
        currentActivityScroll = null;
    }

    public bool HasActivityScroll()
    {
        return currentActivityScroll != null;
    }

    public NetworkObject GetNetworkObject()
    {
        return NetworkObject;
    }




}
