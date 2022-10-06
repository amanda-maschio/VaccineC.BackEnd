﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class EventAppService : IEventAppService
    {

        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public EventAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventViewModel>> GetAllAsync()
        {
            var events = await _queryContext.AllEvents.ToListAsync();
            var eventsViewModel = events.Select(r => _mapper.Map<EventViewModel>(r)).ToList();
            return eventsViewModel;
        }

        public EventViewModel GetById(Guid id)
        {
            var eventClass = _mapper.Map<EventViewModel>(_queryContext.AllEvents.Where(r => r.ID == id).First());
            return eventClass;
        }
    }
}