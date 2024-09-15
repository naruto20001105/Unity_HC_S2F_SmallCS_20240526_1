using UnityEngine;
using UnityEngine.UI;

namespace Henry
{ 
    /// <summary>
    /// 血量系統
    /// </summary>
    /// 
    public class HpSystem : MonoBehaviour
    {
        [SerializeField, Header("血條圖片")]
        private Image image;
        [SerializeField, Header("爆炸特效")]
        private GameObject explosion;

        private float hp = 100, hpMax = 100;
        private string bulletName = "子彈";
        // 碰撞事件
        // OnCollisiom：兩個物件碰撞器都沒勾選 Is Trigger
        // OnTrigger：輛個物件碰撞器其中一個勾選 Is Trigger
        // Enter：碰到開始執行一次
        // Exit：碰撞結束執行一次
        // Stay：碰撞期間持續執行約每秒 60 次

        // OCE2
        // collision 碰到物件的資訊
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // 如果 捧到物件的名稱 包含 子彈兩個字 就受傷
            if (collision.gameObject.name.Contains(bulletName)) 
            {
                float bulletDamage = collision.gameObject.GetComponent<Bullet>().bulletDamage;
                Damage(bulletDamage);
            }
        }

        private void Damage (float damage)
        {
            hp -= damage;
            // 音效管理器單例 撥放音效(受傷音效)
            SoundManager.instance.PlaySound(SoundType.Hit);
            image.fillAmount = hp / hpMax;
            if (hp <= 0) Dead();
        }

        protected virtual void Dead()
        {
            // print("<color=#f31>死亡</color>");
            SoundManager.instance.PlaySound(SoundType.Dead);
            GameObject temp =Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(temp, 1);
            Destroy(gameObject);
        }

        public void SetImgHp(Image _imgHp)
        { 
            image = _imgHp;
        }
    }
}
