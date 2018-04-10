﻿using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

namespace Microb.Update {
    
    class Function: MicrobFunction {
        
        //--- Methods ---
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public APIGatewayProxyResponse LambdaHandler(APIGatewayProxyRequest request) {
            LambdaLogger.Log(JsonConvert.SerializeObject(request));
            try {
                // TODO Read single item
                return new APIGatewayProxyResponse {
                    Body = "{\"message\": \"TODO\"}",
                    StatusCode = 200
                };
            }
            catch (Exception e) {
                LambdaLogger.Log($"*** ERROR: {e}");
                return new APIGatewayProxyResponse {
                    Body = "{\"message\": \"{e.message}\"}",
                    StatusCode = 500
                };
            }
        }

        private async Task UpdateItem(string id, string title, string content) {
            var item = new Document();
            item["Id"] = id.ToString();
            item["Title"] = title;
            item["Content"] = content;
            _table.UpdateItemAsync(item);
        }
    }
}