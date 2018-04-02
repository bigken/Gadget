using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Schema;

namespace Gadget.Core.Tools.JsonSchema
{
    public class JsonSchemaInputData : GadgetData
    {
        public SchemaVersion Version { get; set; }
    }
}
