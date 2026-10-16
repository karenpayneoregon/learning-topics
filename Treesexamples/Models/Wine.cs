using System;
using System.Collections.Generic;
using System.Text;

namespace TreesExamples.Models
{
    public class Wine
    {
        public int WineId { get; set; }
        public string Name { get; set; }
        public WineType WineType { get; set; }
    }
    public enum WineType
    {
        Red = 1,
        White = 3,
        Rose = 2
    }
}
