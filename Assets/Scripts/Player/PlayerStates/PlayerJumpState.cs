using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
//Состояние прыжка игрока
public class PlayerJumpState : PlayerState
{
    private GameObject InnerTarget;
    private GameObject OuterTarget;
    private float counter;
    private Vector3 startPos;
    private bool jumpingAnimation;
    private float jumpDistance;

    public PlayerJumpState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }
    public override void EnterState()
    {
        player.DirectionArrow.transform.rotation = Quaternion.identity;
        player.animator.SetTrigger("JumpPrep");
        InnerTarget = Player.Instantiate(player.InnerTarget);
        OuterTarget = Player.Instantiate(player.OuterTarget, player.transform.position, new Quaternion());
    }
    public override void ExitState()
    {
        jumpingAnimation = false;
        Player.Destroy(OuterTarget);
    }
    public override void FrameUpdate()
    {
        if (player.DirectionArrow.transform.localScale.x > 0)
        {
            player.DirectionArrow.transform.localScale -= new Vector3(2f, 2f, 2f) * Time.deltaTime;
        }
        if (player.DirectionArrow.transform.localScale.x < 0)
        {
            player.DirectionArrow.transform.localScale = Vector3.zero;
        }
        if (Input.GetButtonDown("Jump") & !jumpingAnimation)
        {
            player.animator.SetTrigger("Jump");
            Player.Destroy(InnerTarget);
            counter = 0;
            startPos = player.transform.position;
            jumpingAnimation = true;
            jumpDistance = Vector3.Distance(startPos, OuterTarget.transform.position);
        }
        if (!player.Jumping & !jumpingAnimation)
        {
            #region Движение внутренней и внешней целей
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
            {
                var target = hitInfo.point;
                target.y = 0.2f;
                InnerTarget.transform.position = target;
            }
            if ((InnerTarget.transform.position - OuterTarget.transform.position).magnitude > player.JumpTargetMovingSpeed * Time.deltaTime)
            {
                OuterTarget.transform.position += (InnerTarget.transform.position - OuterTarget.transform.position).normalized
                * player.JumpTargetMovingSpeed
                * Time.deltaTime;
            }
            else
            {
                OuterTarget.transform.position = InnerTarget.transform.position;
            }
            #endregion
            player.transform.rotation = Quaternion.LookRotation((OuterTarget.transform.position + new Vector3(0, 0.2f, 0) - player.transform.position).normalized, Vector3.up);
        }
        else if (player.Jumping)
        {
            #region Движение по арке
            player.transform.position = new Vector3(
                Mathf.Lerp(startPos.x, OuterTarget.transform.position.x, counter/180),
                startPos.y + Mathf.Sin(Mathf.PI * 2 * counter / 360) * player.JumpHeight * jumpDistance,
                Mathf.Lerp(startPos.z, OuterTarget.transform.position.z, counter/180));
            counter += player.JumpSpeed / jumpDistance;
            if (counter >= 180 -  (180 % 2))
            {
                player.transform.position = new Vector3(
                OuterTarget.transform.position.x,
                0.4f,
                OuterTarget.transform.position.z);
                player.animator.SetTrigger("JumpEnd");
                player.Jumping = false;
            }
            #endregion
        }
    }
    public override void PhysicsUpdate()
    {

    }
    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        switch (triggerType)
        {
            case Player.AnimationTriggerType.EndState:
                player.StateMachine.ChangeState(player.NormalState);
                break;
            case Player.AnimationTriggerType.Jump:
                player.Jumping = true;
                break;
        }
    }
}
