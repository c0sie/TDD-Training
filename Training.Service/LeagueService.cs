﻿using Training.Repository.Pattern.Interfaces;
using Training.Service.Interfaces;

namespace Training.Service
{
    public class LeagueService : ILeagueService
    {
        private readonly IUnitOfWork unitOfWork;
    }
}