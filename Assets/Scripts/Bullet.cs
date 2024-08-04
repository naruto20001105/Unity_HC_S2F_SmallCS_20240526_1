using UnityEngine;

namespace Henry
{ 
    /// <summary>
    /// 子彈
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        [SerializeField, Header("武器資料")]
        private Dataweapon dataWeapon;

        private void Awake()
        {
            // 刪除(物件，延遲時間)
            Destroy(gameObject, dataWeapon.bulletLift);
        }
    }

    
}
