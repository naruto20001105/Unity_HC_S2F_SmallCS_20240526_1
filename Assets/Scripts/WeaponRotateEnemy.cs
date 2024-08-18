using UnityEngine;

namespace Henry
{
    /// <summary>
    /// 武器旋轉：敵人
    /// </summary>
　　public class WeaponRotateEnemy : WeaponRotate
    {
        private void Awake()
        {
            // GameObject.Find(物件名稱) 透過物件名稱尋找物件，不能再 Update 使用
            // 將準心指命為玩家
            crossHair = GameObject.Find(GameManager.playerName).transform;
        }
    }
}
