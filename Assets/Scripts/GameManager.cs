using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Henry
{
    /// <summary>
    /// 遊戲管理器
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region 資料
        // 公開給外部存取的變數
        public static GameManager instance
        {
            // 唯獨(其他腳本只能取得此資料)
            get
            {
                // 如果實體為空的 就尋找場景上有 GM 的物件並存放到實體
                if (_instance == null) _instance = FindAnyObjectByType<GameManager>();
                // 傳回實體
                return _instance;
            }
        }

        // 用來存放單例的變數(實體)
        private static GameManager _instance;

        // 定義事件，並且攜帶一個參數 DataWeapon，事件習慣用 on 開頭命名
        // 購買彈匣事件，攜帶當前購買的武器資料
        public event EventHandler<DataWeapon> onBuyMagazine;

        // const 常數：不會變得值
        // 存取方法：腳本名稱.常數名稱
        public const string playerName = "伊莉莎白";

        [SerializeField, Header("武器資料")]
        private DataWeapon[] dataWeapons;

        private TMP_Text textkillCount, textCoin, textDistance;
        private int killCount, coin;
        private int coinIncrease = 100;
        private Transform player;
        private float originalX;　　　　// 原始 X (玩家的起點)
        private float playerX;　　　　  // 玩家當前的 X
        private float totalX;　　　　　 // 距離總數
        private CanvasGroup groupFinal;
        private WaitForSeconds fadeInterval = new WaitForSeconds(0.02f);
        #endregion

        private TMP_Text textFinalTitle;
        
        private void Awake()
        {
            textkillCount = GameObject.Find("擊殺數").GetComponent<TMP_Text>();
            textCoin = GameObject.Find("金錢數").GetComponent<TMP_Text>();
            textDistance = GameObject.Find("文字行走距離").GetComponent<TMP_Text>();
            groupFinal = GameObject.Find("結束畫面組").GetComponent<CanvasGroup>();
            player = GameObject.Find(playerName).transform;
            originalX = player.position.x;
            playerX = player.position.x;
        }

        private void Update()
        {
            BuyMagazine();
            UpdateDistance();
        }

        /// <summary>
        /// 更新擊殺數與金幣數值跟介面
        /// </summary>
        public void UpdaeKillAndCoin()
        {
            killCount++;
            coin += coinIncrease;
            textkillCount.text = $"擊殺數量：{killCount}";
            textCoin.text = $"金幣：{coin}";
        }

        /// <summary>
        /// 顯示結束介面
        /// </summary>
        /// <param name="title">結束標題</param>
        public void ShowFinalUI(string title)
        { 
            textFinalTitle.text = title;
            StartCoroutine(Fade());
        }

        /// <summary>
        /// 購買彈匣
        /// </summary>
        private void BuyMagazine()
        {
            // 迴圈重複執行所有可買彈匣的武器
            for (int i = 0; i < dataWeapons.Length; i++)
            {
                // 獲得每一個武器的資料
                var weapon = dataWeapons[i];
                // 如果玩家按下該武器的購買按鈕
                if (Input.GetKeyDown(weapon.buyMagazineKey))
                {
                    // 如果 錢 小於 武器的彈匣價格 就 跳出
                    if (coin < weapon.magazinePrice) return;
                    // 扣錢與更新介面
                    coin -= weapon.magazinePrice;
                    textCoin.text = $"金幣:{coin}";
                    // 呼叫事件
                    // ?.Invoke 有人訂閱此事件 才會進行呼叫
                    // (呼叫事件的物件，攜帶的參數)
                    // 呼叫購買彈匣事件，並將此物件以及購買的武器資料傳遞出去
                    onBuyMagazine?.Invoke(this, weapon);
                    SoundManager.instance.PlaySound(SoundType.Buy);
                }
            }
        }

        /// <summary>
        /// 更新距離
        /// </summary>
        private void UpdateDistance()
        {
            // 如果玩家為空值 (死亡) 就跳出
            if (player == null) return;
            // 如果玩家 X 軸 小於 上次紀錄的 X 軸就跳出 (回頭不更新)
            if (player.position.x < playerX) return;
            // 紀錄玩家 X 座標
            playerX = player.position.x;
            // 計算距離總數並更新介面
            totalX = playerX - originalX;
            // F0 小數點 0 位數，F2 兩位數
            textDistance.text = $"{totalX.ToString("F0")} m";
        }

        private IEnumerator Fade()
        {
            // 遞增結束畫面透明度，每次 + 0.1，等待 0.02 秒
            for (int i = 0; i < 10; i++)
            {
                groupFinal.alpha += 0.1f;
                yield return fadeInterval;
            }
            // 結束畫面互動與遮擋勾選
            groupFinal.interactable = true;
            groupFinal.blocksRaycasts = true;
        }
    }
}
