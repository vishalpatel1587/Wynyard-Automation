using EVE.BLL;
using EVE.Common;
using TestAutomation.Drivers.DBDriver.Tables;

namespace TestAutomation.Model.DataModel
{
    public class CaseDataModel
    {
        public string PoliceFileNumber { get; set; }

        public string EclCaseNumber { get; set; }

        public string Description { get; set; }

        public string OfficerInCharge { get; set; }

        public string District { get; set; }

        public string OffenceType { get; set; }

        public string AcquisitionSite { get; set; }
        public int OfficerInChargeId { get; set; }
        public int DistrictId { get; set; }
        public int SiteId { get; set; }
        public int OffenceTypeId { get; set; }
        public int CreatedById { get; set; }

        public string CasesDropDownItems { get; set; }

        public CaseDataModel GetDefault()
        {
            //TODO OV retrieve ids from sql queries (like top 1 from Uset table etc)
            PoliceFileNumber = "Wyn_AutCase_" + System.Guid.NewGuid();
            EclCaseNumber = "Wyn_AutCase_" + System.Guid.NewGuid();
            Description = "***This is a test DEI case created by the automation script ***";
            OfficerInCharge = "Admin";
            OfficerInChargeId = 1;

            District = "Canterbury";
            /*Added the Wellington District for Local Machine*/
           // District = "Wellington";
            DistrictId = 1;

            OffenceType = "ASSAULT";
            OffenceTypeId = 1;

            AcquisitionSite = "Wynyard";
            SiteId = new SiteTable(Config.GetConnectionString(DataStore.Central)).GetSiteId();

            CreatedById = 1;
            CasesDropDownItems = "100";

            return this;
        }

    }
}
