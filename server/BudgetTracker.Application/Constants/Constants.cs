﻿namespace BudgetTracker.Application.Constants;

public static class Constants
{
    public static readonly List<string> UserRoles = new()
    {
        UserRole.ADMIN,
        UserRole.USER
    };

    public static class UserRole
    {
        public const string ADMIN = "ADMIN";
        public const string USER = "USER";
    }
}