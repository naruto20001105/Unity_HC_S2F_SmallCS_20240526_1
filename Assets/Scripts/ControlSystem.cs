using UnityEngine;

namespace Henry
{
    /// <summary>
    /// 控制系統
    /// </summary>
    public class ControlSystem : MonoBehaviour
    {
        #region 資料
        [SerializeField, Header("移動速度"), Range(0, 10)]
        private float movespeed = 3.5f;
        [SerializeField, Header("爬梯速度"), Range(0, 10)]
        private float ladderspeed = 3.5f;
        // Color(紅，綠，藍，透明度) 值 :0 ~ 1 (百分比)
        [SerializeField, Header("爬梯區域顏色")]
        private Color ladderColor = new Color(1, 0.3f, 0.3f, 0.7f);
        [SerializeField, Header("爬梯區域尺寸")]
        private Vector3 ladderSize;
        [SerializeField, Header("爬梯區域位移")]
        private Vector3 ladderOffset;
        [SerializeField, Header("爬梯區域圖層")]
        private LayerMask laddersLayer = 1 << 3;

        protected Rigidbody2D rig;
        protected Animator ani;
        protected string parMove = "移動數值";
        #endregion

        #region 事件
        // ODG 繪製圖示事件，在編輯器內繪製圖提示圖示
        protected virtual void OnDrawGizmos()
        {
            //決定圖示顏色
            Gizmos.color = ladderColor;
            //決定圖示形狀(座標，尺寸)
            //transform.position 此物件的座標
            Gizmos.DrawCube(transform.position + ladderOffset, ladderSize);
        }

        protected virtual void Awake()
        {
            //獲得此物件身上的 2D 剛體並存放到變數 rig內
            rig = GetComponent<Rigidbody2D>();
            ani = GetComponent<Animator>();
        }
       
        protected virtual void Update()
        {
            // 呼叫自訂方法移動
            // Move();
            // Ladder();
        }
        #endregion

        #region 方法
        protected void Move(float move)
        {

            // 剛體的加速度 = 玩家水平按鍵 * 移動速度，Y 軸是原本的重力
            rig.velocity = new Vector2(move * movespeed, rig.velocity.y);
            // 對 h 取絕對值
            move = Mathf.Abs(move);
            // 設定浮點數參數 為 move
            ani.SetFloat(parMove, move);
        }

        protected void Ladder(float move)
        {
            // 2D 物理.覆蓋立體物(座標，尺寸，角度，圖層)
            Collider2D hit = Physics2D.OverlapBox(transform.position + ladderOffset,
                ladderSize, 0, laddersLayer);

            // 如果 hit 是空的 就不執行下面的程式 (跳出)
            if (hit == null) return;

            // 如果 玩家 水平值絕對值 小於 0.2 就 跳出
            // float h = Input.GetAxis("Horizontal");

            if (Mathf.Abs(move) < 0.2f) return;

            rig.velocity = new Vector2(rig.velocity.x, ladderspeed);
        } 
        #endregion
    }
}
