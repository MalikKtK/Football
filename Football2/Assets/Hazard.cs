using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public AudioSource hazardSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null )
            {
                hazardSound.Play();
                player.Die();
            }
        
        }
    }
}
