using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameManagerSO gM;

    private float inputH;
    private float inputV;
    [SerializeField]
    private float speed = 5f;

    private float interactRadius = 0.1f;
    private Vector3 lastInput;
    private Vector3 targetPosition;
    private Vector3 interactionPoint;
    private bool isMoving = false;
    private bool canMove = true;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private LayerMask isObstacleMask;
    private Collider2D obstacle;
    [SerializeField]
    private GameObject graphics;

    public bool CanMove { get => canMove; set => canMove = value; }
    public GameObject Graphics { get => graphics; set => graphics = value; }
    public global::System.Single Speed { get => speed; set => speed = value; }

    void Start()
    {
        transform.position = gM.InitPlayerPosition;
        anim.SetFloat("inputH", gM.InitPlayerRotation.x);
        anim.SetFloat("inputV", gM.InitPlayerRotation.y);
        graphics.transform.localScale = gM.InitPlayerScale;
        graphics.GetComponent<SpriteRenderer>().color = gM.InitPlayerColor;
        speed = gM.Speed;
    }

    // Update is called once per frame
    void Update()
    {
        Input();

        MovementAndAnimations();

        Interact();
    }

    private void MovementAndAnimations()
    {
        if (!isMoving && (inputH != 0 || inputV != 0))
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("inputH", inputH);
            anim.SetFloat("inputV", inputV);
            lastInput = new Vector3(inputH, inputV, 0);
            targetPosition = transform.position + lastInput;
            interactionPoint = targetPosition;

            obstacle = IsTileObstacle();
            if (!obstacle) StartCoroutine(MovePlayer());
        }
        else if (inputH == 0 && inputV == 0)
        {
            anim.SetBool("isWalking", false);
        }
    }

    private void Input()
    {
        if (!canMove) return;
        if (inputV == 0) inputH = UnityEngine.Input.GetAxisRaw("Horizontal");
        if (inputH == 0) inputV = UnityEngine.Input.GetAxisRaw("Vertical");
    }

    private void Interact()
    {
        if(UnityEngine.Input.GetKeyDown(KeyCode.E) && obstacle != null)
        {
            Debug.Log(obstacle.GetComponent<IInteractive>() != null);

            if(obstacle.TryGetComponent(out IInteractive interactiveObject))
            {
                Debug.Log("Interact");
                interactiveObject.Interact();
            }
        }
    }

    IEnumerator MovePlayer()
    {
        isMoving = true;
        
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        interactionPoint = transform.position + lastInput;
        isMoving = false;
    }

    private Collider2D IsTileObstacle()
    {
        return Physics2D.OverlapCircle(interactionPoint, interactRadius, isObstacleMask);
    }
}
