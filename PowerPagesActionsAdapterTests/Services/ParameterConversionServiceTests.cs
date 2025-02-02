using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using NSubstitute;
using PowerPagesActionsAdapter.Plugins;

namespace PowerPagesActionsAdapter.Services.Tests
{
    [TestClass()]
    public class ParameterConversionServiceTests
    {
        private ParameterConversionService service;
        private ILocalPluginContext localPluginContext;
        private ITracingService tracingService;

        [TestInitialize]
        public void Setup()
        {
            tracingService = Substitute.For<ITracingService>();
            localPluginContext = Substitute.For<ILocalPluginContext>();
            localPluginContext.TracingService.Returns(tracingService);

            service = new ParameterConversionService(localPluginContext);
        }

        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertBool()
        {
            // Arrange
            var input = "{ input: true }";
            var expected = true;
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertString()
        {
            // Arrange
            var input = "{ input: \"test\" }";
            var expected = "test";
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertInt()
        {
            // Arrange
            var input = "{ input: 1 }";
            var expected = 1;
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertFloat()
        {
            // Arrange
            var input = "{ input: 5.5 }";
            var expected = 5.5;
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertStringArray()
        {
            // Arrange
            var input = "{ input: [\"test\", \"test2\"] }";
            var expected = new string[] { "test", "test2" };
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);
            var resultArray = result as string[];

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected.Length, resultArray?.Length);
            Assert.AreEqual(expected.First(), resultArray?.First());
            Assert.AreEqual(expected.Last(), resultArray?.Last());
        }

        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertGuid()
        {
            // Arrange
            var input = "{ input: \"00000000-0000-0000-0000-000000000001\" }";
            var expected = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertDecimal()
        {
            // Arrange
            var input = "{ input: \"10.4m\" }";
            var expected = 10.4m;
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertDate()
        {
            // Arrange
            var input = "{ input: \"2024-08-03T15:27:41Z\" }";
            var expected = DateTime.Parse("2024-08-03T15:27:41Z");
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);
            var date = result as DateTime?;

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.IsNotNull(date);
            Assert.AreEqual(expected.ToUniversalTime(), date.Value.ToUniversalTime());
        }

        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertPickList()
        {
            // Arrange
            var input = "{ input: { OptionSetValue: 1 } }";
            var expected = new OptionSetValue(1);
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertMoney()
        {
            // Arrange
            var input = "{ input: { Money: 7.8 } }";
            var expected = new Money(7.8m);
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);
            var money = result as Money;

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected.Value, money?.Value);
        }

        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertEntityReference()
        {
            // Arrange
            var input = "{ input: { LogicalName: \"contact\", Id: \"4121902e-0530-ef11-8409-6045bd9e7366\" } }";
            var expected = new EntityReference("contact", Guid.Parse("4121902e-0530-ef11-8409-6045bd9e7366"));
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);
            var entityReference = result as EntityReference;

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected.LogicalName, entityReference?.LogicalName);
            Assert.AreEqual(expected.Id, entityReference?.Id);
        }

        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertEntity()
        {
            // Arrange
            var input = "{ input: { LogicalName: \"contact\", Id: \"4121902e-0530-ef11-8409-6045bd9e7366\", Attributes: { firstname: \"Marius\", lastname: \"Wodtke\" } } }";
            var expected = new Entity("contact", Guid.Parse("4121902e-0530-ef11-8409-6045bd9e7366"));
            expected.Attributes["firstname"] = "Marius";
            expected.Attributes["lastname"] = "Wodtke";
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);
            var entity = result as Entity;

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected.LogicalName, entity?.LogicalName);
            Assert.AreEqual(expected.Id, entity?.Id);
            Assert.AreEqual(expected["firstname"], entity?["firstname"]);
            Assert.AreEqual(expected["lastname"], entity?["lastname"]);
        }

        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertEntityCollection()
        {
            // Arrange
            var input = "{ input: { EntityName: \"contact\", Entities: [ { LogicalName: \"contact\", Id: \"4121902e-0530-ef11-8409-6045bd9e7366\", Attributes: { firstname: \"Marius\", lastname: \"Wodtke\" } } ] } }";
            var expected = new Entity("contact", Guid.Parse("4121902e-0530-ef11-8409-6045bd9e7366"));
            expected.Attributes["firstname"] = "Marius";
            expected.Attributes["lastname"] = "Wodtke";
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);
            var entityCollection = result as EntityCollection;
            var entity = entityCollection?.Entities.FirstOrDefault();

            // Assert
            Assert.AreEqual(typeof(EntityCollection), result.GetType());
            Assert.AreEqual(expected.LogicalName, entityCollection?.EntityName);
            Assert.AreEqual(expected.LogicalName, entity?.LogicalName);
            Assert.AreEqual(expected.Id, entity?.Id);
            Assert.AreEqual(expected["firstname"], entity?["firstname"]);
            Assert.AreEqual(expected["lastname"], entity?["lastname"]);
        }

        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertHintedString()
        {
            // Arrange
            var input = "{ \"input@string\": \"4121902e-0530-ef11-8409-6045bd9e7366\" }";
            var expected = "4121902e-0530-ef11-8409-6045bd9e7366";
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertHintedInt()
        {
            // Arrange
            var input = "{ \"input@int\": \"1\" }";
            var expected = 1;
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertHintedDecimal()
        {
            // Arrange
            var input = "{ \"input@decimal\": \"10.4\" }";
            var expected = 10.4m;
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertHintedBool()
        {
            // Arrange
            var input = "{ \"input@bool\": \"True\" }";
            var expected = true;
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertHintedGuid()
        {
            // Ideas for a better test welcome...
            // Arrange
            var input = "{ \"input@guid\": \"4121902e-0530-ef11-8409-6045bd9e7366\" }";
            var expected = Guid.Parse("4121902e-0530-ef11-8409-6045bd9e7366");
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertHintedFloat()
        {
            // Arrange
            var input = "{ \"input@float\": \"5.5\" }";
            var expected = 5.5;
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertHintedDateTime()
        {
            // Ideas for a better test welcome...
            // Arrange
            var input = "{ \"input@datetime\": \"2024-08-03T15:27:41Z\" }";
            var expected = DateTime.Parse("2024-08-03T15:27:41Z");
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);
            var date = result as DateTime?;

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.IsNotNull(date);
            Assert.AreEqual(expected.ToUniversalTime(), date.Value.ToUniversalTime());
        }


        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertHintedPicklist()
        {
            // Arrange
            var input = "{ \"input@picklist\": 1 }";
            var expected = new OptionSetValue(1);
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void ParameterConversionService_Convert_CanConvertHintedMoney()
        {
            // Arrange
            var input = "{ \"input@money\": 7.8 }";
            var expected = new Money(7.8m);
            var parsedInput = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            // Act
            var result = service.Convert(parsedInput.First().Key, parsedInput.First().Value);

            // Assert
            Assert.AreEqual(expected.GetType(), result.GetType());
            Assert.AreEqual(expected, result);
        }
    }
}