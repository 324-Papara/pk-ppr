using Microsoft.AspNetCore.Mvc.Filters;

namespace Para.Base.Attribute;

public class ResponseHeader : ActionFilterAttribute
{
    private readonly string _name;
    private readonly string _value;

    public ResponseHeader(string name, string value) => (_name, _value) = (name, value);
    
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers.Add(_name,_value);
        context.HttpContext.Response.Headers.Add("ResponseHash",Guid.NewGuid().ToString());
        base.OnResultExecuting(context);
    }
}