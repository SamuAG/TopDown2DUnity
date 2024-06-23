using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private float timeBetweenSteps = 2.0f;
    [SerializeField]
    private float maxDistance = 10.0f;
    [SerializeField]
    private float detectionRadius = 0.1f;
    [SerializeField]
    private LayerMask obstacleMask;

    private Vector3 targetPosition;
    private Vector3 initialPosition;
    
    private Animator anim;

    private bool canMove = true;

    public bool CanMove { get => canMove; set => canMove = value; }

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void Start()
    {
        if (TryGetComponent<Animator>(out Animator _animator))
        {
            anim = _animator;
        }

        StartCoroutine(MoveToTargetAndWait());
    }

    private IEnumerator MoveToTargetAndWait()
    {
        while (true)
        {
            
            if (!canMove)
            {
                yield return null;
                continue;
            }
            
            NextTargetPosition();
            if(anim) anim.SetBool("isWalking", true);
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
            if(anim) anim.SetBool("isWalking", false);
            yield return new WaitForSeconds(timeBetweenSteps);
        }
    }

    private void NextTargetPosition()
    {
        // this method calculates the next target position
        int tries = 0;
        do
        {
            int prob = UnityEngine.Random.Range(0, 4);
            tries++;
            switch (prob)
            {
                case 0:
                    targetPosition = transform.position + Vector3.left;
                    if(anim) anim.SetFloat("inputH", -1); if(anim) anim.SetFloat("inputV", 0);
                    break;
                case 1:
                    targetPosition = transform.position + Vector3.right;
                    if(anim) anim.SetFloat("inputH", 1); if(anim) anim.SetFloat("inputV", 0);
                    break;
                case 2:
                    targetPosition = transform.position + Vector3.up;
                    if(anim) anim.SetFloat("inputH", 0); if(anim) anim.SetFloat("inputV", 1);
                    break;
                case 3:
                    targetPosition = transform.position + Vector3.down;
                    if(anim) anim.SetFloat("inputH", 0); if(anim) anim.SetFloat("inputV", -1);
                    break;
                default:
                    Debug.Log("The NPC doesnt find a correct targetposition");
                    break;
            }
        } while (!IsTileTargeteable() && tries < 15);
    }

    private bool IsTileTargeteable()
    {
        // this method checks if the tile where the npc is gonna try to move
        // is free and in distance

        if(Vector3.Distance(initialPosition, targetPosition) > maxDistance)
        {
            return false;
        }
        else
        {
            return !Physics2D.OverlapCircle(targetPosition, detectionRadius, obstacleMask);
        }
    }
}
