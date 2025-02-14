using System;

namespace Activities
{
    abstract class Activity
    {
        private DateTime _date;
        private int _duration;

        public Activity(DateTime date, int duration)
        {
            _date = date;
            _duration = duration;
        }

        protected DateTime Date => _date;
        protected int Duration => _duration;

        public abstract double GetDistance();
        public abstract double GetSpeed();
        public abstract double GetPace();

        public virtual string GetSummary()
        {
            return string.Format("{0:dd MMM yyyy} {1} ({2} min): Distance {3:F2} km, Speed {4:F2} kph, Pace {5:F2} min per km", 
                                 Date, GetType().Name, Duration, GetDistance(), GetSpeed(), GetPace());
        }
    }
}
