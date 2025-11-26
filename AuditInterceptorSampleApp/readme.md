# About

In this project an [Interceptor](https://learn.microsoft.com/en-us/ef/core/logging-events-diagnostics/interceptors) is used to inspect changes, write the changes, current and proposed values to a log file with the help of SeriLog.


There are several available interceptors, for this project we will use the [SaveChangesInterceptor](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.diagnostics.isavechangesinterceptor?view=efcore-7.0).