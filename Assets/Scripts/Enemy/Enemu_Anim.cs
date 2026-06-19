using System;
using UnityEngine;

public class Enemu_Anim : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private GameObject  Player;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, Player.transform.position);
        
        //Debug.Log($"Distance : {distance}");
        if (distance < 9f)
        {
            transform.LookAt(Player.transform.position);
            animator.SetBool("Shoot", true);
        }
    }
}
