namespace TestAutomation.Model.DataModel
{
    //TODO OV change data model to reflect that some fields are the children of other fields (Images, Multimedia)
    //TODO OV this data model currently does not reflect that workflow options are configurable, and there is no 'default' workflow.
    public class WorkflowDataModel
    {
        public enum DefaultWorkflowOptionValues
        {
            Complete,
            None,
            Custom
        }

        public enum WorkflowMediaProcessingOptions
        {
            DeletedFiles,
            Lpp,
            Multimedia,
            VideoClassification,
            Images,
            ImageRating,
            ImageClassification,
            Content,
            Email,
            Browser,
            Uac
        }

        //Use the DefaultWorkflowOptionValues for this field in your test or set this field to the custom string value.
        public string WorkflowOption { get; set; }
        public bool DeletedFiles { get; set; }
        public bool Lpp { get; set; }
        public bool Multimedia { get; set; }
        public bool VideoClassification { get; set; }
        public bool Images { get; set; }
        public bool ImageRating { get; set; }
        public bool ImageClassification { get; set; }
        public bool Content { get; set; }
        public bool Email { get; set; }
        public bool Browser { get; set; }
        public bool Uac { get; set; }
        public string CustomWorkflowName { get; set; }

        public WorkflowDataModel GetDefault()
        {
            CustomWorkflowName = "Workflow_" + System.Guid.NewGuid();
            WorkflowOption = DefaultWorkflowOptionValues.Complete.ToString();
            DeletedFiles = true;
            Lpp = true;
            Multimedia = true;
            VideoClassification = true;
            Images = true;
            ImageRating = true;
            ImageClassification = true;
            Content = true;
            Email = true;
            Browser = true;
            Uac = true;
            return this;
        }

        public WorkflowDataModel OverrideDefaultValues(WorkflowDataModel newWorkflowDataModel)
        {
            if (!Multimedia)
            {
                VideoClassification = false;
            }

            if (!Images)
            {
                ImageRating = false;
                ImageClassification = false;
            }
            return this;
        }
    }
}
