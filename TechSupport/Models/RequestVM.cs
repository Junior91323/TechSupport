using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechSupport.Models
{
    public class RequestVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public int StateId { get; set; }
        public string State { get; set; }
    }
}