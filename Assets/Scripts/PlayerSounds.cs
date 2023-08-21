using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footstepTimer;
    private float footstepTimermax = .2f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;

        if(footstepTimer < 0 ) 
        {
            footstepTimer = footstepTimermax;

            if (player.IsWalking())
            {
                float volume = .15f;
                
                SoundManager.Instance.PlayFootstepsSound(Camera.main.transform.position, volume);
            }

        }
    }
}
