using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _transform;
    private SpriteRenderer _sprite;
    private float speed;
    private Vector3 direction;
    private float damage;
    private Sprite iconBullet;
    private float liveTime=5;
    private float countLiveTime = 0;
    private float moveDistance;
    public void Start()
    {
        _transform = GetComponent<Transform>();
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = iconBullet;
    }
    public void Update()
    {

        moveDistance = speed * Time.deltaTime;
        _transform.position += direction * moveDistance;
        if (countLiveTime<liveTime)
        {
            countLiveTime += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    public void SetParametrs(float speed,float damage,Vector3 direction,Sprite iconBullet)
    {
        this.speed = speed;
        this.damage = damage;
        this.direction = direction;
        this.iconBullet = iconBullet;
    }
}
