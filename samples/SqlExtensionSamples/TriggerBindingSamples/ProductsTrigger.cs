// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Sql;
using Microsoft.Extensions.Logging;
using static SqlExtensionSamples.ProductUtilities;

namespace SqlExtensionSamples.TriggerBindingSamples
{
    public static class ProductsTrigger
    {
        [FunctionName("ProductsTrigger")]
        public static void Run(
            [SqlTrigger("[dbo].[Products]", ConnectionStringSetting = "SqlConnectionString")]
            IEnumerable<SqlChangeTrackingEntry<Product>> changes,
            ILogger logger)
        {
            foreach (var change in changes)
            {
                Product product = change.Data;
                logger.LogInformation($"Change occurred to Products table row: {change.ChangeType}");
                logger.LogInformation($"ProductID: {product.ProductID}, Name: {product.Name}, Cost: {product.Cost}");
            }
        }
    }
}
