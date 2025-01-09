using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.Common
{
    public static class ErrorCodes
    {
        // Common
        public const string common_error_internal_server_error = "common.error.internal_server_error";

        // Account
        public const string account_error_username_or_password_null = "account.error.username_or_password_null";
        public const string account_error_wrong_username_or_password = "account.error.wrong_username_or_password";
        public const string account_error_no_logged_user = "account.error.no_logged_user";
        public const string account_error_no_user_found = "account.error.no_user_found";

        // User
        public const string user_error_cant_delete_loggedin_user = "user.error.cant_delete_loggedin_user";
        public const string user_error_invalid_form = "user.error.invalid_form";
        public const string user_error_user_already_registered = "user.error.user_already_registered";
        public const string user_error_invalid_email = "user.error.invalid_email";
        public const string user_error_invalid_name = "user.error.invalid_name";
        public const string user_error_invalid_profession = "user.error.invalid_profession";
        public const string user_error_invalid_role = "user.error.invalid_role";

        // Identity
        public const string identity_error_cant_add_to_role = "identity.error.cant_add_to_role";
        public const string identity_error_cant_remove_from_role = "identity.error.cant_remove_from_role";
        public const string identity_error_cant_create_user = "identity.error.cant_create_user";
        public const string identity_error_cant_delete_user = "identity.error.cant_delete_user";

        // Customer
        public const string customer_error_no_customer_found = "customer.error.no_customer_found";
        public const string customer_error_invalid_form = "customer.error.invalid_form";
        public const string customer_error_invalid_email = "customer.error.invalid_email";
        public const string customer_error_invalid_name = "customer.error.invalid_name";

        // Work
        public const string work_error_no_customer_found = "work.error.no_customer_found";
        public const string work_error_invalid_form = "work.error.invalid_form";
        public const string work_error_invalid_name = "work.error.invalid_name";
    }
}
