using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CSConfluenceAutomationFWWPFLib
{
    public class NewPageErrorResponse
    {
        [JsonProperty("statusCode")]
        public long StatusCode { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("authorized")]
        public bool Authorized { get; set; }

        [JsonProperty("valid")]
        public bool Valid { get; set; }

        [JsonProperty("allowedInReadOnlyMode")]
        public bool AllowedInReadOnlyMode { get; set; }

        [JsonProperty("errors")]
        public List<object> Errors { get; set; }

        [JsonProperty("successful")]
        public bool Successful { get; set; }
    }
}