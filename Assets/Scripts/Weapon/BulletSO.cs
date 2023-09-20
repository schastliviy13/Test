using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Item/Bullet")]
public class BulletSO : ItemSO
{
    public float bulletSpeed;
    public GameObject prefabBullet;
}
