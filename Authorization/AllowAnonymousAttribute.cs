namespace DiplomaAPI.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    {
    }
}
