using UnityEngine;

public class Player : Entity
{
    [SerializeField] private GameObject firePoint;
    [SerializeField] private WeaponSO _weapon;
    [SerializeField] private BulletSO _bullet;
    public Inventory _inventory;

    public override void Start()
    {
        base.Start();
        GameInput.Instance.OnAttackAction += GameInput_OnAttackAction;
        GameInput.Instance.OnInventoryOpenAction += GameInput_OnInventoryAction;
        ManagerInventory.Instance.OnInventoryCloseAction += ManagerInventory_OnInventoryCloseAction;
        
        detectedEntityRadius = 10;
        Reload();
    }
    void Update()
    {
        CheckEnemiesAround();
        StateMachine();        
    }
    public void CheckEnemiesAround()
    {
        if (detectedEntity.Count > 0)
        {
            GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.Combat);
        }
        if (detectedEntity.Count == 0)
        {
            GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.Explore);
        }
    }
    public void Reload()
    {
        if (_inventory.GetItemById(67)!=null)
        {
            int countBulletReload = _weapon.maxCountbullet - _weapon.countBullet;
            if (_inventory.GetItemById(67).countItem- countBulletReload > 0)
            {
                _weapon.Reload(countBulletReload, _inventory.GetItemById(67));
                _inventory.GetItemById(67).countItem -= countBulletReload;
            }
        }
    }
    private void GameInput_OnAttackAction(object sender, System.EventArgs e)
    {
        if (GameStateManager.Instance.GetGameState() == GameStateManager.GameState.Combat)
        {
            Attack(detectedEntity[detectedEntityId].transform.position - firePoint.transform.position);
        }
        
    }
    private void GameInput_OnInventoryAction(object sender, System.EventArgs e)
    {
        Pause();
    }
    private void ManagerInventory_OnInventoryCloseAction(object sender, System.EventArgs e)
    {
        state = State.Normal;
    }
    private void Movement()
    {
        //получаем вектор движения
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        //преобразуем в Vector3 для дальнейшей работы
        moveDirection = new Vector3(inputVector.x, inputVector.y, 0);
        //просчитываем расстояние за момент времени
        moveDistance = speed * Time.deltaTime;
        //двигаем
        RotateTo(moveDirection);
        transform.position += moveDirection * moveDistance;
        //transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }
    private void Attack(Vector3 direction)
    {
        if (_weapon.countBullet>0)
        {
            _weapon.Shot(firePoint.transform.position, direction);
        }
        else
        {
            Reload();
        }
    }
    public void Pause()
    {
        state = State.Pause;
    }
    protected override void StateMachine()
    {
        switch (state)
        {
            case State.Normal:
                Search();
                Movement();
                break;
            case State.Pause:                
                break;
            case State.Die:
                Die();
                break;
        }
    }
    protected override void Die()
    {
        //отбираем управление при смерти
        state = State.Die;
        //в будущем вызываем анимацию смерти и сопутствующую логику
        Debug.Log(this.name + " умер");
        Destroy(gameObject);
    }
}
