using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UW_Solution
{
   
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<MsgResponseTwModel> MsgList { get; set; }


        public MainPage()
        {
            this.InitializeComponent();
             MsgList = new List<MsgResponseTwModel>() ;

            MsgList= listSms();
            dataMsg.ItemsSource = MsgList;
        }

        public List<MsgResponseTwModel> listSms()
        {
            MsgResponseTwModel msg = new MsgResponseTwModel();
            var restClient = new RestClient("http://localhost:35225/");
            restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            RestRequest restRequest = new RestRequest("api/MsgResponseTw", Method.GET);

            IRestResponse restResponse = restClient.Execute(restRequest);

            List<MsgResponseTwModel> result = JsonConvert.DeserializeObject<List<MsgResponseTwModel>>(restResponse.Content);
        
            return result;
        }

        public List<MsgResponseTwModel> SendSms()
        {
            MsgResponseTwModel msg = new MsgResponseTwModel();
            var restClient = new RestClient("http://localhost:35225/");
            restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            RestRequest restRequest = new RestRequest("api/Messsage", Method.POST);
            restRequest.AddJsonBody(new
            {
                ToNumber = txtToNumber.Text,
                FromNumber= "+14798472349",
                Message= txtMessage.Text
            });
            IRestResponse restResponse = restClient.Execute(restRequest);

            List<MsgResponseTwModel> result = JsonConvert.DeserializeObject<List<MsgResponseTwModel>>(restResponse.Content);
       

            return result;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MsgList = new List<MsgResponseTwModel>();

            MsgList = SendSms();
            dataMsg.ItemsSource = MsgList;
            txtMessage.Text = "";
            txtToNumber.Text = "";
        }


    }
}
