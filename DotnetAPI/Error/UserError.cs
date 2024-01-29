using System;

namespace DotnetAPI.Abstractions
{

    public class UserError
    {


        public static Error EmailIsInvalid = new Error("EmailIsInvalid", "Email is invalid");

        public static Error UserIsNotFound = new Error("UserID is not in the database", "User is database");


    }



}