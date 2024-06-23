using UnityEngine;

namespace Henry
{
    /// <summary>
    /// 控制系統
    /// </summary>
    public class ControlSystem : MonoBehaviour
    {
        // SerializeField 序列化，將數字顯示在面板
        // Header 標題，在變數上顯示文字
        // Range(最小，最大) 設定變數範圍限制
        [SerializeField, Header("移動速度"), Range(0, 100)]
        private float movespeed = 3.5f;

        private Rigidbody2D rig;s
        private Animator ani;
        private string parMove = "移動數值";

        private void Awake()
        {
            //獲得此物件身上的 2D 剛體並存放到變數 rig內
            rig = GetComponent<Rigidbody2D>();
            ani = GetComponent<Animator>();
        }
        private void Update()
        {
            // 呼叫自訂方法移動
            Move();
        }
        
        // 自訂方法移動
        private void Move() 
        {
            // 獲得玩家的水平按鍵：A、D與左右
            // 玩家按下左 -1，右 +1，沒按 0
            float h = Input.GetAxis("Horizontal");
            // 剛體的加速度 = 玩家水平按鍵 * 移動速度，Y 軸是原本的重力
            rig.velocity = new Vector2(h * movespeed, rig.velocity.y);
            // 對 h 取絕對值
            h = Mathf.Abs(h);
            print($"取絕對值後的 h 數值 {h}");
            // 設定浮點數參數 為 h
            ani.SetFloat(parMove, h);
        }
    }
}
