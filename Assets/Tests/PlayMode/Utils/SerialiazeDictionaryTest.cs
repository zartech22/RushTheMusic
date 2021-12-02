using NUnit.Framework;
using System.Collections.Generic;

//TODO: explain your FUNCKING TEST (like youyou in Tests/PlayMode/Enemy/ActionZoneTest)

namespace Aloha.Test
{
    /// <summary>
    /// TODO
    /// </summary>
    public class SerialiazeDictionaryTest
    {
        /// <summary>
        /// TODO
        /// </summary>
        public SerializeDictionary<int, string> GetHelloWorldDictionary()
        {
            List<int> keys = new List<int>();
            keys.Add(3);
            keys.Add(5);

            List<string> values = new List<string>();
            values.Add("Hello");
            values.Add("World");

            SerializeDictionary<int, string> sd = new SerializeDictionary<int, string>(keys, values);
            Assert.AreEqual(2, sd.DictionaryKey.Count);
            Assert.AreEqual(2, sd.DictionaryValue.Count);
            return sd;
        }

        /// <summary>
        /// TODO
        /// </summary>
        [Test]
        public void SerialiazeDictionaryTestConstructor()
        {
            SerializeDictionary<int, string> sd0 = new SerializeDictionary<int, string>();
            Assert.AreEqual(0, sd0.DictionaryKey.Count);
            Assert.AreEqual(0, sd0.DictionaryValue.Count);

            SerializeDictionary<int, string> sd1 = GetHelloWorldDictionary();
            Assert.AreEqual(2, sd1.DictionaryKey.Count);
            Assert.AreEqual(2, sd1.DictionaryValue.Count);
        }

        /// <summary>
        /// TODO
        /// </summary>
        [Test]
        public void SerialiazeDictionaryTestGetValue()
        {
            SerializeDictionary<int, string> sd = GetHelloWorldDictionary();
            Assert.AreEqual("Hello", sd.GetValue(3));
            Assert.AreEqual("World", sd.GetValue(5));
        }

        /// <summary>
        /// TODO
        /// </summary>
        [Test]
        public void SerialiazeDictionaryTestAdd()
        {
            SerializeDictionary<int, string> sd = GetHelloWorldDictionary();
            sd.Add(13, "Dodo");
            Assert.AreEqual("Dodo", sd.GetValue(13));

            sd.Add(13, "Titi");
            Assert.AreEqual("Titi", sd.GetValue(13));
        }
    }
}