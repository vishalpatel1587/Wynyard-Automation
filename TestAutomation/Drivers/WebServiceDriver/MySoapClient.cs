using System;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Messaging;

namespace TestAutomation.Drivers.WebServiceDriver
{
    public class MySoapClient : SoapClient
    {
        public MySoapClient(Uri endpointUrl)
            : base(endpointUrl)
        {
        }

        [SoapMethod("GetExhibitBookmark")]
        public SoapEnvelope RequestResponseMethod(SoapEnvelope envelope)
        {
            return base.SendRequestResponse("GetExhibitBookmark", envelope);
        }

    }
}
