using System.Diagnostics;
using Gadget.Core;
using Gadget.Core.Tools.JsonSchema;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gadget.CoreTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            JsonSchemaTool tool = new JsonSchemaTool();

            var data = tool.Go(new JsonSchemaInputData("{\"checked\": false,\"dimensions\": {\"width\": 5,\"height\": 10  },  \"id\": 1,  \"name\": \"A green door\",  \"price\": 12.5,  \"tags\": [    \"home\",    \"green\"  ]}"));

            Debug.WriteLine(data);
        }
    }
}
