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
        protected Transform crossHair;
        [SerializeField, Header("要旋轉的物件")]
        private Transform rotateTarget;
        [SerializeField, Header("要翻面的武器")]
        private SpriteRenderer sprWeapon;

        protected virtual void Update()
        {
           Rotate();
           Flip();
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
