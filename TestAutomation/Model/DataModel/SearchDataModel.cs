using TestAutomation.Drivers.DBDriver.Tables;

namespace TestAutomation.Model.DataModel
{
    public class SearchDataModel
    {
        public string PoliceFileNumber { get; set; }
        public string EclCaseNumber { get; set; }
        public string Description { get; set; }
        public string OfficerInCharge { get; set; }
        public int OfficerInChargeId { get; set; }
        public string District { get; set; }
        public int DistrictId { get; set; }
        public int SiteId { get; set; }
        public string AcquisitionSite { get; set; }
        public string OffenceType { get; set; }
        public int OffenceTypeId { get; set; }
        public int CreatedById { get; set; }
        public string SearchMobileNumber { get; set; }
        public string SearchContactName { get; set; }
        public string SearchImageName { get; set; }

        public SearchDataModel GetDefault()
        {
            //TODO OV retrieve ids from sql queries (like top 1 from Uset table etc)
            PoliceFileNumber = "Wyn_AutCase_" + System.Guid.NewGuid();
            EclCaseNumber = "Wyn_AutCase_" + System.Guid.NewGuid();
            Description = "***This is a test DEI case created by the automation script ***";
            OfficerInCharge = "Admin";
            OfficerInChargeId = 1;

            District = "Wellington";
            DistrictId = 1;

            OffenceType = "BURGLARY";
            OffenceTypeId = 1;

            AcquisitionSite = "Wynyard";
            SiteId = 1;

            CreatedById = 1;

            SearchMobileNumber = "+64278145947";
            //SearchContactName = "addie";
            //SearchImageName = "Pic_0305_013.jpg";
            return this;
        }

    }
}
