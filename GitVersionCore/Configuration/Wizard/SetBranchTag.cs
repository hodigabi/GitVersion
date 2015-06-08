namespace GitVersion
{
    using System.Collections.Generic;

    public class SetBranchTag : ConfigInitWizardStep
    {
        string name;
        readonly BranchConfig branchConfig;

        public SetBranchTag(string name, BranchConfig branchConfig)
        {
            this.branchConfig = branchConfig;
            this.name = name;
        }

        protected override StepResult HandleResult(string result, Queue<ConfigInitWizardStep> steps, Config config)
        {
            if (string.IsNullOrWhiteSpace(result))
            {
                return StepResult.InvalidResponseSelected();
            }

            switch (result)
            {
                case "0":
                    steps.Enqueue(new ConfigureBranch(name, branchConfig));
                    return StepResult.Ok();
                case "1":
                    branchConfig.Tag = string.Empty;
                    steps.Enqueue(new ConfigureBranch(name, branchConfig));
                    return StepResult.Ok();
                default:
                    branchConfig.Tag = result;
                    steps.Enqueue(new ConfigureBranch(name, branchConfig));
                    return StepResult.Ok();
            }
        }

        protected override string GetPrompt(Config config)
        {
            return @"This sets the pre-release tag which will be used for versions on this branch (beta, rc etc)

0) Back
1) No tag

Anything else will be used as the tag";
        }

        protected override string DefaultResult
        {
            get { return "0"; }
        }
    }
}