using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private const string IS_WALING = "IsWalking";
    Animator animator;

    [SerializeField] private Player player;



    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALING, player.IsWalking());
    }




}
