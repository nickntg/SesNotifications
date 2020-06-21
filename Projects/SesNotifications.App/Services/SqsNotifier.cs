using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using NLog;
using SesNotifications.App.Models;
using SesNotifications.App.Services.Interfaces;

namespace SesNotifications.App.Services
{
    public class SqsNotifier : ISqsNotifier, IDisposable
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private AmazonSQSClient _sqsClient;

        public void Notify(string header, string message, SqsConfiguration configuration)
        {
            var client = CreateClient(configuration);

            try
            {
                var response = client.SendMessageAsync(new SendMessageRequest
                {
                    QueueUrl = configuration.QueueUrl,
                    MessageBody = message,
                    MessageAttributes = new Dictionary<string, MessageAttributeValue>
                    {
                        ["Title"] = new MessageAttributeValue { DataType = "String", StringValue = header }
                    }
                }, CancellationToken.None).Result;

                if (response.HttpStatusCode != HttpStatusCode.OK)
                {
                    Logger.Error($"Error while sending SNS notification, status code {response.HttpStatusCode}");
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, "Unexpected error while sending SQS notification");
            }
        }

        private AmazonSQSClient CreateClient(SqsConfiguration configuration)
        {
            if (_sqsClient == null)
            {
                _sqsClient = string.IsNullOrEmpty(configuration.AccessKey)
                    ? new AmazonSQSClient(new AmazonSQSConfig
                    {
                        RegionEndpoint = RegionEndpoint.GetBySystemName(configuration.Region)
                    })
                    : new AmazonSQSClient(new BasicAWSCredentials(configuration.AccessKey, configuration.SecretKey),
                        RegionEndpoint.GetBySystemName(configuration.Region));
            }

            return _sqsClient;
        }
        
        public void Dispose()
        {
            _sqsClient?.Dispose();
        }
    }
}
