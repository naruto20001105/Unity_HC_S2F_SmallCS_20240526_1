using UnityEngine;
using System.Collections;


namespace Henry
{
    /// <summary>
    /// 學習協同程序 Coroutine
    /// </summary>
    public class LeamCoroutine : MonoBehaviour
    {
        // 1. 引用 system.Collections;
        // 2. 定義傳回 IEnumerator 方法
        // 3. 方法內使用關鍵字 yield return 時間
        // 4. 使用 StartCoroutine 啟動方法

        private void Awake()
        {
            // 一般方法呼叫
            // Test();

            StartCoroutine(Test());
        }

        private IEnumerator Test()
        {
            print("<color=#f36>第一行</color>");
            // yield return new WaitForSeconds(1); 停止一秒
            yield return new WaitForSeconds(1);
            print("<color=#f36>第二行</color>");
            yield return new WaitForSeconds(2);
            print("<color=#f36>第三行</color>");
        }
    }
}

