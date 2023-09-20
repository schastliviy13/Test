using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShot
{
    public abstract void Shot(Vector3 firePoint,Vector3 direction);
}
public interface IReload
{
    public abstract void Reload(int countBullet, ItemSO typeBullet);
}
