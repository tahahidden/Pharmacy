using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace Pharmacy.WebApi.WebFramework.Transform
{
    public class KebabCaseParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value == null)
                return null;
            // پیاده‌سازی ساده
                return Regex.Replace(value?.ToString() ?? "", "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }
}