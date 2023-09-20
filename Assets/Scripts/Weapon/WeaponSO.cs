using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Weapon")]
public class WeaponSO : ItemSO, IShot,IReload //иплементация интерфейсов для возможности обращаться слюбым оружием
{
    public float weaponDamage;
    private BulletSO typeBullet;
    public int maxCountbullet;
    public int countBullet;

    public void Reload(int countBullet, ItemSO typeBullet)
    {
        Debug.Log("перезарядка");
        this.countBullet = countBullet;
        this.typeBullet = (BulletSO)typeBullet;
    }

    public void Shot(Vector3 firePoint,Vector3 direction)
    {
        if (countItem > 0)
        {
            Debug.Log("выстрел");
            direction = direction.normalized;
            countBullet -= 1;
            var tmpBullet = Instantiate(typeBullet.prefabBullet, firePoint, Quaternion.identity);
            tmpBullet.GetComponent<Bullet>().SetParametrs(typeBullet.bulletSpeed, weaponDamage, direction, typeBullet.iconItem);
        }
    }
        

}
