using System;
using System.Threading.Tasks;
using Amazon.Pinpoint;
using Amazon.Pinpoint.Model;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace Api.Database.MySql
{
    public class AWSHelper
    {
        private static readonly IAmazonSQS sqsClient;
        private static readonly IAmazonPinpoint pinpointClient;

        private static readonly string pinpointAppId = "1eabd9c3c1fb4b6ca86c6963548e2494";

        private static readonly string queueUrl =
            "https://sqs.eu-west-1.amazonaws.com/460234074473/HandleMyCaseEmailService-EmailServiceQueue01EA8C78-1CHTN8ZULBUKN.fifo";

        static AWSHelper()
        {
            sqsClient = new AmazonSQSClient();
            pinpointClient = new AmazonPinpointClient();
        }

        public static async Task SendEmail(string messageBody, string messageGroupId, string messageDeduplicationId)
        {
            var sendMessageRequest = new SendMessageRequest();
            sendMessageRequest.MessageBody = messageBody;
            sendMessageRequest.MessageGroupId = messageGroupId;
            sendMessageRequest.MessageDeduplicationId = messageDeduplicationId;
            sendMessageRequest.QueueUrl = queueUrl;

            var response = await sqsClient.SendMessageAsync(sendMessageRequest);
        }

        public static async Task SendTotpMessage(string phoneNumber)
        {
            var sendMessageRequestParameters = new SendOTPMessageRequestParameters
            {
                Channel = "SMS",
                AllowedAttempts = 3,
                OriginationIdentity = "+18335381981",
                DestinationIdentity = $"+44{phoneNumber}",
                ValidityPeriod = 5,
                BrandName = "HelpMyCase",
                CodeLength = 6,
                ReferenceId = $"{phoneNumber}HelpMyCase",
                Language = "en-GB"
            };
            var sendTotp = new SendOTPMessageRequest
            {
                ApplicationId = pinpointAppId,
                SendOTPMessageRequestParameters = sendMessageRequestParameters
            };

            var response = await pinpointClient.SendOTPMessageAsync(sendTotp);
            Console.WriteLine(response);
        }

        public static async Task<VerifyOTPMessageResponse?> ValidateTotpMessage(string token, string phoneNumber)
        {
            var verifyOtpMessageRequestParams = new VerifyOTPMessageRequestParameters
            {
                Otp = token,
                DestinationIdentity = $"+44{phoneNumber}",
                ReferenceId = $"{phoneNumber}HelpMyCase"
            };
            var verifyMessageRequest = new VerifyOTPMessageRequest
            {
                ApplicationId = pinpointAppId,
                VerifyOTPMessageRequestParameters = verifyOtpMessageRequestParams
            };

            var response = await pinpointClient.VerifyOTPMessageAsync(verifyMessageRequest);
            return response;
        }
    }
}