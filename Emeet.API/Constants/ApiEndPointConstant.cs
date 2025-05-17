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

        public static class User
        {
            public const string UserEndpoint = ApiEndpoint + "/user";
        }
        public static class Expert
        {
            public const string ExpertEndpoint = ApiEndpoint + "/expert";
        }
        public static class Appointment
        {
            public const string AppointmentEndpoint = ApiEndpoint + "/appointment";
        }
        public static class Schedule
        {
            public const string ScheduleEndpoint = ApiEndpoint + "/schedule";
        }
        public static class Feedback
        {
            public const string FeedbackEndpoint = ApiEndpoint + "/feedback";
        }
        public static class Payment
        {
            public const string PaymentEndpoint = ApiEndpoint + "/payment";
        }

    }
}
