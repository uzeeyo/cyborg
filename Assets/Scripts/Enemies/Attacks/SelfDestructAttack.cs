using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Cyborg.Enemies
{
    public class SelfDestructAttack : EnemyAttack
    {
        private float _timeToComplete;

        [SerializeField] private float _blastRadius = 1;
        [SerializeField] private AnimationClip _animationClip;
        [SerializeField] List<Light2D> _lights;

        private void Awake()
        {
            _timeToComplete = _animationClip.length;
        }

        public override void Attack(Transform targetTransform)
        {
            if (Vector3.Distance(transform.position, targetTransform.position) > _blastRadius)
            {
                RaiseAttackComplete();
                return;
            }

            foreach (var light in _lights)
            {
                light.enabled = false;
            }

            GetComponentInParent<Animator>().Play("Attack");
            if (Vector2.Distance(targetTransform.position, transform.position) < _blastRadius)
            {
                EnergyManager.Instance.RemoveEnergy(_damage);
            }
            StartCoroutine(DestroyAfterAnimation());
        }

        private IEnumerator DestroyAfterAnimation()
        {
            yield return new WaitForSeconds(_timeToComplete);

            //RaiseAttackComplete();
            Destroy(transform.parent.gameObject);
        }
    }
}