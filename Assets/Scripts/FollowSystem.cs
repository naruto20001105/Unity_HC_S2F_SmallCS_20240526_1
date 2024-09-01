using UnityEngine;

namespace Henry
{ 
    /// <summary>
    /// 跟隨系統：跟隨指定物件
    /// </summary>
　　public class FollowSystem : MonoBehaviour
　　{
        [SerializeField, Header("跟隨目標")]
        private Transform target;
        [SerializeField, Header("位移"), Range(-3, 3)]
        private float offset;

        private void Update()
        {
            // 如果 準心 是空的 就 跳出
            if (target == null) return;
            Follow();
        }

        private void Follow()
        {
            // 此物件的座標 = 目標物件的座標
            transform.position = target.position + Vector3.up * offset;
        }

        public void SetTarget(Transform _target)
        {
            target = _target;
        }
    }
}