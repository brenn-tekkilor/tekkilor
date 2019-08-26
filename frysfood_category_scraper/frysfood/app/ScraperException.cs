#nullable enable
using System;
namespace scraper.frysfood.app
{
    [System.Serializable]
    public class ScraperException : Exception
    {
        public ScraperException() { }
        public ScraperException(string message) : base(message) { }
        public ScraperException(string message, Exception inner) : base(message, inner) { }
        protected ScraperException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
