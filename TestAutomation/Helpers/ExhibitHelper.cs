using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using EVE.BLL;
using EVE.Serialization;
using System.Globalization;
using EVE.Common;
using TestAutomation.Drivers.DBDriver.Tables;
using TestAutomation.Model;
using TestAutomation.Model.DataModel;
using Activity = EVE.Site.BLL.Activity;
using Media = EVE.Site.BLL.Media;
using Work = EVE.Site.BLL.Work;
using WorkflowStep = EVE.Site.BLL.WorkflowStep;
using WorkflowStepEvent = EVE.Site.BLL.WorkflowStepEvent;
using WorkflowStepProperty = EVE.Site.BLL.WorkflowStepProperty;

namespace TestAutomation.Helpers
{
    public class ExhibitHelper
    {
        /// <summary>
        /// Process an exhibit with a custom workflow
        /// </summary>
        /// <param name="exhibitId"></param>
        /// <param name="mediaId"></param>
        /// <param name="workflow"></param>
        public void ProcessExhibit(int exhibitId, int mediaId, List<EVE.Workflow.WorkflowStep> workflow)
        {
            const bool isDistributable = false;
            int? parentWorkId = null;

            // Create the work item which will trigger processing.
            var workParams = Serializer.SerializeToBinary(new EVE.GroupController.Contract.Work.WorkBase.WorkParameters
            {
                RuntimeData = new Dictionary<string, string>
                        {
                            {"ExhibitId", exhibitId.ToString(CultureInfo.InvariantCulture)},
                            {"MediaId", mediaId.ToString(CultureInfo.InvariantCulture)}
                        }
            });

            Media.UpdateMedia_Workflow(exhibitId, mediaId, Serializer.SerializeToXml<List<EVE.Workflow.WorkflowStep>>(workflow));
            Work.InsertWork(typeof(EVE.GroupController.Contract.Work.ProcessMedia).FullName, workParams, isDistributable, parentWorkId);
            Media.UpdateMedia_Status(exhibitId, mediaId, MediaStatus.Queued);
        }

        /// <summary>
        /// Process an exhibit with the default workflow (full) of an specific acquisition format
        /// </summary>
        /// <param name="exhibitId"></param>
        /// <param name="mediaId"></param>
        /// <param name="acquisitionFormat"></param>
        public void ProcessExhibit(int exhibitId, int mediaId, AcquisitionFormatType acquisitionFormat)
        {
            // Get workflow steps by acquisition type
            var workflowSteps = WorkflowStep.GetWorkflowStep_AcquisitionFormat(acquisitionFormat);
            var workflowStepId = workflowSteps.Select(x => x.WorkflowStepId).Distinct().ToArray();
            var workflowStepEvents = WorkflowStepEvent.GetWorkflowStepEvent_WorkflowSteps(workflowStepId);
            var workflowStepProperties = WorkflowStepProperty.GetWorkflowStepProperty_WorkflowSteps(workflowStepId);
            var activities = Activity.GetActivity_All();

            var workflowHelper = new WorkflowHelper(workflowSteps, activities, workflowStepProperties, workflowStepEvents);

            var workflowCustom = new List<EVE.Workflow.WorkflowStep>();
            var initialSteps = (from workflowStep in workflowSteps
                                where workflowStepEvents.All(x => x.LinkedWorkflowStepId != workflowStep.WorkflowStepId)
                                select workflowStep).ToList();

            foreach (var step in initialSteps.Select(initialStep => workflowHelper.CreateStep(initialStep.WorkflowStepId)))
            {
                workflowCustom.Add(step);
                workflowHelper.ParseChildren(workflowCustom, step);
            }

            // Process default workflow
            ProcessExhibit(exhibitId, mediaId, workflowCustom);
        }

        public ExhibitHelperModel PrepareDbData(string evidencePath, AcquisitionFormatType type)
        {
            var exhibitDataModel = new ExhibitDataModel().GetDefault();
            var caseDataModel = new CaseDataModel().GetDefault();
            var mediaDataModel = new MediaDataModel().GetDefault();

            caseDataModel.SiteId = new SiteTable( Config.GetConnectionString(DataStore.Central)).GetSiteId();

            var caseId = Case.InsertCase(caseDataModel.PoliceFileNumber,
                 caseDataModel.EclCaseNumber,
                 caseDataModel.Description,
                 caseDataModel.OfficerInChargeId,
                 caseDataModel.DistrictId,
                 caseDataModel.SiteId,
                 caseDataModel.OffenceTypeId,
                 caseDataModel.CreatedById);

            var exhibitId = EVE.BLL.Exhibit.CreateExhibit(caseId,
               caseDataModel.CreatedById,
               exhibitDataModel.EclExhibitNumber,
               exhibitDataModel.Description,
               exhibitDataModel.PoliceExhibitNumber);

            mediaDataModel.EvidencePath = Path.GetFullPath(evidencePath);

            var mediaId = Media.InsertMedia(exhibitId,
                mediaDataModel.MediaNumber,
                mediaDataModel.Description,
                mediaDataModel.EvidencePath,
                MediaStatus.Defined,
                type, 
                string.Empty);

            return new ExhibitHelperModel {ExhibitId = exhibitId, MediaId = mediaId, CaseId = caseId, CasePoliceNumber = caseDataModel.PoliceFileNumber, EclExhibitNumber= exhibitDataModel.EclExhibitNumber, MediaNumber=mediaDataModel.MediaNumber };
        }

        public bool IsExhibitProcessed(ExhibitHelperModel model)
        {
            var media = Media.GetMedia(model.ExhibitId, model.MediaId);

            switch (media.MediaStatusCode)
            {
                case "DEFINED":
                case "QUEUED":
                case "PROC":
                case "REPROC":
                    return false;
                case "VALIDATIONERR":
                    throw new Exception("Exhibit Processing validation error");
                case "PROCESSED":
                    return true;
                case "PROCERR":
                    throw new Exception("Exhibit Processing Error");
            }
            return false;
        }
    }
}
