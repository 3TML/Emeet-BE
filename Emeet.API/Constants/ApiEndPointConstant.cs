namespace Emeet.API.Constants
{
    public class ApiEndPointConstant
    {
        public const string RootEndPoint = "/api";
        public const string ApiVersion = "/v1";
        public const string ApiEndpoint = RootEndPoint + ApiVersion;

        public static class Authentication
        {
            public const string AuthenticationEndpoint = ApiEndpoint + "/auth";
            public const string Login = AuthenticationEndpoint + "/login";
            public const string Info = AuthenticationEndpoint + "/info";
        }

        public static class Category
        {
            public const string CategoryEndpoint = ApiEndpoint + "/category";
        }

        public static class OTP
        {
            public const string OTPEndpoint = ApiEndpoint + "/otp";
        }
    }
}
