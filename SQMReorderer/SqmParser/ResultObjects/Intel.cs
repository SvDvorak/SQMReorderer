﻿namespace SQMReorderer.SqmParser.ResultObjects
{
    public class Intel
    {
        public string BriefingName { get; set; }
        public string BriefingDescription { get; set; }
        public double? StartWeather { get; set; }
        public double? ForecastWeather { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public int? Hour { get; set; }
        public int? Minute { get; set; }
    }
}
