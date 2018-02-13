using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using EVE.Common;
using EVE.ProcessingAgent.Contract.WorkItem;
using TestAutomation.Drivers.DBDriver.Tables;
using TestAutomation.Helpers;
using Config = EVE.Site.BLL.Config;

namespace TestAutomation.Steps.Integration.Handlers.Infrastructure
{
    public abstract class HandlersStepsBase
    {
        protected WorkItemBase.ExecutionOutcome HandlerOutcome;
        protected int ExhibitId;
        protected int MediaId;
        protected bool ExhibitSetup = false;
        
        protected string ExhibitConnectionString;

        protected virtual void Init()
        {
            var siteConnectionString = Config.GetConnectionString(DataStore.Site);
            ExhibitId = new ExhibitTable(siteConnectionString).GetFirstActiveExhibitId();
            

            if (ExhibitId == 0)
            {
                const string resourceLocation = "TestAutomation.Resources.DEI_Automation.vhd";

                //create the exhibit and hand off for processing.
                var exhibitHelper = new ExhibitHelper();
                var exhibitData = exhibitHelper.PrepareDbData(resourceLocation, AcquisitionFormatType.ForensicImage);
                exhibitHelper.ProcessExhibit(exhibitData.ExhibitId, exhibitData.MediaId, AcquisitionFormatType.ForensicImage);
                ExhibitId = exhibitData.ExhibitId;
                MediaId = exhibitData.MediaId;

                //wait while the exhibit processes...
                //check periodically upto 10 minutes, this will need to be adjusted if we process a larger exhibit - or come up with a better approach.
                var startTime = DateTime.Now;

                try
                {
                    while (!exhibitHelper.IsExhibitProcessed(exhibitData))
                    {
                        var elapsedTime = DateTime.Now.Subtract(startTime).TotalSeconds;
                        if (elapsedTime > 600)
                            throw new Exception("Exhibit Processing Time out");
                        Thread.Sleep(500);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Processing failed: {0}", ex.Message);
                    ExhibitSetup = false;
                    return;
                }
            }
            ExhibitConnectionString = EVE.Site.DAL.Config.GetConnectionString(DataStore.Exhibit).Replace(EVE.Site.DAL.Config.ExhibitPlaceholder, ExhibitId.ToString(CultureInfo.InvariantCulture));
            MediaId = new MediaTable(ExhibitConnectionString).GetFirstActiveMediaId();
            ExhibitSetup = true;
        }
    }
}
