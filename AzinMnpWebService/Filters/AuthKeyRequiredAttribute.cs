namespace AzinMnpWebService.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthKeyRequiredAttribute : Attribute
{
}