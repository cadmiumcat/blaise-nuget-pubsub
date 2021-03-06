﻿using Blaise.Nuget.PubSub.Core.Interfaces;
using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using System.Collections.Generic;

namespace Blaise.Nuget.PubSub.Core.Services
{
    public class PublishService : IPublishService
    {
        public void PublishMessage(string projectId, string topicId, string message, Dictionary<string, string> attributes = null)
        {
            var publisherService = PublisherServiceApiClient.Create();
            var topicName = new TopicName(projectId, topicId);
            var pubsubMessage = BuildPubsubMessage(message, attributes);

            publisherService.Publish(topicName, new[] { pubsubMessage });
        }

        private PubsubMessage BuildPubsubMessage(string message, Dictionary<string, string> attributes)
        {
            if (attributes == null)
            {
                return new PubsubMessage
                {
                    Data = ByteString.CopyFromUtf8(message),
                };
            }

            return new PubsubMessage
            {
                Data = ByteString.CopyFromUtf8(message),
                Attributes =
                {
                    attributes
                }
            };
        }
    }
}
