using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SistemaMonitoramento.Util.Servicos
{
    public static class Converter
    {
        public static string ParaTexto(string texto)
        {
            var _retorno = "";

            if (texto != null)
                _retorno = texto;

            return _retorno;
        }

        public static double ParaValor(string valor)
        {
            double _retorno = 0;

            if (valor != null)
                _retorno = Convert.ToDouble(valor.Replace(".", ","));

            return _retorno;
        }

        public static DateTime ParaData(string data)
        {
            DateTime _retorno = Convert.ToDateTime("1900-01-01");

            if (data != null)
                _retorno = Convert.ToDateTime(data);

            return _retorno;
        }

        public static string XmlEnumToString<TEnum>(this TEnum value) where TEnum : struct, IConvertible
        {
            Type enumType = typeof(TEnum);
            if (!enumType.IsEnum)
                return null;

            MemberInfo member = enumType.GetMember(value.ToString()).FirstOrDefault();
            if (member == null)
                return null;

            XmlEnumAttribute attribute = member.GetCustomAttributes(false).OfType<XmlEnumAttribute>().FirstOrDefault();
            if (attribute == null)
                return member.Name; // Fallback to the member name when there's no attribute

            return attribute.Name;
        }


        public static T GetObject<T>()
        {
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            return obj;
        }



        public static void MapDataToObject<T>(this IDataReader dataReader, T newObject)
        {
            if (newObject == null) throw new ArgumentNullException(nameof(newObject));

            // Fast Member Usage
            var objectMemberAccessor = TypeAccessor.Create(newObject.GetType());
            var propertiesHashSet =
                    objectMemberAccessor
                    .GetMembers()
                    .Select(mp => mp.Name)
                    .ToHashSet(StringComparer.InvariantCultureIgnoreCase);

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                var name = propertiesHashSet.FirstOrDefault(a => a.Equals(dataReader.GetName(i), StringComparison.InvariantCultureIgnoreCase));
                if (!String.IsNullOrEmpty(name))
                {
                    try
                    {
                        objectMemberAccessor[newObject, name]
                        = dataReader.IsDBNull(i) ? null : dataReader.GetValue(i);
                    }
                    finally
                    {

                    }
                }
            }
        }

        public static byte[] ObjectToByteArray(object objData)
        {
            if (objData == null)
                return default;

            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(objData, GetJsonSerializerOptions()));
        }

        /// <summary>
        /// Convert a byte array to an Object of T.
        /// </summary>

        public static T ByteArrayToObject<T>(byte[] byteArray)
        {
            if (byteArray == null || !byteArray.Any())
                return default;

            return JsonSerializer.Deserialize<T>(byteArray, GetJsonSerializerOptions());
        }


        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,
                WriteIndented = true,
                AllowTrailingCommas = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
        }



    }
}
