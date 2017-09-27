using System.Activities;


namespace VIRAActivityLibrary
{
    public class InitializeVendorRequestInfo: CodeActivity
    {
        [RequiredArgument]
        public InArgument<VendorRequestInfo> VendorRequestInfo { get; set; }

        [RequiredArgument]
        public OutArgument<VendorRequestInfo> VendorRequestInfoOut { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            VendorRequestInfo VendorRequestInfo = this.VendorRequestInfo.Get(context);
            VendorRequestInfo.WorkflowInstanceId = context.WorkflowInstanceId;
            VendorRequestInfo.IsCompleted = false;
            VendorRequestInfo.IsCancelled = false;
            VendorRequestInfo.IsSuccess = false;
            this.VendorRequestInfoOut.Set(context, VendorRequestInfo);
        }
    }
}
