using UnityEngine;

namespace Henry
{
    /// <summary>
    /// �ǰe�}�޲z��
    /// </summary>
    public class TelepornManager : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.name.Equals(GameManager.playerName))
            {
                GameManager.instance.ShowFinalUI("�D�Ԧ��\");
            }
        }
    }
}
