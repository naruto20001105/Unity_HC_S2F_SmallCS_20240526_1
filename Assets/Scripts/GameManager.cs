using TMPro;
using UnityEngine;

namespace Henry
{
    /// <summary>
    /// 遊戲管理器
    /// </summary>
    public class GameManager : MonoBehaviour
    {
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

        // const 常數：不會變得值
        // 存取方法：腳本名稱.常數名稱
        public const string playerName = "伊莉莎白";

        private TMP_Text textkillCount, textCoin;
        private int killCount, coin;
        private int coinIncrease = 100;

        private void Awake()
        {
            textkillCount = GameObject.Find("擊殺數").GetComponent<TMP_Text>();
            textCoin = GameObject.Find("金錢數").GetComponent<TMP_Text>();
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
    }
}
