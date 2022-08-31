﻿using MediatR;

namespace VaccineC.Command.Application.Commands.Company
{
    public class UpdateCompanyCommand : IRequest<Guid>
    {
        public Guid ID;
        public Guid PersonId { get; set; }
        public string Details;
        public DateTime Register;

        public UpdateCompanyCommand(Guid id, Guid personId, string details, DateTime register)
        {
            ID = id;
            PersonId = personId;
            Details = details;
            Register = register;
        }
    }
}