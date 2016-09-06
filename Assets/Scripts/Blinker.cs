using UnityEngine;

namespace Assets.Scripts
{
    public class Blinker : MonoBehaviour
    {
        private float _nextTime;
        public float Interval = 1.0f;
        // 点滅周期
 
        // Use this for initialization
        private void Start()
        {
            _nextTime = Time.time;
        }
 
        // Update is called once per frame
        private void Update()
        {
            if (!(Time.time > _nextTime)) return;
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
 
            _nextTime += Interval;
        }
    }
}