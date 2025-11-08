using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MyAWSLambda
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// For Amazon DynamoDB commented
        //public string FunctionHandler(User input, ILambdaContext context)
        //{
        //    return input.Name?.ToUpper();
        //}
        public async Task<User> FunctionHandler(Guid Id, ILambdaContext context)
        {
            var dynomoDBcontext = new DynamoDBContext(new AmazonDynamoDBClient());
            var user= await dynomoDBcontext.LoadAsync<User>(Id);
            return user;
        }
    }
    [DynamoDBTable("User")]
    public class User
    {
        [DynamoDBHashKey] // <-- Required
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

}
