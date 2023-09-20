using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public event EventHandler OnHealthChanged;

    [SerializeField]protected float speed=5;
    [SerializeField] protected float health;
    private float healthMax=100;
    protected float baseDamage=10;

    protected Vector3 moveDirection;
    protected float moveDistance;

    [SerializeField] protected State state;

    protected bool isWalking;
    protected bool isAttack;

    protected List<Entity> detectedEntity = new List<Entity>();
    protected int detectedEntityId = 0;
    protected float detectedEntityRadius;
    protected TypeEntity typeEntity; 
    public enum TypeEntity
    {
        Enemy,
        Player,
        NPC
    }
    public enum State
    {
        Normal,
        Follow,
        Attack,
        ReloadAttack,
        Pause,
        Die
    }
    public virtual void Start()
    {
        //выставляем стартовое здоровье и состояние
        health = healthMax;
        state = State.Normal;
    }
    protected void Search()
    {
        //находим все колайдеры в раудисе перебором выбираем те кторые нам подходят и добавляем найденные сущности
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, detectedEntityRadius);
        detectedEntity.Clear();
        foreach (Collider2D colliders in collider)
        {
            GameObject hitObject = colliders.gameObject;
            AddSelectedType(hitObject);
        }
        
    }
    public GameObject GetTarget()
    {
        return detectedEntity[detectedEntityId].gameObject;
    }
    //слабая часть кода, постаянно придеться расширять если добавиться новый класс котрый нужно будет искать
    //основаная задача сделать поиск сущностей настраиваемым
    private void AddSelectedType(GameObject hitObject)
    {
        switch (typeEntity)
        {

            case TypeEntity.Enemy:
                if (hitObject.transform.TryGetComponent(out Enemy Enemy))
                {
                    detectedEntity.Add(Enemy);
                }
                break;
            case TypeEntity.Player:
                if (hitObject.transform.TryGetComponent(out Player Player))
                {
                    detectedEntity.Add(Player);
                }
                break;
        }
    }
    public virtual float GetTotalDamage()
    {
        return baseDamage;
    }
    public void TakeDamage(float damage)
    {
        ChangeHealthByAmount(-damage);
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        Debug.Log(this.name+"получен урон");
    }

    protected virtual void ChangeHealthByAmount(float amount)
    {
        health += amount;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        if (health > healthMax)
        {
            health = healthMax;
        }
    }
    public float GetHealth()
    {
        return health;
    }
    protected abstract void Die();
    protected abstract void StateMachine();
    protected void RotateTo(Vector3 direction)
    {
        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180, transform.rotation.z));
        }
        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0, transform.rotation.z));
        }

    }

}
