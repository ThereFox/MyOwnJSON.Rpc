using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using ThereFox.JsonRPC.AspNet.Register.Responses;
using ThereFox.JsonRPC.Common;
using ThereFox.JsonRPC.Core.Client.Abstractions;

namespace ThereFox.JsonRPC.Core.Client;

public class JSONRpcClient : IRPCClient
{
    private readonly HttpClient _client;
    private readonly JsonRPCRequestBuilder _requestBuilder;
    
    
    public JSONRpcClient(Uri url)
    {
        _client = new HttpClient() { BaseAddress = url };
        _requestBuilder = new JsonRPCRequestBuilder();
    }
    
    public async Task<Result<TResult>> CallAsync<TResult>(string method, params object[] args)
    {
        var requestString = _requestBuilder.CreateRequest(method, args.ToList());
        
        var getResponseResult = await ExecuteRequest(requestString);

        if (getResponseResult.IsFailure)
        {
            return getResponseResult.ConvertFailure<TResult>();
        }
        
        var responseString = getResponseResult.Value;
        
        var bodyOkParse = ResultJsonDeserialiser.Deserialise<FormattableOKResponse>(responseString);

        if (bodyOkParse.IsFailure)
        {
            var bodyErrorParse = ResultJsonDeserialiser.Deserialise<FormattableErrorResponse>(responseString);

            if (bodyErrorParse.IsFailure)
            {
                return Result.Failure<TResult>("SystemError");
            }

            return Result.Failure<TResult>(bodyErrorParse.Value.ErrorMessage);
        }

        var result = bodyOkParse.Value.Result;

        if (result.GetType().IsPrimitive && result.GetType() != typeof(string))
        {
            return Result.Success<TResult>((TResult)Convert.ChangeType(result, typeof(TResult)));
        }
            
        if (result.GetType() == typeof(string) && typeof(TResult) == typeof(string))
        {
            return Result.Success<TResult>((TResult)result);
        }
        
        return ResultJsonDeserialiser.Deserialise<TResult>((string)result);

    }

    private async Task<Result<string>> ExecuteRequest(string requestString)
    {
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            Content = new StringContent(requestString)
        };

        try
        {
            var executeResult = await _client.SendAsync(request);

            return await executeResult.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            return Result.Failure<string>(e.Message);
        }
    }
}