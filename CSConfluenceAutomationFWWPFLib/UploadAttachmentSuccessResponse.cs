using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CSConfluenceAutomationFWWPFLib
{
    class UploadAttachementSuccessResponse
    {
        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("_links")]
        public TemperaturesLinks Links { get; set; }
    }

    public partial class TemperaturesLinks
    {
        [JsonProperty("base")]
        public Uri Base { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("version")]
        public Version Version { get; set; }

        [JsonProperty("container")]
        public Container Container { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("extensions")]
        public ResultExtensions Extensions { get; set; }

        [JsonProperty("_links")]
        public ResultLinks Links { get; set; }

        [JsonProperty("_expandable")]
        public ResultExpandable Expandable { get; set; }
    }

    public partial class Container
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("extensions")]
        public ContainerExtensions Extensions { get; set; }

        [JsonProperty("_links")]
        public ContainerLinks Links { get; set; }

        [JsonProperty("_expandable")]
        public ContainerExpandable Expandable { get; set; }
    }

    public partial class ContainerExpandable
    {
        [JsonProperty("container")]
        public string Container { get; set; }

        [JsonProperty("metadata")]
        public string Metadata { get; set; }

        [JsonProperty("operations")]
        public string Operations { get; set; }

        [JsonProperty("children")]
        public string Children { get; set; }

        [JsonProperty("restrictions")]
        public string Restrictions { get; set; }

        [JsonProperty("history")]
        public string History { get; set; }

        [JsonProperty("ancestors")]
        public string Ancestors { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("descendants")]
        public string Descendants { get; set; }

        [JsonProperty("space")]
        public string Space { get; set; }
    }

    public partial class ContainerExtensions
    {
        [JsonProperty("position")]
        public string Position { get; set; }
    }

    public partial class ContainerLinks
    {
        [JsonProperty("webui")]
        public string Webui { get; set; }

        [JsonProperty("edit")]
        public string Edit { get; set; }

        [JsonProperty("tinyui")]
        public string Tinyui { get; set; }

        [JsonProperty("self")]
        public Uri Self { get; set; }
    }

    public partial class ResultExpandable
    {
        [JsonProperty("operations")]
        public string Operations { get; set; }

        [JsonProperty("children")]
        public string Children { get; set; }

        [JsonProperty("restrictions")]
        public string Restrictions { get; set; }

        [JsonProperty("history")]
        public string History { get; set; }

        [JsonProperty("ancestors")]
        public string Ancestors { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("descendants")]
        public string Descendants { get; set; }

        [JsonProperty("space")]
        public string Space { get; set; }
    }

    public partial class ResultExtensions
    {
        [JsonProperty("mediaType")]
        public string MediaType { get; set; }

        [JsonProperty("fileSize")]
        public long FileSize { get; set; }
    }

    public partial class ResultLinks
    {
        [JsonProperty("webui")]
        public string Webui { get; set; }

        [JsonProperty("download")]
        public string Download { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("self")]
        public Uri Self { get; set; }
    }

    public partial class Metadata
    {
        [JsonProperty("mediaType")]
        public string MediaType { get; set; }

        [JsonProperty("_expandable")]
        public MetadataExpandable Expandable { get; set; }
    }

    public partial class MetadataExpandable
    {
        [JsonProperty("currentuser")]
        public string Currentuser { get; set; }

        [JsonProperty("properties")]
        public string Properties { get; set; }

        [JsonProperty("frontend")]
        public string Frontend { get; set; }

        [JsonProperty("editorHtml")]
        public string EditorHtml { get; set; }
    }

    public partial class Version
    {
        [JsonProperty("by")]
        public By By { get; set; }

        [JsonProperty("when")]
        public DateTimeOffset When { get; set; }

        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("minorEdit")]
        public bool MinorEdit { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("_links")]
        public VersionLinks Links { get; set; }

        [JsonProperty("_expandable")]
        public VersionExpandable Expandable { get; set; }
    }

    public partial class By
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("userKey")]
        public string UserKey { get; set; }

        [JsonProperty("profilePicture")]
        public ProfilePicture ProfilePicture { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("_links")]
        public VersionLinks Links { get; set; }

        [JsonProperty("_expandable")]
        public ByExpandable Expandable { get; set; }
    }

    public partial class ByExpandable
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class VersionLinks
    {
        [JsonProperty("self")]
        public Uri Self { get; set; }
    }

    public partial class ProfilePicture
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("isDefault")]
        public bool IsDefault { get; set; }
    }

    public partial class VersionExpandable
    {
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}