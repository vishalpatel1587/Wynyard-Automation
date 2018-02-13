using System.IO;

namespace TestAutomation.Model.DataModel
{
    public class ExhibitDataModel
    {
        public string EclExhibitNumber { get; set; }

        public string PoliceExhibitNumber { get; set; }

        public string Description { get; set; }

        public string OfficerInCharge { get; set; }

        public string District { get; set; }

        public string OffenceType { get; set; }

        public string AcquisitionSite { get; set; }

        public ExhibitDataModel GetDefault()
        {
            EclExhibitNumber = "Exhibit_" + System.Guid.NewGuid();
            PoliceExhibitNumber = "Exhibit_" + System.Guid.NewGuid();
            Description = "***This is a test DEI exhibit created by the automation script ***";
            return this;
        }

    }
}
