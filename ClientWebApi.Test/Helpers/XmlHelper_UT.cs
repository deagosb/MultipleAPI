using ClientWebApi.Helpers;
using ClientWebApi.Models;
using ClientWebApi.Services.Deal;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ClientWebApi.Test.Helpers
{
    public class XmlHelper_UT
    {
        private PostBestDealRequest input = new PostBestDealRequest()
        {
            SourceAddress = "Origin Address",
            DestinationAddress = "Destination Address",
            CartonDimensions = new int[] { 10, 15, 50 }
        };

        [Fact]
        public void SerializeObject_OK()
        {
            string serializedObject = XmlHelper.Serialize(input);
            Assert.NotNull(serializedObject);
        }

        [Fact]
        public void DeserializeObject_OK()
        {
            string serializedObject = XmlHelper.Serialize(input);
            PostBestDealRequest deserializedObject = XmlHelper.Deserialize<PostBestDealRequest>(serializedObject);
            Assert.Equal(input.SourceAddress, deserializedObject.SourceAddress);
            Assert.Equal(input.DestinationAddress, deserializedObject.DestinationAddress);
            Assert.Equal(input.CartonDimensions, deserializedObject.CartonDimensions);
        }
    }
}
