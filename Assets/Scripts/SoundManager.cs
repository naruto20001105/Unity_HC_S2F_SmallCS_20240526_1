using UnityEngine;

namespace Henry
{
    /// <summary>
    /// 音效管理器
    /// </summary>
    // 要求元件，在套用此腳本時會添加小括號內的元件
    // 套用音效管理器腳本到物件十戶同步添加音效來源元件
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<SoundManager>();
                return _instance;
            }
            
        }
        private static SoundManager _instance;

        [SerializeField, Header("音效")]
        private AudioClip[] sounds;

        private AudioSource aud;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="soundType"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void PlaySound(SoundType soundType,float min = 0.8f,float max =1.2f)
        {
            // 隨機音量
            float volume = Random.Range(min,max);
            // 撥放一次音效(音效，音量)
            aud.PlayOneShot(sounds[(int)soundType], volume);
        }

        /// <summary>
        /// 播放音量
        /// </summary>
        /// <param name="sound">音效類型</param>
        /// <param name="min">最小音量</param>
        /// <param name="max">最大音量</param>
        public void PlaySound(AudioClip sound, float min = 0.8f, float max = 1.2f)
        {
            // 隨機音量
            float volume = Random.Range(min, max);
            // 撥放一次音效(音效，音量)
            aud.PlayOneShot(sound, volume); 
        }
    }   

    /// <summary>
    /// 音效類型：順序與音效陣列相同
    /// </summary>
    public enum SoundType
    {
         Reload, Empty, Buy, Hit, Dead
    }
}

