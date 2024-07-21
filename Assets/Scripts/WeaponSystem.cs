using TMPro;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

namespace Henry
{
    /// <summary>
    /// 武器系統
    /// </summary>
    public class WeaponSystem : MonoBehaviour
    {
        [SerializeField, Header("武器資料")]
        private Dataweapon dataWeapon;
        [SerializeField, Header("子彈生成位置")]
        private Transform spawnBulletPoint;
        [Header("介面")]
        [SerializeField]
        private TMP_Text textWeaponName;
        [SerializeField]
        private TMP_Text textBulletCurrent;
        [SerializeField]
        private TMP_Text textBulletTotal;
        [SerializeField]
        private TMP_Text textMagazinePrice;

        private int bulletCurrent;
        private int bulletTotal;
        private int magazineCount;
        // 能不能開槍，預設值為 true 代表一開始可以開槍
        private bool canFire = true;
        // 是否再換彈匣
        private bool isReload;

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            Fire();
            Reload();
#if UNITY_EDITOR
            // 如果 在編輯器內 才可以執行這邊的程式
            Test();
#endif
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            textWeaponName.text = dataWeapon.weaponName;
            textBulletCurrent.text = $"子彈：{dataWeapon.magazineBulletCount}";
            textBulletTotal.text = "總數：0";
            textMagazinePrice.text = $"子彈：{dataWeapon.magazinePrice}";
            bulletCurrent = dataWeapon.magazineBulletCount;
            bulletTotal = 0;
        }

        private void Fire()
        {
            // 如果 不能開槍 就 跳出
            if (!canFire) return;
            // 如果 目前子彈 <= 0 就 跳出
            if (bulletCurrent <= 0) return;
            // 如果 按下左鍵 就 生成子彈
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // 生成(物件，座標，角度)
                // Quaternion.identity 零度角
                GameObject tempBullet = Instantiate(dataWeapon.bulletPrefab, spawnBulletPoint.position, Quaternion.identity);
                // 獲得生成子彈的 2D 剛體 並添加推力 往子彈生成位置前方 (X軸) 發射
                tempBullet.GetComponent<Rigidbody2D>().AddForce(spawnBulletPoint.right * dataWeapon.bulletSpeed);
                // 扣一顆子彈
                bulletCurrent--;
                UpdateUI();
                StartCoroutine(BulletCD());
            }
        }

        private IEnumerator BulletCD()
        {
            // 不能開槍
            canFire = false;
            // 等待子彈冷卻
            yield return new WaitForSeconds(dataWeapon.bulletCD);
        }

        private void UpdateUI()
        {
            textBulletCurrent.text = $"子彈：{bulletCurrent}";
            textBulletTotal.text = $"總數：{dataWeapon.magazineBulletCount * magazineCount}";
        }

        private void Reload()
        {
            // 如果 再換彈匣 就跳出
            if (isReload) return;
            // 如果 沒有 彈匣 或者 滿彈 (當前子彈等於彈匣可裝子彈數) 就 跳出
            if (magazineCount <= 0 || bulletCurrent == dataWeapon.magazineBulletCount) return;

            if (Input.GetKeyDown(KeyCode.Mouse1))
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
            UpdateUI();
            // 等待換彈匣
            yield return new WaitForSeconds(dataWeapon.magazineCD);
            // 裝填子彈
            bulletCurrent = dataWeapon.magazineBulletCount;
            // 扣除一個彈匣並更新介面
            magazineCount--;
            UpdateUI() ;
            // 換彈匣結束
            isReload = false;
        }

        /// <summary>
        /// 測試用：添加彈匣
        /// </summary>
        private void Test()
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                magazineCount++;
                UpdateUI();
            }
        }
    }
}
