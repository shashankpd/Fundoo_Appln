﻿
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonLayer.Model.Validation
{
    public class UserRequestValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Registration valid = validationContext.ObjectInstance as Registration;
            if (valid != null)
            {
                if (Regex.IsMatch(valid.firstName, @"^[a-zA-Z]{1,20}$"))
                {
                    if (Regex.IsMatch(valid.lastName, "^[a-zA-Z]{1,20}$"))
                    {
                        if (!Regex.IsMatch(valid.emailId, @"^([a-z0-9]+)@([a-z]+)((\.[a-z]{2,3})+)$"))
                        {

                            return new ValidationResult("Give Email id in correct Format");
                        }
                        else
                        {
                            if (!Regex.IsMatch(valid.password, @"^(?=.[a-z])(?=.[A-Z])(?=.\d)[a-zA-Z\d!@#$%^&]{8,16}$"))

                            {
                                return new ValidationResult("Given Password is incorrect Format");
                            }
                        }
                    }
                    else return new ValidationResult("Given LastName is incorrect Format");
                }
                else return new ValidationResult("Given FirstName is incorrect Format");

                return ValidationResult.Success;


            }
            return new ValidationResult("The given object is null");
        }
    }

}
