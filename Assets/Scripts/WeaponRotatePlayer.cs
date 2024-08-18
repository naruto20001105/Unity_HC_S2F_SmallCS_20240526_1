using UnityEngine;
using UnityEngine.Rendering;

namespace Henry
{
    /// <summary>
    /// 武器旋轉：玩家
    /// </summary>
    public class WeaponRotatePlayer : WeaponRotate
    {
        protected override void Update()
        {
            MousePosition();
            base.Update();
        }

        private void MousePosition()
        {
            // 輸入 的 滑鼠座標
            Vector3 mousePoistion = Input.mousePosition;
            //print($"<color=#f33>滑鼠原始座標：{mousePoistion}</color>");

            //滑鼠座標 =主要攝影機 螢幕座標轉為世界座標
            mousePoistion = Camera.main.ScreenToWorldPoint(mousePoistion);
            print($"<color=#3f3>滑鼠世界座標：{mousePoistion}</color>");
            mousePoistion.z = 0;
            //準心的座標 = 滑鼠座標 (轉完)
            crossHair.position = mousePoistion;
        }
    }
    
}