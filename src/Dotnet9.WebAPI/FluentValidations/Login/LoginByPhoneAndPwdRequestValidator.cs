﻿namespace Dotnet9.WebAPI.FluentValidations.Login;

public class LoginByPhoneAndPwdRequestValidator : AbstractValidator<LoginByPhoneAndPwdRequest>
{
    public LoginByPhoneAndPwdRequestValidator()
    {
        RuleFor(e => e.PhoneNumber).NotNull().WithMessage("电话号码不能为Null").NotEmpty().WithMessage("电话号码不能为空");
        RuleFor(e => e.Password).NotNull().WithMessage("密码不能为Null").NotEmpty().WithMessage("密码不能为空");
    }
}