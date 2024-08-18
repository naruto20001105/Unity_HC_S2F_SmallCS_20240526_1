using UnityEngine;

namespace Henry
{
　　/// <summary>
  　/// 控制系統：敵人 
  　/// </summary>
    public class ControlSystemEnemy : ControlSystem
    {
        // 定義列舉
        // 列舉自帶有編號從零開始，手槍0、步槍1、散彈槍2、狙擊槍3
        private enum WeaponType
        {
            Pistol, MachineGun, ShotGun, Sniper
        }

        [SerializeField, Header("敵人武器")]
        private WeaponType weaponType;
        [SerializeField, Header("武器物件")]
        private GameObject[] weapons;

        private Transform weaponFirePoint;

        [Header("偵測玩家射線")]
        [SerializeField]
        private Color checkPlayerRayColor = new Color(0.5f, 1, 0.5f, 0.7f);
        [SerializeField, Range(0, 15)]
        private float checkPlayerLength = 3.5f;
        [SerializeField]
        private LayerMask checkPlayerLayer = 1 << 3 | 1 << 6;

        protected override void Awake()
        {
            base.Awake();

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
