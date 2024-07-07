using UnityEngine;
using UnityEngine.Rendering;

namespace Henry
{
    /// <summary>
    /// 旋轉武器
    /// </summary>
    public class WeaponRotate : MonoBehaviour
    {
        [SerializeField, Header("準心")]
        private Transform crossHair;
        [SerializeField, Header("要旋轉的物件")]
        private Transform rotateTarget;
        [SerializeField, Header("要翻面的武器")]
        private SpriteRenderer sprWeapon;

        private void Update()
        {
           MousePosition();
           Rotate();
           Flip();
        }

        private void MousePosition()
        {
            // 輸入 的 滑鼠座標
            Vector3 mousePoistion =Input.mousePosition;
            //print($"<color=#f33>滑鼠原始座標：{mousePoistion}</color>");

            //滑鼠座標 =主要攝影機 螢幕座標轉為世界座標
            mousePoistion = Camera.main.ScreenToWorldPoint(mousePoistion);
            print($"<color=#3f3>滑鼠世界座標：{mousePoistion}</color>");
            mousePoistion.z = 0;
            //準心的座標 = 滑鼠座標 (轉完)
            crossHair.position = mousePoistion; 
        }

        private void Rotate()
        {
            // 武器前方的軸 指定為 準心與武器的向量
            transform.right = crossHair.position - transform.position;
        }

        private void Flip()
        {
            //如果 準心的 X > 要旋轉物件的 X 代表在右邊
            if (crossHair.position.x > rotateTarget.position.x)
            {
                rotateTarget.eulerAngles = Vector3.zero;
                sprWeapon.flipY = false;
            }
            // 否則 在左邊
            else
            {
                rotateTarget.eulerAngles = new Vector3(0, 180, 0);
                sprWeapon.flipY = true;

            }
        }
    }
}
