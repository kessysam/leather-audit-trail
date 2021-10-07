using System;
using System.Text.Json.Serialization;

namespace Domain
{
    public enum AuditType
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }
    public class ServiceAuditTrail
    {
        [JsonPropertyName("id")]
        public string id { get; set; }

        [JsonPropertyName("userId")]
        public Guid? UserId { get; set; }

        [JsonPropertyName("applicationName")]
        public string ApplicationName { get; set; }

        [JsonPropertyName("auditType")]
        public string AuditType { get; set; }

        [JsonPropertyName("tableName")]
        public string TableName { get; set; }

        [JsonPropertyName("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonPropertyName("oldValues")]
        public string OldValues { get; set; }

        [JsonPropertyName("newValues")]
        public string NewValues { get; set; }

        [JsonPropertyName("affectedColumns")]
        public string AffectedColumns { get; set; }

        [JsonPropertyName("primaryKey")]
        public string PrimaryKey { get; set; }
    }
}
