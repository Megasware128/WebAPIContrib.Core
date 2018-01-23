﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace WebApiContrib.Core.Versioning
{
    internal class CompositeVersionStrategy : IVersionStrategy
    {
        public CompositeVersionStrategy(IReadOnlyCollection<IVersionStrategy> versionStrategies)
        {
            VersionStrategies = versionStrategies;
        }

        private IReadOnlyCollection<IVersionStrategy> VersionStrategies { get; }

        public VersionResult? GetVersion(HttpContext context, RouteData routeData)
        {
            foreach (var strategy in VersionStrategies)
            {
                var version = strategy.GetVersion(context, routeData);

                if (version.HasValue)
                {
                    return version;
                }
            }

            return null;
        }
    }
}
