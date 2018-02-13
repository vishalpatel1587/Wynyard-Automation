using System;
using System.Web.Services.Protocols;
using Microsoft.Web.Services3;
using NUnit.Framework;
using TestAutomation.Drivers.WebServiceDriver;

namespace TestAutomation.Drivers.WebServiceDriver
{
    //Using GetExhibitBookmark web service as an example. 
    //[TestFixture]
    public class WsDriverUsingWebServices3Library
    {
       // [Test]
        public void SendRequest()
        {
            var requestString = "<tem:GetExhibitBookmark>\r\n";
            requestString = requestString + "<tem:exhibitBookmarkId>6107</tem:exhibitBookmarkId>\r\n";
            requestString = requestString + "</tem:GetExhibitBookmark>";

            var envelope = CreateEnvelopeForGetExhibitBookmark(requestString);
            var client = new MySoapClient(new Uri("http://localhost:300/EVE.Site.Services/ExhibitBookmarkService.svc"))
            {
                SoapVersion = SoapProtocolVersion.Soap12
            };
            var returnEnvelope = client.RequestResponseMethod(envelope);
        }

        private static SoapEnvelope CreateEnvelopeForGetExhibitBookmark(string request)
        {
            var envelope = new SoapEnvelope(SoapProtocolVersion.Soap12);
            envelope.Envelope.SetAttribute("xmlns:tem", "http://tempuri.org/");
            var header = envelope.CreateHeader();
            header.SetAttribute("xmlns:wsa", "http://www.w3.org/2005/08/addressing");
            header.InnerXml = "<wsa:To>http://localhost:300/EVE.Site.Services/ExhibitBookmarkService.svc</wsa:To>";
            var body = envelope.CreateBody();

            body.InnerXml = request;
            return envelope;
        }
    }
}
