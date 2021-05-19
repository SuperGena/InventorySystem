using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorials
{
    public class ProjectileLouncher : MonoBehaviour
    {
        [SerializeField] private Rigidbody projectilePrefab;
        [SerializeField] private Transform weaponMountPoint;

        //[SerializeField] private float fireForce = 300f;

        [SerializeField] Queue<Rigidbody> bulletQueue = new Queue<Rigidbody>();

        private void Awake()
        {
            GetComponent<GetPlayerInput>().OnFire += HandleFire;
        }

        private void HandleFire()
        {
            BulletPooling();
        }

        private void BulletPooling()
        {
            if (bulletQueue.Count < 1)
            {
                var spawnedProjectile = Instantiate(projectilePrefab, weaponMountPoint.position, weaponMountPoint.rotation);

                bulletQueue.Enqueue(spawnedProjectile);

            }
            if (bulletQueue.Peek().gameObject.activeSelf)
            {
                var spawnedProjectile = Instantiate(projectilePrefab, weaponMountPoint.position, weaponMountPoint.rotation);

                bulletQueue.Enqueue(spawnedProjectile);
            }
            else
            {
                bulletQueue.Peek().transform.SetPositionAndRotation(weaponMountPoint.position, weaponMountPoint.rotation);

                bulletQueue.Peek().gameObject.SetActive(true);

                //bulletQueue.Peek().AddForce(weaponMountPoint.transform.forward * fireForce);

                bulletQueue.Enqueue(bulletQueue.Dequeue());
            }
        }
    }
}
