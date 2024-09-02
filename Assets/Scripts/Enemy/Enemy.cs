using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
//Класс противника
public class Enemy : MonoBehaviour, IDamagable, IMovable, ITriggerCheckable
{
    public GameObject PlayerTarget;
    [field: SerializeField] public float MaxHealth { get; set; }
    [field: SerializeField] public float MovementSpeed { get; set; }
    [field: SerializeField] public float CurrentHealth { get; set; }
    [SerializeField] public float DamageValue;
    [SerializeField] public bool Invulnurable = false;
    [SerializeField] public EnemyHitbox HitBox;
    public GameObject CurrentHitBox;
    [field: SerializeField] public bool IsWithinStrikingDistance { get; set; }
    [field: SerializeField] public Slider HealthBar { get; set; }
    [SerializeField] public Vector3 HealthBarOffset;
    [SerializeField] public GameObject DamageVFX;
    public GameObject CurrentDamageVFX;
    public Rigidbody RB;
    public Animator animator;
    public Camera mainCamera;
    private float currentVelocity = 0;
    public float fadeTime;

    #region StateMachineVariables
    public EnemyStateMachine StateMachine { get; set; }
    public EnemySpawnState SpawnState { get; set; }
    public EnemyWalkState WalkState { get; set; }
    public EnemyHurtState HurtState { get; set; }
    public EnemyDeathState DeathState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public EnemyIdleState IdleState { get; set; }

    #endregion

    #region ScriptableObjectsValues
    [SerializeField] private EnemySpawnSOBase EnemySpawnBase;
    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyWalkSOBase EnemyWalkBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;
    [SerializeField] private EnemyHurtSOBase EnemyHurtBase;
    [SerializeField] private EnemyDeathSOBase EnemyDeathBase;

    public EnemySpawnSOBase EnemySpawnBaseInstance { get; set; }
    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyWalkSOBase EnemyWalkBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }
    public EnemyHurtSOBase EnemyHurtBaseInstance { get; set; }
    public EnemyDeathSOBase EnemyDeathBaseInstance { get; set; }
    #endregion
    private void Awake()
    {
        EnemySpawnBaseInstance = Instantiate(EnemySpawnBase);
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyWalkBaseInstance = Instantiate(EnemyWalkBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);
        EnemyHurtBaseInstance = Instantiate(EnemyHurtBase);
        EnemyDeathBaseInstance = Instantiate(EnemyDeathBase);

        StateMachine = new EnemyStateMachine();

        SpawnState = new EnemySpawnState(this, StateMachine);
        WalkState = new EnemyWalkState(this, StateMachine);
        HurtState = new EnemyHurtState(this, StateMachine);
        DeathState = new EnemyDeathState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
        IdleState = new EnemyIdleState(this, StateMachine);
    }
    private void Start()
    {
        mainCamera = Camera.main;
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        CurrentHealth = MaxHealth;
        HealthBar.value = 1f;
        HealthBar.gameObject.GetComponentInParent<CanvasGroup>().alpha = 0;
        RB = GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        animator.SetFloat("WalkSpeed", MovementSpeed / 2);

        EnemySpawnBaseInstance.Initialize(gameObject, this);
        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyWalkBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);
        EnemyHurtBaseInstance.Initialize(gameObject, this);
        EnemyDeathBaseInstance.Initialize(gameObject, this);
        StateMachine.Initialize(SpawnState);
        transform.rotation = Quaternion.LookRotation((PlayerTarget.transform.position - transform.position).normalized);
    }
    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
        HealthBar.transform.rotation = mainCamera.transform.rotation;
        HealthBar.transform.position = transform.position + HealthBarOffset;
    }
    private void FixedUpdate()
    {
        if (HealthBar.value != CurrentHealth / MaxHealth)
            HealthBar.value = Mathf.SmoothDamp(HealthBar.value, CurrentHealth / MaxHealth, ref currentVelocity, 0.1f);
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }
    #region Health/Die
    public void Damage(float damageAmount)
    {
        if (!Invulnurable)
        {
            CurrentHealth -= damageAmount;
            CurrentDamageVFX = Instantiate(DamageVFX, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            Destroy(CurrentDamageVFX, 2f);
            if (CurrentHealth <= 0f)
            {
                Die();
            }
            else
            {
                StateMachine.ChangeState(HurtState);
            }
        }
    }
    public void Heal(float healAmount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + healAmount, MaxHealth);
    }
    public void Die()
    {
        GameController.Score += 1;
        GameController.ScoreUpdate();
        StateMachine.ChangeState(DeathState);
    }

    #endregion

    #region Move
    public void Move(Vector3 velocity, float RotationSpeed)
    {
        velocity.y = 0;
        velocity.Normalize();
        transform.position += velocity * MovementSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,velocity, 5 * Time.deltaTime, 0f));
    }
    #endregion

    #region DistanceCheck
    public void SetWithinStrikingDistance(bool isWithinStrikingDistance)
    {
        IsWithinStrikingDistance = isWithinStrikingDistance;
    }
    #endregion

    #region AnimationTrigger

    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }
    public enum AnimationTriggerType
    {
        EndState,
        Attack,
        EndAttack
    }
    #endregion
}
