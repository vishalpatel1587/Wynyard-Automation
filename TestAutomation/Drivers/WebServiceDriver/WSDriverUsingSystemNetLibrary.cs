using System.Net;
using System.Text;
using System.Web.Services.Protocols;
using Microsoft.Web.Services3;

namespace TestAutomation.Drivers.WebServiceDriver
{
    //Using GetExhibitBookmark web service as an example. 
   // [TestFixture]
    public class WsDriverUsingSystemNetLibrary
    {
        //[Test]
        public void SendRequest()
        {
            var requestString = "<tem:GetExhibitBookmark>\r\n";
            requestString = requestString + "<tem:exhibitBookmarkId>6107</tem:exhibitBookmarkId>\r\n";
            requestString = requestString + "</tem:GetExhibitBookmark>";

            var envelope = CreateEnvelopeForGetExhibitBookmark(requestString);
            var request = CreateClient();

            var data = Encoding.UTF8.GetBytes(envelope.InnerXml.ToString());
            var stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);

            var response = request.GetResponse() as HttpWebResponse;
        }

        private static HttpWebRequest CreateClient()
        {
            var request =
                (HttpWebRequest)WebRequest.Create("http://localhost:300/EVE.Site.Services/ExhibitBookmarkService.svc?WSDL") as
                    HttpWebRequest;

            request.Method = "POST";
            request.ContentType =
                "application/soap+xml;charset=UTF-8;action=\"http://tempuri.org/IExhibitBookmark/GetExhibitBookmark\"";
            request.Expect = "application/soap+xml";
            request.AllowWriteStreamBuffering = true;
            return request;
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
