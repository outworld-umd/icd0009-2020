using System;
using Contracts.DAL.Base;
using Domain;

namespace PublicApi.DTO {

    public abstract class BaseDTO {
        public Guid Id { get; set; }
    }

}