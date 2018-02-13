using EVE.Common;
using EVE.Data;
using EVE.Workflow;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomation.Helpers
{
    public class WorkflowHelper
    {
        public WorkflowStepList WorkflowSteps { get; set; }
        public ActivityList Activities { get; set; }
        public IEnumerable<WorkflowStepPropertyList.WorkflowStepPropertyRow> WorkflowStepProperties { get; set; }
        public IEnumerable<WorkflowStepEventList.WorkflowStepEventRow> WorkflowStepEvents { get; set; }

        public WorkflowHelper()
        {
        }

        public WorkflowHelper(WorkflowStepList workflowSteps, ActivityList activities, 
                IEnumerable<WorkflowStepPropertyList.WorkflowStepPropertyRow> workflowStepProperties,
                IEnumerable<WorkflowStepEventList.WorkflowStepEventRow> workflowStepEvents)
        {
            this.WorkflowSteps = workflowSteps;
            this.Activities = activities;
            this.WorkflowStepProperties = workflowStepProperties;
            this.WorkflowStepEvents = workflowStepEvents;
        }

        public WorkflowStep CreateStep(int workflowStepId)
        {
            var workflowStep = WorkflowSteps.First(x => x.WorkflowStepId == workflowStepId);
            var activity = Activities.First(x => x.ActivityId == workflowStep.ActivityId);

            var step = new WorkflowStep
            {
                WorkflowStepId = workflowStep.WorkflowStepId,
                ActivityId = workflowStep.ActivityId,
                ActivityTypeId = activity.ActivityTypeId,
                ExecutionTimeout = activity.ExecutionTimeout,
                HandlerType = activity.HandlerType,
                Priority = activity.Priority
            };

            // Add properties to step.
            var stepProperties = WorkflowStepProperties.Where(x => x.WorkflowStepId == workflowStep.WorkflowStepId).ToList();
            if (stepProperties.Count > 0)
            {
                step.StepProperties = new List<StepProperty>();
                foreach (var p in stepProperties)
                {
                    step.StepProperties.Add((StepProperty)Enum.Parse(typeof(StepProperty), p.StepPropertyId.ToString(CultureInfo.InvariantCulture)));
                }
            }

            return step;
        }

        public void ParseChildren(List<WorkflowStep> workflow, WorkflowStep parentStep)
        {
            // Associate all subsequent steps with this step.
            parentStep.StepEvents = LinkSteps(parentStep.WorkflowStepId, null);

            if (parentStep.StepEvents != null)
            {
                // Only create a step when the step has not been already added (eg. known file will have been created as part 
                // of file discovery, compound handler will also link to it but we don't want to re-create it for compound.
                foreach (var step in from stepEvent in parentStep.StepEvents
                                     where workflow.Any(x => x.WorkflowStepId == stepEvent.LinkedWorkflowStepId) == false
                                     select CreateStep(stepEvent.LinkedWorkflowStepId))
                {
                    workflow.Add(step);
                    ParseChildren(workflow, step);
                }
            }
        }

        private List<StepEventItem> LinkSteps(int workflowStepId, List<StepEventItem> stepEvents)
        {
            // Determine the Events associated with this step.
            var linkedStepEvents = WorkflowStepEvents.Where(x => x.WorkflowStepId == workflowStepId).ToList();
            if (!linkedStepEvents.Any()) return null;

            if (stepEvents == null) stepEvents = new List<StepEventItem>();

            foreach (var linkedStepEvent in linkedStepEvents)
            {
                var step = CreateStep(linkedStepEvent.LinkedWorkflowStepId);

                // If the User cannot modify the Workflow or the step is mandatory.
                //if (!WorkflowDiv.Visible || (step.StepProperties != null && step.StepProperties.Any(x => x == StepProperty.IsMandatory)))
                //{

                    stepEvents.Add(new StepEventItem
                    {
                        Event = (StepEvent)Enum.Parse(typeof(StepEvent), linkedStepEvent.StepEventId.ToString(CultureInfo.InvariantCulture)),
                        LinkedWorkflowStepId = linkedStepEvent.LinkedWorkflowStepId
                    });

                //}
                //else
                //{
                //    // The step is not mandatory which means it would have been displayed for user selection.
                //    // Check if the step is TypeOptional
                //    if (step.StepProperties != null && step.StepProperties.Any(x => x == StepProperty.IsTypeOptional))
                //    {
                //        // Step is TypeOptional which means there would have been an activity 
                //        // displayed (1st level of treee = ActivityTypes, 2nd level = Activities).
                //        if (ActivityTypeTreeView.GetAllNodes().Where(node => node.Level == 1 && int.Parse(node.Value) == step.WorkflowStepId).Any(node => node.Checked))
                //        {
                //            stepEvents.Add(new StepEventItem
                //            {
                //                Event = (StepEvent)Enum.Parse(typeof(StepEvent), linkedStepEvent.StepEventId.ToString(CultureInfo.InvariantCulture)),
                //                LinkedWorkflowStepId = linkedStepEvent.LinkedWorkflowStepId
                //            });
                //        }
                //        else
                //        {
                //            // Step has not been selected. Add its children to this step.
                //            LinkSteps(linkedStepEvent.LinkedWorkflowStepId, stepEvents);
                //        }
                //    }
                //    else if (ActivityTypeTreeView.GetAllNodes().Where(node => node.Level == 0 && int.Parse(node.Value) == step.ActivityTypeId).Any(node => node.Checked))
                //    {
                //        stepEvents.Add(new StepEventItem
                //        {
                //            Event = (StepEvent)Enum.Parse(typeof(StepEvent), linkedStepEvent.StepEventId.ToString(CultureInfo.InvariantCulture)),
                //            LinkedWorkflowStepId = linkedStepEvent.LinkedWorkflowStepId
                //        });
                //    }
                //    else
                //    {
                //        // Step has not been selected. Add its children to this step.
                //        LinkSteps(linkedStepEvent.LinkedWorkflowStepId, stepEvents);
                //    }
                //}
            }

            return stepEvents;
        }

    }
}
