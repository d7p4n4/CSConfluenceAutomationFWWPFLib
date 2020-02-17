using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CSConfluenceAutomationFWWPFLib
{
    public class NewPageSuccessResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("space")]
        public Container Space { get; set; }

        [JsonProperty("history")]
        public History History { get; set; }

        [JsonProperty("version")]
        public Version Version { get; set; }

        [JsonProperty("ancestors")]
        public List<Ancestor> Ancestors { get; set; }

        [JsonProperty("container")]
        public Container Container { get; set; }

        [JsonProperty("body")]
        public Body Body { get; set; }

        [JsonProperty("extensions")]
        public Extensions Extensions { get; set; }

        [JsonProperty("_links")]
        public TemperaturesLinks Links { get; set; }

        [JsonProperty("_expandable")]
        public TemperaturesExpandable Expandable { get; set; }
    }

    public partial class Ancestor
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("extensions")]
        public Extensions Extensions { get; set; }

        [JsonProperty("_links")]
        public AncestorLinks Links { get; set; }

        [JsonProperty("_expandable")]
        public AncestorExpandable Expandable { get; set; }
    }

    public partial class AncestorExpandable
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

    public partial class Extensions
    {
        [JsonProperty("position")]
        public string Position { get; set; }
    }

    public partial class AncestorLinks
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

    public partial class Body
    {
        [JsonProperty("storage")]
        public Storage Storage { get; set; }

        [JsonProperty("_expandable")]
        public BodyExpandable Expandable { get; set; }
    }

    public partial class BodyExpandable
    {
        [JsonProperty("editor")]
        public string Editor { get; set; }

        [JsonProperty("view")]
        public string View { get; set; }

        [JsonProperty("export_view")]
        public string ExportView { get; set; }

        [JsonProperty("styled_view")]
        public string StyledView { get; set; }

        [JsonProperty("anonymous_export_view")]
        public string AnonymousExportView { get; set; }
    }

    public partial class Storage
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("representation")]
        public string Representation { get; set; }

        [JsonProperty("_expandable")]
        public StorageExpandable Expandable { get; set; }
    }

    public partial class StorageExpandable
    {
        [JsonProperty("content")]
        public string Content { get; set; }
    }

    public partial class Container
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("_links")]
        public ContainerLinks Links { get; set; }

        [JsonProperty("_expandable")]
        public ContainerExpandable Expandable { get; set; }
    }

    public partial class ContainerExpandable
    {
        [JsonProperty("metadata")]
        public string Metadata { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("homepage")]
        public string Homepage { get; set; }
    }

    public partial class ContainerLinks
    {
        [JsonProperty("webui")]
        public string Webui { get; set; }

        [JsonProperty("self")]
        public Uri Self { get; set; }
    }

    public partial class TemperaturesExpandable
    {
        [JsonProperty("metadata")]
        public string Metadata { get; set; }

        [JsonProperty("operations")]
        public string Operations { get; set; }

        [JsonProperty("children")]
        public string Children { get; set; }

        [JsonProperty("restrictions")]
        public string Restrictions { get; set; }

        [JsonProperty("descendants")]
        public string Descendants { get; set; }
    }

    public partial class History
    {
        [JsonProperty("latest")]
        public bool Latest { get; set; }

        [JsonProperty("createdBy")]
        public CreatedBy CreatedBy { get; set; }

        [JsonProperty("createdDate")]
        public DateTimeOffset CreatedDate { get; set; }

        [JsonProperty("_links")]
        public HistoryLinks Links { get; set; }

        [JsonProperty("_expandable")]
        public HistoryExpandable Expandable { get; set; }
    }

    public partial class CreatedBy
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
        public HistoryLinks Links { get; set; }

        [JsonProperty("_expandable")]
        public CreatedByExpandable Expandable { get; set; }
    }

    public partial class CreatedByExpandable
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class HistoryLinks
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

    public partial class HistoryExpandable
    {
        [JsonProperty("lastUpdated")]
        public string LastUpdated { get; set; }

        [JsonProperty("previousVersion")]
        public string PreviousVersion { get; set; }

        [JsonProperty("contributors")]
        public string Contributors { get; set; }

        [JsonProperty("nextVersion")]
        public string NextVersion { get; set; }
    }

    public partial class TemperaturesLinks
    {
        [JsonProperty("webui")]
        public string Webui { get; set; }

        [JsonProperty("edit")]
        public string Edit { get; set; }

        [JsonProperty("tinyui")]
        public string Tinyui { get; set; }

        [JsonProperty("collection")]
        public string Collection { get; set; }

        [JsonProperty("base")]
        public Uri Base { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("self")]
        public Uri Self { get; set; }
    }

    public partial class Version
    {
        [JsonProperty("by")]
        public CreatedBy By { get; set; }

        [JsonProperty("when")]
        public DateTimeOffset When { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("minorEdit")]
        public bool MinorEdit { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("_links")]
        public HistoryLinks Links { get; set; }

        [JsonProperty("_expandable")]
        public StorageExpandable Expandable { get; set; }
    }
}
