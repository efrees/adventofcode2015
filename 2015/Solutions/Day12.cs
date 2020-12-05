using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Dynamic;
using System.Collections;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;

namespace AdventOfCode.Solutions
{
    public class Day12
    {
        public static FileInfo GetInputFileInfo()
        {
            return new FileInfo("..\\..\\Inputs\\Day12input.txt");
        }

        public static void ProcessInput(StreamReader inputReader)
        {
            var sumOfNumbers = 0;
            var filteredSum = 0;

            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });

            string inputLine;

            while (!string.IsNullOrEmpty(inputLine = inputReader.ReadLine()))
            {
                DynamicJsonObject json = jss.Deserialize(inputLine, typeof(object)) as DynamicJsonObject;

                sumOfNumbers += json.SumIntegerValues();
                filteredSum += json.SumIntegersExcludingRedObjects();
            }

            Console.WriteLine("Sum of all integers in JSON hierarchy: " + sumOfNumbers);
            Console.WriteLine("Sum of all integers on non-red objects: " + filteredSum);
        }
    }

    //Parsing strategy borrowed from http://www.drowningintechnicaldebt.com/ShawnWeisfeld/archive/2010/08/22/using-c-4.0-and-dynamic-to-parse-json.aspx
    public class DynamicJsonObject : DynamicObject
    {
        private IDictionary<string, object> Dictionary { get; set; }

        public DynamicJsonObject(IDictionary<string, object> dictionary)
        {
            this.Dictionary = dictionary;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = this.Dictionary[binder.Name];

            if (result is IDictionary<string, object>)
            {
                result = new DynamicJsonObject(result as IDictionary<string, object>);
            }
            else if (result is ArrayList)
            {
                var arrayList = result as ArrayList;
                if (arrayList.Count > 0 && arrayList[0] is IDictionary<string, object>)
                {
                    result = new List<DynamicJsonObject>((result as ArrayList).ToArray().Select(x => new DynamicJsonObject(x as IDictionary<string, object>)));
                }
                else
                {
                    result = new List<object>((result as ArrayList).ToArray());
                }
            }

            return this.Dictionary.ContainsKey(binder.Name);
        }

        public int SumIntegerValues()
        {
            return GetSumRecursive(Dictionary);
        }

        public int SumIntegersExcludingRedObjects()
        {
            return GetSumRecursive(Dictionary, true);
        }

        private int GetSumRecursive(object value, bool filterReds = false)
        {
            var sum = 0;

            if (value is IDictionary<string, object>)
            {
                var dict = value as IDictionary<string, object>;

                if (!filterReds || !HasRedValue(dict))
                {
                    foreach (var nestedVal in dict.Values)
                    {
                        sum += GetSumRecursive(nestedVal, filterReds);
                    }
                }
            }
            else if (value is ArrayList)
            {
                foreach (var nestedVal in (value as ArrayList))
                {
                    sum += GetSumRecursive(nestedVal, filterReds);
                }
            }
            else if (value is int)
            {
                sum += value as int? ?? 0;
            }
            else
            {
                //Skip
            }

            return sum;
        }

        private bool HasRedValue(IDictionary<string, object> dictionary)
        {
            if (dictionary != null)
            {
                foreach (var value in dictionary.Values)
                {
                    if (value.ToString().ToLower() == "red")
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    public class DynamicJsonConverter : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            if (type == typeof(object))
            {
                return new DynamicJsonObject(dictionary);
            }

            return null;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(object) })); }
        }
    }
}
