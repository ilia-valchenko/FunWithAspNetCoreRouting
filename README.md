# FunWithAspNetCoreRouting
There are some interesting steps from APS .NET Core pipeline:
 - DEBUG | Microsoft.AspNetCore.Routing.Matching.DfaMatcher | 1 candidate(s) found for the request path `/WeatherForecast`
 - DEBUG | Microsoft.AspNetCore.Routing.Matching.DfaMatcher| Endpoint `AspNetCoreLogging.Controllers.WeatherForecastController.PostAsync` with route pattern `WeatherForecast` is valid for the request path `/WeatherForecast`
 - DEBUG | Microsoft.AspNetCore.Routing.EndpointRoutingMiddleware| Request matched endpoint `AspNetCoreLogging.Controllers.WeatherForecastController.PostAsync`
 - INFO | Microsoft.AspNetCore.Routing.EndpointMiddleware | Executing endpoint `AspNetCoreLogging.Controllers.WeatherForecastController.PostAsync`
 - INFO | Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker | Route matched with {action = "Post", controller = "WeatherForecast"}. Executing controller action with signature ... 
 - DEBUG | Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker | Execution plan of authorization filters (in the following order): None 
 - DEBUG | Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker | Execution plan of resource filters (in the following order): None
 - DEBUG | Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker | Execution plan of action filters (in the following order)
 - DEBUG | Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker | Execution plan of exception filters (in the following order): None 
 - DEBUG | Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker | Execution plan of result filters (in the following order)
 - DEBUG | Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker | Executing controller factory for controller `AspNetCoreLogging.Controllers.WeatherForecastController`
 - DEBUG | Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker | Executed controller factory for controller `AspNetCoreLogging.Controllers.WeatherForecastController`
 - DEBUG | Microsoft.AspNetCore.Mvc.ModelBinding.ParameterBinder | Attempting to bind parameter `forecast` of type `AspNetCoreLogging.Services.Models.WeatherForecast`
 
 ## Filters order
 1. Authorization filters
 2. Resource filters
 3. Action filters
 4. Exception filters
 5. Result filters