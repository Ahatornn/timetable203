﻿using Microsoft.AspNetCore.Mvc;
using TimeTable203.Api.Models.Exceptions;

namespace TimeTable203.Api.Attribute
{
    /// <summary>
    /// Фильтр, который определяет тип значения и код состояния 406, возвращаемый действием
    /// </summary>
    public class ApiNotAcceptableAttribute : ProducesResponseTypeAttribute
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ApiNotAcceptableAttribute"/>
        /// </summary>
        public ApiNotAcceptableAttribute() : this(typeof(ApiExceptionDetail))
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ApiNotAcceptableAttribute"/>
        /// </summary>
        public ApiNotAcceptableAttribute(Type type)
            : base(type, StatusCodes.Status406NotAcceptable)
        {
        }
    }
}