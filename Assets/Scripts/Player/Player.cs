using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Класс игрока
public class Player : MonoBehaviour, IDamagable, IMovable
{
    [field: SerializeField] public float MaxHealth { get; set; }
    [field: SerializeField] public float MovementSpeed { get; set; }
    [field: SerializeField] public float CurrentHealth { get; set; }
    [SerializeField] public float JumpTargetMovingSpeed;
    [SerializeField] public float JumpSpeed;
    [SerializeField] public float JumpHeight;
    [Space]
    [SerializeField] public float CurrentLightCooldown;
    [SerializeField] public float CurrentHeavyCooldown;
    [SerializeField] public float LightCooldown;
    [SerializeField] public float HeavyCooldown;
    [Space]
    [SerializeField] public float LightDamage;
    [SerializeField] public float HeavyDamage;
    [Space]
    [SerializeField] public PlayerHitbox LightHitbox;
    [SerializeField] public PlayerHitbox HeavyHitbox;
    [Space]
    public Rigidbody RB;
    public Animator animator;
    [Space]
    [SerializeField] public GameObject OuterTarget;
    [SerializeField] public GameObject InnerTarget;
    [SerializeField] public GameObject TentaclePrefab;
    [SerializeField] public GameObject DirectionArrow;
    [SerializeField] public Slider HealthBar;
    [SerializeField] public List<GameObject> VFXs;
    [SerializeField] public GameObject DamageVFX;
    public GameObject CurrentDamageVFX;
    public bool Jumping = false;
    private float currentVelocity = 0;
    //Переменные машины состояний
    #region StateMachineVariables
    public PlayerStateMachine StateMachine { get; set; }
    public PlayerNormalState NormalState { get; set; }
    public PlayerJumpState JumpState { get; set; }
    #endregion
    private void Awake()
    {
        NormalState = new PlayerNormalState(this, StateMachine);
        JumpState = new PlayerJumpState(this, StateMachine);

        StateMachine = new PlayerStateMachine();
    }
    public void Start()
    {
        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        HealthBar.value = 1f;
        StateMachine.Initialize(NormalState);
    }
    private void Update()
    {
        if (HealthBar.value != CurrentHealth / MaxHealth)
            HealthBar.value = Mathf.SmoothDamp(HealthBar.value, CurrentHealth / MaxHealth, ref currentVelocity, 0.1f);
        if (CurrentLightCooldown > 0)
        {
            CurrentLightCooldown -= Time.deltaTime;
        }
        if (CurrentHeavyCooldown > 0)
        {
            CurrentHeavyCooldown -= Time.deltaTime;
        }
        StateMachine.CurrentPlayerState.FrameUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentPlayerState.PhysicsUpdate();
    }
    //Методы интерфейса IDamageble
    #region Health/Die
    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        CurrentDamageVFX =  Instantiate(DamageVFX, transform.position, Quaternion.identity);
        Destroy(CurrentDamageVFX, 2f);
        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }
    public void Heal(float healAmount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + healAmount, MaxHealth);
    }
    public void Die()
    {
        GameObject.Find("GameController").GetComponent<GameController>().GameOver();
    }
    #endregion
    //методы интерфейса IMovable
    #region Move
    public void Move(Vector3 velocity, float rotationSpeed)
    {
        Vector3 _newPosition = transform.position + velocity * MovementSpeed * Time.deltaTime;
        if ((_newPosition.x > 13f || _newPosition.x < -13f))
        {
            _newPosition.x = transform.position.x;
        }
        if ((_newPosition.z > 7f || _newPosition.z < -11f))
        {
            _newPosition.z = transform.position.z;
        }
        transform.position = _newPosition;
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, velocity, rotationSpeed * Time.deltaTime, 0f));
    }
    #endregion
    //мотод, вызываемый при событии в анимации
    #region AnimationTrigger

    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentPlayerState.AnimationTriggerEvent(triggerType);
    }
    public enum AnimationTriggerType
    {
        EndState,
        Jump
    }
    #endregion
}
