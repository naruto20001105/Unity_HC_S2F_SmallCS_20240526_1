using UnityEngine;

namespace Henry
{
    /// <summary>
    ///  生成敵人系統
    /// </summary>
    public class SpawnEnem : MonoBehaviour
    {
        [SerializeField, Header("敵人預製物")]
        private  GameObject prefabEnemy;
        [SerializeField, Header("要生成的敵人資料")]
        private SpawnEnemyData[] spawnEnemyDatas;

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0.5f, 0.5f, 0.5f);

            for (int i = 0; i < spawnEnemyDatas.Length; i++)
            {
                // 繪製球體(座標，半徑)
                Gizmos.DrawSphere(
                    transform.position + spawnEnemyDatas[i].point, 0.2f);
            }
        }

        // 觸發事件：兩個物件其中一個有勾選 Is Trigger 可以使用
        // 快速完成 OTE2
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.name.Equals(GameManager.playerName)) 
            {
                spawn();
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// 生成
        /// </summary>
        private void spawn()
        {
            for (int i = 0;i < spawnEnemyDatas.Length;i++)
            {
                GameObject temp =Instantiate(prefabEnemy,
                    transform.position + spawnEnemyDatas[i].point,
                    Quaternion.identity);
                // 指定生成敵人的武器類型
                temp.GetComponent<ControlSystemEnemy>().
                    SetWaponType(spawnEnemyDatas[i].type);
            }
        }
    }

    // 預設 class 不會序列化(不顯示在面板上)
    // class 專用序列化
    [System.Serializable]
    public class SpawnEnemyData
    {
        public Vector3 point;
        public WeaponType type;
    }
}
