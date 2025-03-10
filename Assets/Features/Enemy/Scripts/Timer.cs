namespace Feature.Enemy
{
    public class Timer
    {
        private float _CurrentTime;

        public bool IsFinished => _CurrentTime <= 0;

        public Timer(float startTime)
        {
            Start(startTime);
        }

        public void Start(float startTime)
        {
            _CurrentTime = startTime;
        }

        public void RemoveTime(float deltaTime)
        {
            if (_CurrentTime <= 0) return;

            _CurrentTime -= deltaTime;
        }
    }
}