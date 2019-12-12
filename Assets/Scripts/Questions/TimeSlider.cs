using UnityEngine;
using UnityEngine.EventSystems;

namespace Questions
{
    public class TimeSlider : MonoBehaviour
    {

        public float Seconds = 5.0f;
        private float elapsed = 0.0f;
        private RectTransform _sliderTransform;
        private GameObject _eventHandler;

        // Start is called before the first frame update
        void Start()
        {
            _eventHandler = ExecuteEvents.GetEventHandler<ITimeSliderEventHandler>(transform.gameObject);
            _sliderTransform = transform.Find("Fill Area").GetComponent<RectTransform>();
        }

        public void Restart()
        {
            elapsed = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {
            elapsed += Time.deltaTime;

            if (Seconds - elapsed <= 0.0f)
            {
                GameObject target;
                ExecuteEvents.Execute<ITimeSliderEventHandler>(_eventHandler, null,
                    (handler, data) => { handler.TimeEnd(); });
            }
            else
            {
                _sliderTransform.sizeDelta = new Vector2(500.0f / Seconds * elapsed, 10.0f);
            }
        }
    }
}