using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorials
{
    public class Bullet : MonoBehaviour, IDamager
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _velocity;
        [SerializeField] private float _time = 4f;
        List<BulletModifier> bulletModifiers = new List<BulletModifier>();

        private float timer;

        // TODO: Create constructor for modifying Bullet


            //RegisterBulletModifier(new HealModifier(this, 240));


        public void InflictDamage(IDamageable target)
        {
            for (int i = 0; i < bulletModifiers.Count; i++)
            {
                bulletModifiers[i].InflictDamage(target);
            }
            target.ReceiveDamage(_damage);
        }

        private void Start()
        {
            //RegisterBulletModifier(new HealModifier(this, 240));
            //RegisterBulletModifier(new BonusDamageModifier(this, 40));
            //RegisterBulletModifier(new PoisonModifiier(this, 5, 4));
            //Destroy(gameObject, 4);
            //timer = Time.time;
        }

        private void Update()
        {
            transform.Translate(new Vector3(0, 0, Time.deltaTime * _velocity));

            //timer += Time.deltaTime;

            //if (timer > 4)
            //{
            //    gameObject.SetActive(false);
            //}
        }

        public void AddDamage(int extraDamage)
        {
            _damage += extraDamage;
        }

        public void RegisterBulletModifier(BulletModifier newModifier)
        {
            bulletModifiers.Add(newModifier);
        }

        IEnumerator SetLiveTime(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        private void OnEnable()
        {
            StartCoroutine(SetLiveTime(_time));
        }
    }
}
