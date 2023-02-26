// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using AWSFundamentals;

var sqsClient = new AmazonSQSClient();
var customer = new CustomerCreated
{
    Id = Guid.NewGuid(),
    Email = "nehemiahlimo02@gmail.com",
    FullName = "Nehemiah Limo",
    DateOfBirth = new DateTime(1994, 1, 1),
    GitHubUsername = "NLimo"
};

var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");
var msgReq = new SendMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    MessageBody = JsonSerializer.Serialize(customer),
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType", new MessageAttributeValue
            {
                DataType="String",
                StringValue=nameof(CustomerCreated)
            }
        }
    },
    
};

var response = await sqsClient.SendMessageAsync(msgReq);



Console.WriteLine("Hello, World!" +response.ResponseMetadata);

