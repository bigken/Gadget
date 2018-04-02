using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace Gadget.Core.Tools.JsonSchema
{
    public class JsonSchemaTool : IGadgetTool<JsonSchemaInputData>
    {
        public string Go(JsonSchemaInputData inputData)
        {
            var jtoken = JToken.Parse(inputData.RawMaterial);

            return GenerateRootSchema(
                jtoken).ToString(
                inputData.Version);
        }

        JSchema GenerateRootSchema(JToken jtoken)
        {
            JSchema schema = new JSchema();

            if (jtoken.Type == JTokenType.Array)
            {
                schema.Type = JSchemaType.Array;
                schema.Items.Add(GenerateRootSchema(jtoken.Children().First()));
            }
            else if (jtoken.Type == JTokenType.Object)
            {
                schema.Type = JSchemaType.Object;

                foreach (var property in (jtoken as JObject).Properties())
                {
                    schema.Properties.Add(property.Name,
                        GenerateSubSchema(property));
                }
            }
            else if (jtoken.Type == JTokenType.Date)
            {
                schema.Type = JSchemaType.String;
                schema.Format = "date-time";
            }
            else
            {
                schema.Type = GenerateJSchemaType(jtoken.Type);
            }

            return schema;
        }

        JSchema GenerateSubSchema(JProperty property)
        {
            if (property.Value.Type == JTokenType.Array || property.Value.Type == JTokenType.Object)
            {
                return GenerateRootSchema(property.Children().First());
            }

            if (property.Value.Type == JTokenType.Date)
            {
                return new JSchema()
                {
                    Type = JSchemaType.String,
                    Format = "date-time"
                };
            }

            return new JSchema()
            {
                Type = GenerateJSchemaType(property.Value.Type)
            };
        }

        JSchemaType GenerateJSchemaType(JTokenType type)
        {
            switch (type)
            {
                case JTokenType.Integer:
                    return JSchemaType.Integer;
                case JTokenType.Boolean:
                    return JSchemaType.Boolean;
                case JTokenType.Float:
                    return JSchemaType.Number;
                case JTokenType.String:
                    return JSchemaType.String;
                default:
                    return JSchemaType.None;
            }
        }
    }
}
