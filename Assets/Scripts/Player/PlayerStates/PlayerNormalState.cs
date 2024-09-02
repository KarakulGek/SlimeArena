using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Обычное состояние игрока
public class PlayerNormalState : PlayerState
{
    public GameObject Tentacle;
    public Quaternion TentacleRotation;
    public PlayerNormalState(Player player, PlayerStateMachine playerStateMachine): base(player, playerStateMachine)
    {
    }
    public override void EnterState()
    {
        player.animator.SetTrigger("Bounce");
        //player.DirectionArrow.SetActive(true);
    }
    public override void ExitState()
    {
        //player.DirectionArrow.SetActive(false);
    }
    public override void FrameUpdate()
    {
        if (player.DirectionArrow.transform.localScale.x < 1)
        {
            player.DirectionArrow.transform.localScale += new Vector3(2f, 2f, 2f) * Time.deltaTime;
        }
        if (player.DirectionArrow.transform.localScale.x > 1)
        {
            player.DirectionArrow.transform.localScale = Vector3.one;
        }
        #region Movement
        if ((Input.GetButtonDown("Horizontal") & !Input.GetButton("Vertical")) || (Input.GetButtonDown("Vertical") & !Input.GetButton("Horizontal")))
        {
            player.animator.SetTrigger("Slither");
        }
        else if ((Input.GetButtonUp("Horizontal") & !Input.GetButton("Vertical")) || (Input.GetButtonUp("Vertical") & !Input.GetButton("Horizontal")))
        {
            player.animator.SetTrigger("Bounce");
        }
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            player.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized, 8f);
        }
        #endregion
        #region Rotation
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var targetVector = hitInfo.point - player.transform.position;
            targetVector.y = 0;
            TentacleRotation = Quaternion.LookRotation(targetVector.normalized, Vector3.up);
            player.DirectionArrow.transform.rotation = Quaternion.LookRotation(targetVector.normalized, Vector3.up);
            Debug.DrawLine(ray.origin, hitInfo.point);
        }
        #endregion
        #region Attack
        if (Input.GetButtonDown("Fire1") && player.CurrentLightCooldown <= 0)
        {
            Tentacle = Player.Instantiate(player.TentaclePrefab, player.transform);
            Tentacle.GetComponent<PlayerAttack>().Sender = player;
            Tentacle.GetComponent<PlayerAttack>().HitBox = player.LightHitbox;
            Tentacle.GetComponent<PlayerAttack>().Damage = player.LightDamage;
            Tentacle.GetComponent<PlayerAttack>().VFX = player.VFXs[0];
            Tentacle.GetComponent<Animator>().SetTrigger("AttackLight");
            Tentacle.transform.localPosition += new Vector3(Random.Range(-0.3f, 0.3f), -0.4f, Random.Range(-0.3f, 0.3f));
            Player.Destroy(Tentacle, 1.5f);
            player.CurrentLightCooldown = player.LightCooldown;
        }
        if (Input.GetButtonDown("Fire2") && player.CurrentHeavyCooldown <= 0 && player.CurrentLightCooldown <= 0)
        {
            Tentacle = Player.Instantiate(player.TentaclePrefab, player.transform);
            Tentacle.GetComponent<PlayerAttack>().Sender = player;
            Tentacle.GetComponent<PlayerAttack>().HitBox = player.HeavyHitbox;
            Tentacle.GetComponent<PlayerAttack>().Damage = player.HeavyDamage;
            Tentacle.GetComponent<PlayerAttack>().VFX = player.VFXs[1];
            Tentacle.GetComponent<Animator>().SetTrigger("AttackHeavy");
            Player.Destroy(Tentacle, 2);
            player.CurrentHeavyCooldown = player.HeavyCooldown;
            player.CurrentLightCooldown = player.LightCooldown;
        }
        if (Tentacle != null)
            Tentacle.transform.rotation = TentacleRotation;
        #endregion
        if (Input.GetButtonDown("Jump")) player.StateMachine.ChangeState(player.JumpState);
    }
    public override void PhysicsUpdate()
    {

    }
    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {

    }
}
