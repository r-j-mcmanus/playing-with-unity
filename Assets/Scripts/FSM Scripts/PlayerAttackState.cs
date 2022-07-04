using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttackState : PlayerBaseState
{
    public override void EnterState(PlayerControler_FSM player)
    {
        player.uiSwitches.SetPlayerDataActive(true);
    }

    public override void ExitState(PlayerControler_FSM player)
    {
        player.uiSwitches.SetPlayerDataActive(false);
        player.lineRenderer.gameObject.SetActive(false);
    }

    public override void Update(PlayerControler_FSM player)
    {
        if (Input.GetMouseButtonDown(1))
        {
            player.selectedCharacter.GetComponent<CharacterData>().RevertToTurnStartPosition();
            player.TransitionToState(player.playerSelectedState);
            return;
        }

        player.mouseOverRayCast.CheckMouseOver();

        bool mouseOverAttackableEnemyCharacter = false;
        player.lineRenderer.gameObject.SetActive(false);

        if (player.mouseOverRayCast.mouseOverObject != null)
        {
            foreach (GameObject ec in player.enemyCharacters)
            {
                if (GameObject.ReferenceEquals(ec, player.mouseOverRayCast.mouseOverObject))
                {
                    player.uiSwitches.enemyHealthBarControler.SetHealthBars((CharacterData)ec.GetComponent<CharacterData>());
                    player.uiSwitches.SetEnemyDataActive(true);

                    Vector3 diferenceVector = ec.transform.position - player.selectedCharacter.transform.position;

                    if (diferenceVector.magnitude < player.selectedCharacter.GetComponent<CharacterData>().attackRange)
                    {
                        RaycastHit hit;
                        Physics.Raycast(player.selectedCharacter.transform.position, diferenceVector, out hit);

                        if (GameObject.ReferenceEquals(hit.transform.gameObject, ec))
                        {
                            player.lineRenderer.gameObject.SetActive(true);
                            player.lineRenderer.SetPositions(
                                new Vector3[] {
                                player.selectedCharacter.transform.position,
                                ec.transform.position
                                }
                            );

                            mouseOverAttackableEnemyCharacter = true;
                        }
                    }
                    

                    break;
                }
                player.uiSwitches.SetEnemyDataActive(false);
            }
        }

        if (mouseOverAttackableEnemyCharacter && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("should be changing state!!");
            player.attackedCharacter = player.mouseOverRayCast.mouseOverObject;
            player.TransitionToState(player.playerAttackingState);
            return;
        }
    }
}


