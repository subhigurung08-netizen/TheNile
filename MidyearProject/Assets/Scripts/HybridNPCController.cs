using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HybridNPCController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float detectionDistance = 15f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private GameObject target;

    // (main / high level states)
    [SerializeField] private enum NPCState

    
    {
        Roam,
        Chase
    }

    // (roaming sub states)
    public enum RoamState
    {
        Idle = 0,
        Walking = 1
    }

    public enum PlayerMoveState
    {
        Idle,
        Left,
        Right,
        Up,
        Down
    }

    private NPCState currentState = NPCState.Roam;
    private RoamState currentRoamState = RoamState.Idle;
    private PlayerMoveState predictedPlayerState = PlayerMoveState.Idle;

    private float[,] transitionMatrix = new float[,]
    {
        { 0.5f, 0.5f }, // From Idle -> (Idle, Walking)
        { 0.05f, 0.95f }  // From Walking -> (Idle, Walking)
    };

    // Player movement tracking
    private Vector3 lastPlayerPosition;
    private float movementThreshold = 0.02f;

    void Start()
    {
        if (player != null)
            lastPlayerPosition = player.position;
    }

    void Update()
    {
        if (player == null) return;

        PredictPlayerMovement();
        UpdateHighLevelState();
        Act();
    }

    // high level state transitions (Roam / Chase)
    void UpdateHighLevelState()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionDistance)
        {
            if (currentState != NPCState.Chase)
                Debug.Log("NPC State -> Chase");

            currentState = NPCState.Chase;
        }
        else
        {
            if (currentState != NPCState.Roam)
                Debug.Log("NPC State -> Roam");

            currentState = NPCState.Roam;
        }
    }

    // action selection based on current state
    void Act()
    {
        switch (currentState)
        {
            case NPCState.Roam:
                UpdateRoamState();
                Roam();
                break;

            case NPCState.Chase:
                ChasePredictedDirection();
                break;
        }
    }

    // mdp (Idle <-> Walking)
    void UpdateRoamState()
    {
        float randomValue = Random.Range(0f, 1f);
        int current = (int)currentRoamState;

        if (randomValue < transitionMatrix[current, 0])
        {
            currentRoamState = RoamState.Idle;
        }
        else
        {
            currentRoamState = RoamState.Walking;
        }

        Debug.Log($"Roam MDP Transition -> {currentRoamState}");
    }

    void PredictPlayerMovement()
    {
        Vector3 delta = player.position - lastPlayerPosition;
        delta.y = 0f;

        if (delta.magnitude < movementThreshold)
        {
            predictedPlayerState = PlayerMoveState.Idle;
        }
        else if (Mathf.Abs(delta.x) > Mathf.Abs(delta.z))
        {
            predictedPlayerState = delta.x > 0
                ? PlayerMoveState.Right
                : PlayerMoveState.Left;
        }
        else
        {
            predictedPlayerState = delta.z > 0
                ? PlayerMoveState.Up
                : PlayerMoveState.Down;
        }

        Debug.Log($"Predicted Player Direction -> {predictedPlayerState}");

        lastPlayerPosition = player.position;
    }

    // behaviors section

    Vector3 wanderDir;
    float roamTimer;
    [SerializeField] float changeInterval = 2f;

    void Roam()
    {
        if (currentRoamState == RoamState.Idle)
            return;

        roamTimer -= Time.deltaTime;

        if (roamTimer <= 0f)
        {
            wanderDir = Random.insideUnitSphere;
            wanderDir.y = 0f;
            wanderDir.Normalize();
            roamTimer = changeInterval;
        }

        transform.position += wanderDir * moveSpeed * Time.deltaTime;
    }

    void ChasePredictedDirection()
    {
        Vector3 moveDir = Vector3.zero;

        switch (predictedPlayerState)
        {
            case PlayerMoveState.Left:
                moveDir = Vector3.left;
                break;
            case PlayerMoveState.Right:
                moveDir = Vector3.right;
                break;
            case PlayerMoveState.Up:
                moveDir = Vector3.forward;
                break;
            case PlayerMoveState.Down:
                moveDir = Vector3.back;
                break;
            case PlayerMoveState.Idle:
                moveDir = player.position - transform.position;
                moveDir.y = 0f;
                moveDir.Normalize();
                break;
        }

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Destroy(target, 1f);
            SceneManager.LoadScene(2);
        }
    }
}
