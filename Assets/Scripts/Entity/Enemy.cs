using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private float attackDistance=1;
    private float followDistance=5;
    private float speedAttack = 1;
    private float timeReloadAttack = 0;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private List<ItemSO> spawnItemIsDead = new List<ItemSO>();
    [SerializeField] private GameObject prefabItemGraund;

    public override void Start()
    {
        base.Start();
        typeEntity = TypeEntity.Player;
        detectedEntityRadius = followDistance;
    }

    void Update()
    {
        StateMachine();
        Search();        
    }
    private void Attack(Vector3 direction,Vector3 attackPoint)
    {
        Debug.Log(this.name+" ��������");
        float rayLength = 2f; // ����� ����
        Vector2 pos = new Vector2(attackPoint.x, attackPoint.y);
        // �������� ���� �� ������� ������� � ����������� �����
        RaycastHit2D hit = Physics2D.Raycast(pos, direction, rayLength);
        // ������� ��� � ��������� ��������� � ������
        if (hit.collider!=null)
        {
            Debug.Log(hit.collider.gameObject.name);
            // ������, � ������� ����� ���
            GameObject hitObject = hit.collider.gameObject;
            // �������� �������
            if (hitObject.transform.TryGetComponent(out Player _player))
            {
                // �������� �������� ������ ������ �������� �� ��������� ����� � ������� ����� ��������� ����� � �������
                _player.TakeDamage(this.GetTotalDamage());

            }
        }
        state = State.ReloadAttack;
    }
    public void ReloadAttack()
    {
        if (timeReloadAttack<=speedAttack)
        {
            timeReloadAttack += Time.deltaTime;
        }
        else
        {
            timeReloadAttack = 0;
            state = State.Normal;
        }
    }
    private void Follow(GameObject targetPoint=null)
    {
        moveDirection = targetPoint.transform.position - transform.position;
        moveDistance = speed * Time.deltaTime;
        // ���� ����� ���������
        if (followDistance > moveDirection.magnitude && attackDistance < moveDirection.magnitude) 
        {
            RotateTo(moveDirection);
            transform.position += moveDirection.normalized * moveDistance;
        }
        //���� ������ ������
        else
        {
            state = State.Normal;
        }
        // ���� ������
        if (attackDistance > moveDirection.magnitude)
        {
            state = State.Attack;
        }

    }

    protected override void StateMachine()
    {
        switch (state)
        {
            case State.Normal:
                if (detectedEntity.Count > 0) state = State.Follow;
                break;
            case State.Follow:
                if (detectedEntity.Count == 0) state = State.Normal;
                else Follow(GetTarget());
                break;
            case State.Attack:
                Attack(detectedEntity[detectedEntityId].transform.position-transform.position,attackPoint.transform.position);
                break;
            case State.ReloadAttack:
                ReloadAttack();
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
        //�������� ���������� ��� ������
        state = State.Die;
        //�������� �������� ������ � ������������� ������
        Debug.Log(this.name + " ����");
        //����� ���� ����� ������
        LutSpawn();

        Destroy(gameObject);
    } 
    private void LutSpawn()
    {
        //�������� �� ������ ���������� ���� ��������� � ������� ������ ��������
        if (spawnItemIsDead.Count > 0)
        {
            prefabItemGraund.GetComponent<ItemGraund>()._itemSO =
                spawnItemIsDead[Random.Range(0, spawnItemIsDead.Count - 1)];
            Instantiate(prefabItemGraund, transform.position, Quaternion.identity);
        }
    }
}
