using UnityEngine;

namespace Henry
{
    /// <summary>
    ///  生成敵人系統
    /// </summary>
    public class SpawnEnem : MonoBehaviour
    {
        [SerializeField, Header("要生成的敵人資料")]
        private SpawnEnemyData[] spawnEnemyDatas;
        
        // 觸發事件：兩個物件其中一個有勾選 Is Trigger 可以使用
        // 快速完成 OTE2
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.name.Equals(GameManager.playerName)) 
            {
                print("出現敵人");
                Destroy(gameObject);
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
