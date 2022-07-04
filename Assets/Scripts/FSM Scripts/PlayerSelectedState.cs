using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSelectedState : PlayerBaseState
{
    public override void EnterState(PlayerControler_FSM player)
    {
        //player.selectedCharacter.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = false;
        //player.selectedCharacter.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;

        player.uiSwitches.SetPlayerDataActive(true);
        player.uiSwitches.SetActionMenuActive(true);
        player.walkablePath.SetActive(true);

        player.uiSwitches.playerHealthBarControler.SetHealthBars((CharacterData) player.selectedCharacter.GetComponent<CharacterData>());
    }
    
    public override void ExitState(PlayerControler_FSM player)
    {
        //player.selectedCharacter.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        //player.selectedCharacter.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = true;

        player.uiSwitches.SetPlayerDataActive(false);
        player.uiSwitches.SetActionMenuActive(false);
        player.walkablePath.SetActive(false);
    }

    public override void Update(PlayerControler_FSM player)
    {
        player.mouseOverRayCast.CheckMouseOver();
        player.walkablePath.makePath(player.selectedCharacter.transform.position, player.mouseOverRayCast.hitPoint, player.selectedCharacter.GetComponent<CharacterData>().maxMoveRange);

        string requestedStateName = player.CheckRequestedState();
        switch (requestedStateName)
        {
            case "none":
            case "None":
                break;

            case "attackState":
                //Debug.Log("Chaning to player.attackState " + player.attackState);
                player.TransitionToState(player.attackState);
                return;

            default:
                Debug.Log("Requested change to invalid state name string: " + requestedStateName);
                break;
        }

        if (Input.GetMouseButtonDown(1))
        {
            player.TransitionToState(player.idleState);
            return;
        }

        player.clickRayCast.CheckClick();
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (player.clickRayCast.clickedObject != null)
            {
                foreach (GameObject pc in player.playerCharacters)
                {
                    if (GameObject.ReferenceEquals(pc, player.clickRayCast.clickedObject))
                    {
                        player.selectedCharacter = pc;
                        player.TransitionToState(player.playerSelectedState);
                        return;
                    }
                }
            }
            
            player.selectedCharacter.GetComponent<CharacterData>().targetPosition = player.walkablePath.endPosition;
            player.TransitionToState(player.playerMovingState);
            return;
        }
    }
}
