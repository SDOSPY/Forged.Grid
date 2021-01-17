using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.WebUtilities;
using System;

namespace Forged.Grid.Tests
{
    public static class HttpUtility
    {
        public static IQueryCollection ParseQueryString(string query)
        {
            return new QueryCollection(QueryHelpers.ParseQuery(query));
        }
    }
}
