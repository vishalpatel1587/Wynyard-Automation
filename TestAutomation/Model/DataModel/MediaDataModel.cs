namespace TestAutomation.Model.DataModel
{
    public class MediaDataModel
    {
        public string MediaNumber { get; set; }

        public string Description { get; set; }

        public string EvidencePath { get; set; }

        public string AcquisitionFormat { get; set; }

        public string TimeZone { get; set; }

        public MediaDataModel GetDefault()
        {
            MediaNumber = "Media_" + System.Guid.NewGuid();
            Description = "Description_" + System.Guid.NewGuid();
            AcquisitionFormat = "ForensicImage";
            TimeZone = null;
            return this;
        }

        public MediaDataModel GetDefaultUfed()
        {
            MediaNumber = "Media_" + System.Guid.NewGuid();
            Description = "Description_" + System.Guid.NewGuid();
            AcquisitionFormat = "MobileDeviceUfed";
            EvidencePath = "E:\\Testpath";
            TimeZone = "(UTC+12:00) Auckland, Wellington (12:00:00)";
            return this;
            
        }

        public MediaDataModel GetDefaultXry()
        {
            MediaNumber = "Media_" + System.Guid.NewGuid();
            Description = "Description_" + System.Guid.NewGuid();
            AcquisitionFormat = "MobileDeviceXry";
            EvidencePath = "E:\\Testpath";
            TimeZone = "(UTC+12:00) Auckland, Wellington (12:00:00)";
            return this;

        }

    }
}
