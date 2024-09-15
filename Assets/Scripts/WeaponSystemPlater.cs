using TMPro;
using UnityEngine;

namespace Henry
{
    /// <summary>
    /// 武器系統：玩家
    /// </summary>
    /// 子類別：父類別（繼承）
    public class WeaponSystemPlayer : WeaponSystem
    {
        [SerializeField, Header("是否預設武器")]
        private bool isDefaultWeapon;
        [SerializeField, Header("是否連射")]
        private bool isRepaid;
        [SerializeField, Header("是否無限子彈")]
        private bool isInifiniteBullet;
        [SerializeField, Header("介面父物件：按鈕武器")]
        private Transform uiParent;

        private TMP_Text textWeaponName;
        private TMP_Text textBulletCurrent;
        private TMP_Text textBulletTotal;
        private TMP_Text textMagazinePrice;

        // 開槍輸入按鍵，如果連射就使用 GetKey 否則使用 GetKeyDown
        private bool fireKey => isRepaid ? Input.GetKey(KeyCode.Mouse0) : Input.GetKeyDown(KeyCode.Mouse0);
        private bool reloadKey => Input.GetKeyDown(KeyCode.Mouse1);

        // 物件被啟動 (屬性面板最上方左邊的勾勾) 會執行一次
        private void OnEnable()
        {
            canFire = true;
        }

        protected override void Awake()
        {
            base.Awake();
            // 將 玩家的更新介面方法 放到 updateUI 資料裡面
            updateUI = UpdateUI;
            // 訂閱玩家購買彈匣事件
            // 當玩家購買彈匣後 會執行 OnPlayerBuyMagazine 方法
            GameManager.instance.onBuyMagazine += OnPlayerBuyMagazine;
        }

        private void OnPlayerBuyMagazine(object sender, DataWeapon e)
        {
            // 刪除此行：提醒要修改
            // throw new System.NotImplementedException();
            // 如果 玩家購買的 武器 與此武器相同 就 添加一個彈匣 並更新介面
            if (e == dataWeapon)
            {
                magazineCount++;
                UpdateUI();
            }
        }
        protected override void Update()
        {
            base.Update();
            Fire(fireKey);
            Reload(reloadKey);
#if UNITY_EDITOR
            // 如果 在編輯器內 才可以執行這邊的程式
            Test();
#endif
        }

        // 覆寫 override：覆寫父類別帶有虛擬關鍵字的成員
        protected override void Initialize()
        {
            // base 原本父類別的內容
            base.Initialize();

            // GetChid(編號) 透過編號取的子物件 0 代表父物件下面的第一物件
            textWeaponName = uiParent.GetChild(0).GetComponent<TMP_Text>();
            textBulletCurrent = uiParent.GetChild(1).GetComponent<TMP_Text>();
            textBulletTotal = uiParent.GetChild(2).GetComponent<TMP_Text>();
            textMagazinePrice = uiParent.GetChild(3).GetComponent<TMP_Text>();

            textWeaponName.text = dataWeapon.weaponName;            
            textMagazinePrice.text = $"價格：{dataWeapon.magazinePrice}";

            // 如果是無限子彈就給他 999 個彈匣 否則 就是 0 個彈匣
            magazineCount = isInifiniteBullet ? 999 : 0;
            UpdateUI();
            // 如果預設武器，不適預設武器就關閉
            gameObject.SetActive(isDefaultWeapon);
        }

        private void UpdateUI()
        {
            bulletTotal = dataWeapon.magazineBulletCount * magazineCount;
            textBulletCurrent.text = $"子彈：{bulletCurrent}";
            // 如果是無限子彈九顯示∞
            textBulletTotal.text = $"總數：{(isInifiniteBullet ? "<size=20>∞</size>" : bulletTotal)}";
        }

        protected override void Reload(bool reload)
        {
            base.Reload(reload);
            // 如果是無限子彈就給他 999 個彈匣 否則 就是 原本的彈匣
            magazineCount = isInifiniteBullet ? 999 : magazineCount;
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
