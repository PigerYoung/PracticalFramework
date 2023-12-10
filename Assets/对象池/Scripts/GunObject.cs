using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GunObject : MonoBehaviour
{
   public Transform firePoint;//开火点
   public GameObject bulletPrefab;//子弹预制体

    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab = ResourcesManager.Instance.Load<GameObject>("Bullet");
        firePoint = transform.Find("FirePoint");
        ObjectPoolManager.Instance.InitPool(bulletPrefab, 5);
        EventCenterManager.Instance.AddListener<KeyCode>(EventDefine.GetkeyDown, CheckInputKeysDown);
        InputKeysManager.Instance.SetActive (true);
    }

   public void CheckInputKeysDown(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.Mouse0:
                Shoot();
                break;
        }
    }
    private void Shoot()
    {
       GameObject bullet= ObjectPoolManager.Instance.GetObject(bulletPrefab);
        bullet.transform.position = firePoint.position;
        bullet.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1000));
    }
    private void OnDestroy()
    {
        EventCenterManager.Instance.RemoveListener<KeyCode>(EventDefine.GetkeyDown, CheckInputKeysDown);
    }
   
}
