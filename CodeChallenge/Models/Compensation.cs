using System;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        public String Id { get; set; } //this doubles as a primary and unique key
        public int Salary { get; set; }
        public String EffectiveDate { get; set; }
    }
}
