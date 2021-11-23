using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Aloha.EntityStats;

namespace Aloha
{
    public class Lancer : Enemy<LancerStats> {

        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        protected override IEnumerator AI()
        {
            int rand = Utils.RandomInt(1, 4);
            for (int i = 0; i < rand; i++)
            {
                yield return StartCoroutine(MoveXToAnimation(Utils.RandomFloat(-1.5f, 1.5f), 1));
            }
            yield return StartCoroutine(AttackAnimation(4f));
        }

        protected IEnumerator AttackAnimation(float speed)
        {
            // Start animation
            anim.SetBool("isAttacking", true);

            yield return StartCoroutine(GetBump(new Vector3(0, 0, 1f), 3f));
            yield return new WaitForSeconds(0.2f);

            float temps = 0;
            Vector3 posInit = gameObject.transform.position;
            Vector3 posFinal = posInit;
            posFinal.x = 0; posFinal.z = -1;


            while (temps < 1f)
            {
                temps += speed * Time.deltaTime;
                gameObject.transform.position = Vector3.Lerp(posInit, posFinal, temps);
                yield return null;
            }

            gameObject.transform.position = posFinal;

            Hero hero = GameManager.Instance.GetHero();
            Attack(hero);
            Debug.Log(hero.currentHealth);

            Disappear();
        }

    }
}