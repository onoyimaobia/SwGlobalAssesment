using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwGlobal.Notification
{
    public enum AlertStatus
    {
        Success = 0,
        Error = 1,
        Info = 2,
        Warning = 3
    }
    public class Alert
    {
        private string alertMessage;
        private AlertStatus alertStatus;
        public Alert(string alertMessage, AlertStatus alertStatus)
        {
            this.alertMessage = alertMessage;
            this.alertStatus = alertStatus;
        }
        public string AlertMessage
        {
            get
            {
                return alertMessage;
            }
            set
            {
                alertMessage = value;
            }
        }
        public string AlertStatus
        {
            get
            {
                string status = "info";
                int statusInt = (int)alertStatus;
                switch (statusInt)
                {
                    case 0:
                        status = "success";
                        break;
                    case 1:
                        status = "danger";
                        break;
                    case 2:
                        status = "info";
                        break;
                    case 3:
                        status = "warning";
                        break;
                }
                return status;
            }
        }
    }
}