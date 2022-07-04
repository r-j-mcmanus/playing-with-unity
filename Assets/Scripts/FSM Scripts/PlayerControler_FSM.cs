using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler_FSM : MonoBehaviour
{
    public PlayerBaseState currentState;

    public readonly PlayerIdleState idleState = new PlayerIdleState();
    public readonly PlayerSelectedState playerSelectedState = new PlayerSelectedState();
    public readonly PlayerAttackState attackState = new PlayerAttackState();
    public readonly PlayerAttackingState playerAttackingState = new PlayerAttackingState();
    public readonly PlayerMovingState playerMovingState = new PlayerMovingState();

    //public FloatingTextController = null;

    [HideInInspector]
    public UISwitchesScript uiSwitches = null;
    [HideInInspector]
    public ButtonManagerScript buttonManager = null;
    [HideInInspector]
    public ClickRayCastScript clickRayCast = null;
    [HideInInspector]
    public MouseOverRayCastScript mouseOverRayCast = null;
    [HideInInspector]
    public WalkablePathScript walkablePath = null;
    [HideInInspector]
    public CameraControlerScript camControler = null;
    [HideInInspector]
    public FloatingTextController floatingTextControler = null;
    [HideInInspector]
    public LineRenderer lineRenderer = null;

    public GameObject[] playerCharacters = null;
    public GameObject[] enemyCharacters = null;

    public GameObject selectedCharacter = null;
    public GameObject attackedCharacter = null;

    

    private string _requestedState;

    void Awake()
    {
        uiSwitches = GameObject.Find("UISwitches").GetComponent<UISwitchesScript>();
        //Debug.Log("uiSwitches " + uiSwitches);

        buttonManager = GameObject.Find("ButtonManager").GetComponent<ButtonManagerScript>();
        //Debug.Log("buttonManager " + buttonManager);

        clickRayCast = GameObject.Find("ClickRayCast").GetComponent<ClickRayCastScript>();
        //Debug.Log("clickRayCast " + clickRayCast);

        mouseOverRayCast = GameObject.Find("MouseOverRayCast").GetComponent<MouseOverRayCastScript>();
        //Debug.Log("mouseOverRayCast " + mouseOverRayCast);

        walkablePath = GameObject.Find("WalkablePath").GetComponent<WalkablePathScript>();
        //Debug.Log("walkablePath " + walkablePath);

        camControler = GameObject.Find("CameraControler").GetComponent<CameraControlerScript>();
        //Debug.Log("camControler " + camControler);

        lineRenderer = GameObject.Find("AttackingLine").GetComponent<LineRenderer>();
        //Debug.Log("camControler " + camControler);


        floatingTextControler = GameObject.Find("PopUpCanvas").GetComponent<FloatingTextController>();
        Debug.Log("floatingTextControler " + floatingTextControler);
    }

    // Start is called before the first frame update
    void Start()
    {

        currentState = idleState;
        currentState.EnterState(this);
        lineRenderer.gameObject.SetActive(false);

        _requestedState = "none";
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Update(this);

        if (Input.GetKeyDown("space"))
        {
            enemyCharacters[0].GetComponent<CharacterData>().bodyController.DestroyLArm();

        }
    }

    public void TransitionToState(PlayerBaseState state)
    {
        Debug.Log("Transitioning to state " + state);
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public void RequestChangeToState(string stateName)
    {
        _requestedState = stateName;
    }

    public string CheckRequestedState()
    {
        string temp = _requestedState;
        _requestedState = "none";
        return temp;
    }

    public void CallStartCoroutine(IEnumerator cr)
    {
        StartCoroutine(cr);
    }
}
