using System;
using System.Collections.Generic;

namespace _2c2pAssessment.DataContext
{
    public partial class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
