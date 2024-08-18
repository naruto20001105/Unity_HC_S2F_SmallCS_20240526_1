using UnityEngine;

namespace Henry
{
    /// <summary>
    /// 控制系統：玩家
    /// </summary>
    public class ControlSystemPlayer : ControlSystem
    {
        protected override void Update()
        {
        　　base.Update();
        　　PlayerInput();
　　　　}

        private void PlayerInput()
        {
            // 獲得玩家的水平按鍵：A、D 與左右
            // 玩家按下左 -1，右 +1，沒按 0
            float h = Input.GetAxis("Horizontal");
            Move(h);
            Ladder(h);
        }
    }
}

