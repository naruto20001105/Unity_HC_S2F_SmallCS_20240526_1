using System.Collections;
using UnityEngine;

namespace Henry
{
    /// <summary>
    /// 武器系統：敵人
    /// </summary>
    public class WeaponSystemEnemy : WeaponSystem
    {
        [Header("開槍間隔")]
        [SerializeField, Range(1, 3)]
        private float fireIntervalMin = 3;
        [SerializeField, Range(3, 6)]
        private float fireIntervalMax = 5;

        private ControlSystemEnemy controlSystemEnemy;
        private bool enemyfire;

        protected override void Awake()
        {
            base.Awake();
            controlSystemEnemy = transform.root.GetComponent<ControlSystemEnemy>();
        }

        protected override void Update() 
        {
            base.Update();
            EnemyInput();
        }

        private void EnemyInput()
        {
            // 如果再開槍就跳出
            if (enemyfire) return;
            Fire(controlSystemEnemy.checkPlayer);
            // 啟動開槍間隔協同程序
            StartCoroutine(FireInterval());
        }

        private IEnumerator FireInterval()
        {
            // 正在開槍中
            enemyfire = true;
            // 獲得隨機揩央間隔並等待
            float fireInterval =Random.Range(fireIntervalMin, fireIntervalMax);
            yield return new WaitForSeconds(fireInterval);
            // 恢復為沒有開槍
            enemyfire = false;
        }
    }
}
