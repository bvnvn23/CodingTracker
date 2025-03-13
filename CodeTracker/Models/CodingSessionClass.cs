using Spectre.Console;

namespace CodingTracker.Models
{
    class CodingSessionClass
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }

        public CodingSessionClass()
        {
        }
        
        public CodingSessionClass(int id, string date, string startTime, string endTime, string duration)
        {
            Id = id;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
        }
    }
}
