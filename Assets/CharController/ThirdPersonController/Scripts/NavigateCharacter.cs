using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonController))]
public class NavigateCharacter : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent Agent { get; private set; }             // the navmesh agent required for the path finding
    public ThirdPersonController Character { get; private set; } // the character we are controlling
    public PlayerInput PlayerInput { get; private set; }
    [SerializeField] LayerMask clickableLayers;
    public CustomActions input;
    float distance;

    void OnEnable()
    {
        input = new CustomActions();
        input.Enable();
    }
    void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        Agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        Character = GetComponent<ThirdPersonController>();
        PlayerInput = GetComponent<PlayerInput>();

        Agent.updateRotation = false;
        Agent.updatePosition = true;

        AssignInputs();
    }

    void AssignInputs()
    {
        input.Main.Move.performed += ctx => ClickToMove();
    }

    public void ClickToMove()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, clickableLayers))
        {
            // Disable Keyboard Inputs
            PlayerInput.enabled = false;
            Agent.enabled = true;
            Agent.SetDestination(hit.point);
        }
    }

    private void Update()
    {
        if(Agent.enabled)
        {
            distance = Vector3.Distance(transform.position, Agent.destination);

            if (distance > Agent.stoppingDistance)
            {
                Character.Move(Agent.desiredVelocity / 2.0f);
            }
            else
            {
                // Enable Keyboard Inputs
                PlayerInput.enabled = true;
                Agent.enabled = false;
            }
        }
        else
        {
            Character.Move();
        }
    }
}