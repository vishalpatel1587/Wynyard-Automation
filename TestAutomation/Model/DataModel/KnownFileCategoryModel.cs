using System;

namespace TestAutomation.Model.DataModel
{
    public class KnownFileCategoryModel
    {
        public int KnownfileCategoryId { get; set; }
        public int HashSetId { get; set; }
        public string CategoryName { get; set; }
        public string Vendor { get; set; }
        public string Package { get; set; }
        public string Version { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsNotable { get; set; }
        public string Initials { get; set; }
        public int? NumberOfFiles { get; set; }
        public string CategoryDescription { get; set; }
        public DateTime DateLoaded { get; set; }
    }
}
