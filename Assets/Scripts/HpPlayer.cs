using UnityEngine;

namespace Henry
{
    public class HpPlayer : HpSystem
    {
        protected override void Dead()
        {
            base.Dead();
            GameManager.instance.ShowFinalUI("挑戰失敗");
        }
    }
}
