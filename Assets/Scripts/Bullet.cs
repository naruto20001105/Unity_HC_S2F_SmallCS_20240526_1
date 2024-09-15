using UnityEngine;

namespace Henry
{ 
    /// <summary>
    /// 子彈
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        /// <summary>
        /// 子彈的傷害
        /// </summary>
        public float bulletDamage => dataWeapon.bulletDamage;

        [SerializeField, Header("武器資料")]
        private DataWeapon dataWeapon;

        private void Awake()
        {
            // 刪除(物件，延遲時間)
            Destroy(gameObject, dataWeapon.bulletLift);
        }

        // OCE2 碰撞事件，物件有碰撞器碰到其他物件時執行事件
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }
    }

    
}
