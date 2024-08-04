using UnityEngine;

namespace Henry
{
    // MonoBehaviour 允許腳本掛在遊戲物件上
    // ScriptableObject 腳本化物件：將程式內容存放在專案內

    // 建立素材自定義菜單 menuName = "主菜單/子菜單"
    [CreateAssetMenu(menuName = "Henry/Weapon")]
    public class Dataweapon : ScriptableObject
    {
        [Header("武器名稱")]
        public string weaponName;
        [Header("彈匣裝彈量"), Range(0, 60)]
        public int magazineBulletCount;
        [Header("彈匣價格"), Range(0, 1000)]
        public int magazinePrice;
        [Header("彈速"), Range(0, 2000)]
        public int bulletSpeed;
        [Header("子彈生命"), Range(0, 2)]
        public float bulletLift;
        [Header("子彈傷害"), Range(0, 100)]
        public float bulletDamage;
        [Header("子彈後座力"), Range(0, 50)]
        public float bulletRecoil;
        [Header("子彈冷卻"), Range(0, 1)]
        public float bulletCD;
        [Header("彈匣冷卻"), Range(0, 2)]
        public float magazineCD;
        [Header("子彈預製物")]
        public GameObject bulletPrefab;
    }
}
