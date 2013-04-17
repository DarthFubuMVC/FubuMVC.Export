using System;
using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.Core.Http;

namespace FubuMVC.Export.Tests
{
    public class StubCurrentHttpRequest : ICurrentHttpRequest
    {
        public string ApplicationRoot = "http://server";
        public string TheHttpMethod = "GET";
        public string TheRawUrl;
        public string TheRelativeUrl;

        public string RawUrl()
        {
            return TheRawUrl;
        }

        public string RelativeUrl()
        {
            return TheRelativeUrl;
        }

        public string FullUrl()
        {
            return ApplicationRoot;
        }

        public string ToFullUrl(string url)
        {
            return url.ToAbsoluteUrl(ApplicationRoot);
        }

        public string HttpMethod()
        {
            return TheHttpMethod;
        }

        public bool HasHeader(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetHeader(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> AllHeaderKeys()
        {
            throw new NotImplementedException();
        }
    }
}
