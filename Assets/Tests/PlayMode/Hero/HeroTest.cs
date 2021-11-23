using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Aloha.Test
{
    public class HeroTest
    {
        [Test]
        public void HeroInstantierTest()
        {
            GameObject manager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"));
            HeroInstantier.Instance.InstantiateHero(HeroType.Warrior);
            Hero hero = GameManager.Instance.GetHero();

            Assert.IsTrue(hero != null);
            Assert.IsTrue(hero is Warrior);

            GameObject.DestroyImmediate(hero.gameObject);
            GameObject.DestroyImmediate(manager);
        }

        [Test]
        public void HeroStatsTest()
        {
            GameObject manager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"));
            HeroInstantier.Instance.InstantiateHero(HeroType.Warrior);
            Hero hero = GameManager.Instance.GetHero();

            Assert.IsTrue(hero != null);
            Assert.IsTrue(hero is Warrior);
            Assert.IsTrue(hero.GetStats() != null);
            Assert.IsTrue(hero.GetStats() is WarriorStats);

            GameObject.Destroy(hero.gameObject);
            GameObject.Destroy(manager);
        }

        [UnityTest]
        public IEnumerator HeroTestDamage()
        {
            GameObject warriorGO = new GameObject();
            Warrior warrior = warriorGO.AddComponent<Warrior>();
            WarriorStats stats = (WarriorStats)ScriptableObject.CreateInstance("WarriorStats");
            stats.maxRage = 10;
            stats.maxHealth = 10;
            stats.attack = 10;
            stats.defense = 0;
            stats.xp = 10;
            warrior.Init(stats);

            Debug.Log("Hero life: " + warrior.currentHealth);
            Debug.Log("Hero defense: " + stats.defense);

            warrior.TakeDamage(-5);
            Assert.AreEqual(10, warrior.currentHealth);

            yield return null;

            //Test calcul damage reduction 
            stats.defense = 50;
            float damageReduction = warrior.CalculateDamageReduction();
            Assert.IsTrue(Utils.EqualFloat(damageReduction, 0.714f, 0.001f));

            //Test with defense = 0
            stats.defense = 0;
            stats.maxHealth = 10;
            Debug.Log("Hero life: " + warrior.currentHealth);
            Debug.Log("Hero defense: " + stats.defense);
            warrior.TakeDamage(5);
            Assert.AreEqual(5, warrior.currentHealth);

            Debug.Log("Hero life: " + warrior.currentHealth);
            warrior.TakeDamage(2);
            Assert.AreEqual(3, warrior.currentHealth);

            yield return null;

            //Test with defense = 100
            stats.defense = 100;
            warrior.currentHealth = 50;
            Debug.Log("Hero life: " + warrior.currentHealth);
            Debug.Log("Hero defense: " + stats.defense);
            warrior.TakeDamage(200);
            Assert.AreEqual(17, warrior.currentHealth);

            Debug.Log("Hero life: " + warrior.currentHealth);
            warrior.TakeDamage(60);
            Assert.AreEqual(7, warrior.currentHealth);

            GameObject.Destroy(warriorGO);
        }

        [Test]
        public void HeroTestAttack()
        {
            GameObject warriorGO = new GameObject();
            Warrior warrior = warriorGO.AddComponent<Warrior>();
            WarriorStats stats = (WarriorStats)ScriptableObject.CreateInstance("WarriorStats");
            stats.maxRage = 10;
            stats.maxHealth = 10;
            stats.attack = 10;
            stats.defense = 10;
            stats.xp = 10;
            warrior.Init(stats);

            GameObject enemyGO = new GameObject();
            Enemy enemy = enemyGO.AddComponent<Enemy>();
            EnemyStats enemyStats = (EnemyStats)ScriptableObject.CreateInstance("EnemyStats");
            enemyStats.maxHealth = 100;
            enemy.Init(enemyStats);

            warrior.Attack(enemy);
            Assert.IsTrue(enemy.currentHealth < enemy.GetStats().maxHealth);

            GameObject.Destroy(enemyGO);
            GameObject.Destroy(warriorGO);
        }
    }
}