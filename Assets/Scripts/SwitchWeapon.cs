using UnityEngine;

namespace Henry
{
    /// <summary>
    /// 切換武器
    /// </summary>
    public class SwitchWeapon : MonoBehaviour
    {
        // 陣列： 儲存多筆相同類型的資料
        [SerializeField, Header("所有武器")]
        private GameObject[] weapons;
        [SerializeField, Header("更換武器按鍵")]
        private KeyCode[] weaponKeys =
        {
            KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4
        };

        private void Update()
        {
            Switch();
        }

        private void Switch()
        {
            // 迴圈執行全部的按鍵
            for (int i = 0; i < weapons.Length; i++)
            {
                // 如果玩家按下是武器按鍵
                if (Input.GetKeyDown(weaponKeys[i])) 
                {
                    // 先關閉所有武器
                    for (int j = 0; j < weapons.Length; j++)
                    {
                        weapons[j].SetActive(false);
                    }
                    // 在打開玩家按下的武器，i 玩家按下武器的編號
                    weapons[i].SetActive(true);
                }
            }
        }
    }
}
