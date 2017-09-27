
using System.Activities;


namespace VIRAActivityLibrary
{
    // Saves the information of a VendorRequestInfo. 
    public class SaveVendorRequestInfo: NativeActivity
    {
        [RequiredArgument]
        public InArgument<VendorRequestInfo> VendorRequestInfo { get; set; }

        public InArgument<bool> IsCompleted { get; set; }
        public InArgument<bool> IsSuccess { get; set; }
        public InArgument<bool> IsCancelled { get; set; }        

        protected override void Execute(NativeActivityContext context)
        {
            VendorRequestInfo VendorRequestInfo = this.VendorRequestInfo.Get(context);
            VendorRequestInfo.IsCompleted = this.IsCompleted.Get(context);
            VendorRequestInfo.IsCancelled = this.IsCancelled.Get(context);
            VendorRequestInfo.IsSuccess = this.IsSuccess.Get(context);
            VendorRequestRepository.Save(VendorRequestInfo);
        }
    }   
}
