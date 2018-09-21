using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace _2c2pAssessment.WebApi.Controllers
{
	public class BaseController : Controller
	{
		protected JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() { Formatting = Formatting.Indented, ContractResolver = new CamelCasePropertyNamesContractResolver(), Converters = new List<JsonConverter> { new StringEnumConverter() } };
	}
}