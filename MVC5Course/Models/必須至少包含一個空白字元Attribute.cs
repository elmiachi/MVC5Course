using System;
using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models
{
    public class 必須至少包含一個空白字元Attribute : DataTypeAttribute
    {
        public 必須至少包含一個空白字元Attribute() : base(DataType.Text)
        {

        }

        public override bool IsValid(object value)
        {
            var str = (string)value;
            return str.Contains(" "); 
        }
    }
}