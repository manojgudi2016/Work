using System;
using System.Activities;
using System.Activities.Tracking;


namespace VIRAActivityLibrary
{    
    // Emits a custom tracking record containing information about the last operation in the process
    public sealed class SaveActionTrackingInfo : CodeActivity
    {
        [RequiredArgument]
        public InArgument<string> VendorRequestId   { get; set; }
        public InArgument<string> State             { get; set; }
        public InArgument<string> Comment           { get; set; }
        public InArgument<string> Action            { get; set; }
       
        protected override void Execute(CodeActivityContext context)
        {            
            // create and set the record data
            CustomTrackingRecord customRecord = new CustomTrackingRecord("ActionExecuted");
            customRecord.Data.Add("VendorRequestId", this.VendorRequestId.Get(context));
            customRecord.Data.Add("State", this.State.Get(context));
            customRecord.Data.Add("Date", DateTime.Now);
            customRecord.Data.Add("Action", this.Action.Get(context));
            customRecord.Data.Add("Comment", this.Comment.Get(context));            
       
            context.Track(customRecord);
        }
    }
}