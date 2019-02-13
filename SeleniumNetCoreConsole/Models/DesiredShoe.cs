using System;
using System.Collections.Generic;

namespace SeleniumNetCoreConsole.Models
{
    public class DesiredShoe
    {
        public ShoeType ShoeType { get; set; }
        public string ShoeName { get; set; }
        public decimal MaxPrice { get; set; }
        public string TargetSize { get; set; }
        public List<String> AlternateSizes { get; set; }
        public string URL { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime PollingStartDate { get; set; }
        public DateTime PollingEndDate { get; set; }
        public Int32 RefreshInterval { get; set; }
        public Int32 MaxRefreshCount { get; set; }
        public Boolean Purchased { get; set; }
    }
}
