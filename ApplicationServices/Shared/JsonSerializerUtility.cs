using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Formatting = Newtonsoft.Json.Formatting;

namespace ApplicationServices.Shared
{
	public static class JsonSerializerUtility
	{
		public static JsonSerializerSettings SnakeCaseSerializerSettings()
		{
			return new JsonSerializerSettings
			{
				ContractResolver = new DefaultContractResolver
				{
					NamingStrategy = new SnakeCaseNamingStrategy()
				},
				Formatting = Formatting.Indented,
				NullValueHandling = NullValueHandling.Ignore
			};
		}

		public static JsonSerializerSettings CamelCaseSerializerSettings()
		{
			return new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				Formatting = Formatting.Indented,
				NullValueHandling = NullValueHandling.Ignore
			};
		}

		public static JsonSerializerSettings PascalCaseSerializerSettings()
		{
			return new JsonSerializerSettings
			{
				ContractResolver = new DefaultContractResolver(),
				Formatting = Formatting.Indented,
				NullValueHandling = NullValueHandling.Ignore
			};
		}
	}
}