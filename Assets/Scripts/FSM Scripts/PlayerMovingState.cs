using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerBaseState
{

    public override void EnterState(PlayerControler_FSM player)
    {
        //player.selectedCharacter.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = false;
        player.selectedCharacter.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;


        UnityEngine.AI.NavMeshAgent agent = player.selectedCharacter.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = player.selectedCharacter.GetComponent<CharacterData>().targetPosition;
    }

    public override void ExitState(PlayerControler_FSM player)
    {
        player.selectedCharacter.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = player.selectedCharacter.transform.position;
        player.selectedCharacter.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        //player.selectedCharacter.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = true;
    }

    public override void Update(PlayerControler_FSM player)
    {
        //https://answers.unity.com/questions/324589/how-can-i-tell-when-a-navmesh-has-reached-its-dest.html

        UnityEngine.AI.NavMeshAgent agent = player.selectedCharacter.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                player.TransitionToState(player.attackState);
                return;
            }
        }

    }
}
