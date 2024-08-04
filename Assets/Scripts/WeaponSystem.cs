using UnityEngine;
using System.Collections;
using System;
// 指定只用 Unity Engine 的隨機 API
using Random = UnityEngine.Random;

namespace Henry
{
    /// <summary>
    /// 武器系統
    /// </summary>
    public class WeaponSystem : MonoBehaviour
    {
        [SerializeField, Header("武器資料")]
        protected Dataweapon dataWeapon;
        [SerializeField, Header("子彈生成位置")]
        private Transform spawnBulletPoint;
        [SerializeField, Header("生成子彈數量"), Range(1, 20)]
        private int spawnBulletCount = 1;
        [SerializeField, Header("生成子彈左右位移"), Range(0, 5)]
        private int spawnBulletXOffset;

        protected int bulletCurrent;
        protected int bulletTotal;
        protected int magazineCount;
        // 能不能開槍，預設值為 true 代表一開始可以開槍
        private bool canFire = true;
        // 是否再換彈匣
        private bool isReload;

        // Action 儲存方法
        protected Action updateUI;

        protected virtual void Awake()
        {
            Initialize();
        }

        protected virtual void Update()
        {
            
        }

        // 修飾詞
        // 私人 private：僅限此類別存取
        // 公開 public：所有類別都可存取
        // 保護 protected：允許子類別存取

        // 虛擬 virtual：允許子類別覆寫

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Initialize()

        {
            bulletCurrent = dataWeapon.magazineBulletCount;
            bulletTotal = 0;
        }

        /// <summary>
        /// 開槍方法
        /// </summary>
        /// <param name="fire">是否要開槍</param >
        protected void Fire(bool fire)
        {
            // 如果 不能開槍 就 跳出
            if (!canFire) return;
            // 如果 目前子彈 <= 0 就 跳出
            if (bulletCurrent <= 0) return;
            // 如果 按下左鍵 就 生成子彈
            if (fire)
            {
                SpawnBullet();
                // 扣一顆子彈
                bulletCurrent--;
                // 如果 UpdateUI 不適空的，就執行它
                updateUI?.Invoke();
                StartCoroutine(BulletCD());
            }
        }

        private void SpawnBullet()
        {
            for (int i = 0; i < spawnBulletCount; i++) 
            {
                float xFloat = Random.Range(0f, spawnBulletXOffset);
                // 生成(物件，座標，角度)
                // Quaternion.identity 零度角
                GameObject tempBullet = Instantiate(dataWeapon.bulletPrefab, spawnBulletPoint.position + Vector3.right * xFloat, Quaternion.identity);
                // Y 軸的浮動設定 = 隨機的範圍(-後座力，+後座力)
                float yFloat = Random.Range(-dataWeapon.bulletRecoil, dataWeapon.bulletRecoil);
                // 獲得生成子彈的 2D 剛體 並添加推力 往子彈生成位置前方 (X軸) 發射
                tempBullet.GetComponent<Rigidbody2D>().AddForce(spawnBulletPoint.right * dataWeapon.bulletSpeed + Vector3.up * yFloat);
            }
            
        }

        private IEnumerator BulletCD()
        {
            // 不能開槍
            canFire = false;
            // 等待子彈冷卻
            yield return new WaitForSeconds(dataWeapon.bulletCD);

            canFire = true;
        }

        /// <summary>
        /// 換彈匣
        /// </summary>
        /// <param name="reload">是否要換彈匣</param >
        protected virtual void Reload(bool reload)
        {
            // 如果 再換彈匣 就跳出
            if (isReload) return;
            // 如果 沒有 彈匣 或者 滿彈 (當前子彈等於彈匣可裝子彈數) 就 跳出
            if (magazineCount <= 0 || bulletCurrent == dataWeapon.magazineBulletCount) return;

            if (reload)
            {
                StartCoroutine(ReloadHandle());
            }
        }

        private IEnumerator ReloadHandle()
        {
            // 換彈匣中
            isReload = true;
            // 當前子彈數歸零並更新介面
            bulletCurrent = 0;
            updateUI?.Invoke();
            // 等待換彈匣
            yield return new WaitForSeconds(dataWeapon.magazineCD);
            // 裝填子彈
            bulletCurrent = dataWeapon.magazineBulletCount;
            // 扣除一個彈匣並更新介面
            magazineCount--;
            updateUI?.Invoke() ;
            // 換彈匣結束
            isReload = false;
        }   
    }
}
