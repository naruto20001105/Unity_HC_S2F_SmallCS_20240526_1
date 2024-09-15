using UnityEngine;

namespace Henry
{
    public enum WeaponType
    {
        Pistol, MachineGun, ShotGun, Sniper
    }

    /// <summary>
    /// 控制系統：敵人 
    /// </summary>
    public class ControlSystemEnemy : ControlSystem
    {
        /// <summary>
        /// 檢查玩家是否在射縣內
        /// </summary>
        public bool checkPlayer => CheckPlayer();

        // 定義列舉
        // 列舉自帶有編號從零開始，手槍0、步槍1、散彈槍2、狙擊槍3



        [SerializeField, Header("敵人武器")]
        private WeaponType weaponType;
        [SerializeField, Header("武器物件")]
        private GameObject[] weapons;

        [Header("偵測玩家射線")]
        [SerializeField]
        private Color checkPlayerRayColor = new Color(0.5f, 1, 0.5f, 0.7f);
        [SerializeField, Range(0, 15)]
        private float checkPlayerLength = 3.5f;
        [SerializeField]
        private LayerMask checkPlayerLayer = 1 << 3 | 1 << 6;

        private Transform weaponFirePoint;
        private Transform player;

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            // 如果槍口為空值 就 跳出
            if (weaponFirePoint == null) return;
            Gizmos.color = checkPlayerRayColor;
            // 繪製射線(起點，方向 * 長度)
            Gizmos.DrawRay(
                weaponFirePoint.position, weaponFirePoint.right * checkPlayerLength);
        }

        protected override void Awake()
        {
            base.Awake();
            player = GameObject.Find(GameManager.playerName).transform;

            
        }

        protected override void Update()
        {
            // 如果 準心 是空的 就 跳出
            if (player == null) return;
            base.Update();

            // 如果射線打到玩家就停止
            if (CheckPlayer())
            {
                rig.velocity = Vector2.zero;
                ani.SetFloat(parMove, 0);
                return;
            }
            // 移動數值 = 玩家在左邊 -1，在右邊 +1
            float move = player.position.x < transform.position.x ? 1 : +1;
            Move(move);
            Ladder(move);
        }

        private bool CheckPlayer()
        {
            // 2D 物理射線碰撞(起點，方向，長度，圖層)
            RaycastHit2D hit = Physics2D.Raycast(weaponFirePoint.position, weaponFirePoint.right, checkPlayerLength, checkPlayerLayer);
            // 如果 碰到物件是空值 就傳回 false
            if (hit.collider == null) return false;
            // 如果碰到物件的名稱 等於 玩家的名稱 就傳回 ture
            return hit.collider.name.Equals(GameManager.playerName);
        }

        /// <summary>
        /// 設定武器類型
        /// </summary>
        /// <param name="_weaponType">武器類型</param>
        public void SetWaponType(WeaponType _weaponType)
        {
            weaponType = _weaponType;

            // 隱藏非選取武器，顯示選取的武器
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(i == (int)weaponType);
            }
            // 獲得顯示武器的子彈生成位置
            weaponFirePoint = weapons[(int)weaponType].transform.Find("子彈生成位置");
        }
    }
}
