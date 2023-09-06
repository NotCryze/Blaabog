using Newtonsoft.Json;
using SBO.BlaaBog.Domain.Entities;

namespace SBO.BlaaBog.Web.Utils
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static void AddToastNotification(this ISession session, ToastNotification toastNotification)
        {
            Queue<ToastNotification> toastNotifications = session.GetObject<Queue<ToastNotification>>("ToastNotifications");

            if (toastNotifications == null)
            {
                toastNotifications = new Queue<ToastNotification>();
            }

            toastNotifications.Enqueue(toastNotification);

            session.SetObject("ToastNotifications", toastNotifications);
        }
    }
}
