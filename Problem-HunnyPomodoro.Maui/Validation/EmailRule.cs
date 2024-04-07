﻿using System.Text.RegularExpressions;

namespace HunnyPomodoro.Validation;

public class EmailRule<T> : IValidationRule<T>
{
    private readonly Regex _regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

    public string ValidationMessage { get; set; }

    public bool Check(T value) =>
        value is string str && _regex.IsMatch(str);
}
