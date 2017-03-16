using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using DevLab.JmesPath.Expressions;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace DevLab.JmesPath.Functions
{
    public class MinFunction : MathArrayFunction
    {
        public MinFunction()
            : base("min", 1)
        {

        }

        public override JToken Execute(params JmesPathArgument[] args)
        {
            var arg = ((JArray) (args[0].AsJToken()));

            if (arg.Count == 0)
                return JTokens.Null;

            var types = arg.Select(u => u.Type).Distinct().ToArray();
            if (types.Count() != 1)
                throw new Exception("invalid-type");

            var type = types[0];
            switch (type)
            {
                case JTokenType.Float:
                {
                    var s = ((JArray) (args[0].AsJToken()))
                        .Select(u => u.Value<Double>()).ToArray();
                    return s.Any() ? new JValue(s.Min()) : null;
                }

                case JTokenType.Integer:
                {
                    var s = ((JArray) (args[0].AsJToken()))
                        .Select(u => u.Value<Int32>()).ToArray();
                    return s.Any() ? new JValue(s.Min()) : null;
                }
                default:
                {
                    var s = ((JArray) (args[0].AsJToken()))
                        .Select(u => u.Value<String>()).ToArray();
                    return s.Any() ? new JValue(s.Min()) : null;
                }
            }
        }
    }
}