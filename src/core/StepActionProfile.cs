﻿namespace BankSimulator.core
{
    public class StepActionProfile
    {
        public string Action { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Count { get; set; }
        public double Sum { get; set; }
        public double Avg { get; set; }
        public double Std { get; set; }
        public int Step { get; set; }

        public StepActionProfile(int step, string action, int month, int day, int hour, int count, double sum, double avg, double std)
        {
            Action = action;
            Month = month;
            Day = day;
            Hour = hour;
            Count = count;
            Sum = sum;
            Avg = avg;
            Std = std;
            Step = step;
        }
    }
}
