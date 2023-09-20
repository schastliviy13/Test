using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Weapon")]
public class WeaponSO : ItemSO, IShot,IReload //������������ ����������� ��� ����������� ���������� ������ �������
{
    public float weaponDamage;
    private BulletSO typeBullet;
    public int maxCountbullet;
    public int countBullet;

    public void Reload(int countBullet, ItemSO typeBullet)
    {
        Debug.Log("�����������");
        this.countBullet = countBullet;
        this.typeBullet = (BulletSO)typeBullet;
    }

    public void Shot(Vector3 firePoint,Vector3 direction)
    {
        if (countItem > 0)
        {
            Debug.Log("�������");
            direction = direction.normalized;
            countBullet -= 1;
            var tmpBullet = Instantiate(typeBullet.prefabBullet, firePoint, Quaternion.identity);
            tmpBullet.GetComponent<Bullet>().SetParametrs(typeBullet.bulletSpeed, weaponDamage, direction, typeBullet.iconItem);
        }
    }
        

}
